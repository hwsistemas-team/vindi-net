using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace Vindi.SDK.Linq
{
    class FilterExpressionVisitor : ExpressionVisitor
    {
        private StringBuilder queryBuild;
        private Expression rightExpression = null;
        private SnakeCaseNamingStrategy snakeCaseStrategy = new SnakeCaseNamingStrategy();

        public string Make(Expression exp)
        {
            queryBuild = new StringBuilder();
            this.Visit(exp);
            return queryBuild.ToString();
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            queryBuild.Append("(");

            rightExpression = null;
            this.Visit(node.Left);

            switch (node.NodeType)
            {
                case ExpressionType.GreaterThan:
                    queryBuild.Append(">");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    queryBuild.Append(">=");
                    break;
                case ExpressionType.LessThan:
                    queryBuild.Append("<");
                    break;
                case ExpressionType.LessThanOrEqual:
                    queryBuild.Append("<=");
                    break;
                case ExpressionType.Equal:
                    queryBuild.Append("=");
                    break;
                case ExpressionType.AndAlso:
                    queryBuild.Append(" and ");
                    break;
                case ExpressionType.OrElse:
                    queryBuild.Append(" or ");
                    break;
                default:
                    throw new FilterExpressionException("Unsupported operator: " + node.NodeType);
            }

            rightExpression = node.Right;
            this.Visit(node.Right);

            queryBuild.Append(")");

            return node;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.NodeType == ExpressionType.Not)
                queryBuild.Append("-");

            return base.VisitUnary(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            var value = FormatterValue(node.Value);
            queryBuild.Append(value);

            return node;
        }

        protected override Expression VisitNew(NewExpression node)
        {
            if (node.Type == typeof(DateTime))
            {
                var dateParams = node.Arguments.OfType<ConstantExpression>().Select(x => x.Value).ToArray();
                var date = (DateTime)node.Constructor.Invoke(dateParams);
                queryBuild.Append(FormatterValue(date));

                return node;
            }
            else
                throw new FilterExpressionException("Unsupported parameter: " + node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression.NodeType == ExpressionType.Constant)
            {
                var constExp = node.Expression as ConstantExpression;
                object value = null;

                if (node.Member is FieldInfo)
                {
                    value = (node.Member as FieldInfo).GetValue(constExp.Value);
                }
                else if (node.Member is PropertyInfo)
                {
                    value = (node.Member as PropertyInfo).GetValue(constExp.Value);
                }

                value = FormatterValue(value);

                queryBuild.Append(value);
            }
            else
            {
                if (node.Member.DeclaringType == typeof(DateTime))
                {
                    var objectMember = Expression.Convert(node, typeof(DateTime));

                    var getterLambda = Expression.Lambda<Func<DateTime>>(objectMember);

                    var getter = getterLambda.Compile();
                    var value = FormatterValue(getter());

                    queryBuild.Append(value);
                }
                else
                {
                    if (rightExpression != null)
                    {
                        rightExpression = null;

                        object value = GetMemberValue(node);
                        value = FormatterValue(value);

                        queryBuild.Append(value);
                    }
                    else
                    {
                        string name = FormatterName(node.Member.Name);
                        queryBuild.Append(name);
                    }
                }
            }

            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(String.Contains))
            {
                this.Visit(node.Object);
                queryBuild.Append(":");
                this.Visit(node.Arguments[0]);

                return node;
            }
            else if (node.Method.Name == nameof(Object.Equals))
            {
                this.Visit(node.Object);
                queryBuild.Append("=");
                this.Visit(node.Arguments[0]);

                return node;
            }
            else
                throw new FilterExpressionException("Unsupporte Method: " + node.Method.Name);
        }


        private object GetMemberValue(MemberExpression node)
        {
            List<MemberInfo> members = new List<MemberInfo>();

            Expression exp = node;
            ConstantExpression ce = null;

            // There is a chain of getters in propertyToSet, with at the
            // beginning a ConstantExpression. We put the MemberInfo of
            // these getters in members and the ConstantExpression in ce

            while (exp != null)
            {
                MemberExpression mi = exp as MemberExpression;

                if (mi != null)
                {
                    members.Add(mi.Member);
                    exp = mi.Expression;
                }
                else
                {
                    ce = exp as ConstantExpression;
                    var t = exp as ParameterExpression;


                    if (ce == null)
                    {
                        // We support only a ConstantExpression at the base
                        // no function call like
                        // () => myfunc().A.B.C
                        throw new NotSupportedException();
                    }

                    break;
                }
            }

            if (members.Count == 0)
            {
                // We need at least a getter
                throw new NotSupportedException();
            }

            // Now we must walk the getters (excluding the last).
            // From the ConstantValue ce we take the base object
            object targetObject = ce.Value;

            // We have to walk the getters from last (most inner) to second
            // (the first one is the one we have to use as a setter)
            for (int i = members.Count - 1; i >= 1; i--)
            {
                PropertyInfo pi = members[i] as PropertyInfo;

                if (pi != null)
                {
                    targetObject = pi.GetValue(targetObject);
                }
                else
                {
                    FieldInfo fi = (FieldInfo)members[i];
                    targetObject = fi.GetValue(targetObject);
                }
            }

            // The first one is the getter we treat as a setter
            {
                PropertyInfo pi = members[0] as PropertyInfo;

                if (pi != null)
                {
                    return pi.GetValue(targetObject, null);
                }
                else
                {
                    FieldInfo fi = (FieldInfo)members[0];
                    return fi.GetValue(targetObject);
                }
            }
        }

        private object FormatterValue(object value)
        {
            if (value is string)
            {
                return $"\"{value}\"";
            }
            else if (value is DateTime)
            {
                return $"\"{((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss")}\"";
            }
            else
                return value;
        }

        private string FormatterName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return name;

            return snakeCaseStrategy.GetPropertyName(name, false);
        }
    }
}
