using System;
using System.Linq.Expressions;
using Newtonsoft.Json.Serialization;
using Vindi.SDK.Linq;

namespace Vindi.SDK.Services
{
    /// <summary>
    /// References:
    /// https://atendimento.vindi.com.br/hc/pt-br/articles/204163150-Listas-buscas-e-filtros
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class VindRequestParams<TEntity> where TEntity : class
    {
        protected string _paramQuery = null;
        protected string _paramSortBy = null;
        protected string _paramSortOrder = null;
        protected int _page = 0;
        protected int _perPage = 50;
        private  readonly SnakeCaseNamingStrategy _snakeCaseStrategy = new SnakeCaseNamingStrategy();

        public VindRequestParams(int page, int perPage)
        {
            _page = page;
            _perPage =  perPage;
        }

        public VindRequestParams<TEntity> Filters(Expression<Func<TEntity, bool>> filter)
        {
            _paramQuery = new FilterExpressionVisitor().Make(filter);
            return this;
        }

        public VindRequestParams<TEntity> RawFilters(string rawFilter)
        {
            _paramQuery = rawFilter;
            return this;
        }

        public VindRequestParams<TEntity> OrderBy<TPropValue>(Expression<Func<TEntity, TPropValue>> property)
        {
            if (property.Body.NodeType != ExpressionType.MemberAccess)
                throw new FilterExpressionException("Expected member access expression");

            var propertyName = (property.Body as MemberExpression).Member.Name;
            propertyName = _snakeCaseStrategy.GetPropertyName(propertyName, false);
            _paramSortBy = propertyName;
            _paramSortOrder = "asc";

            return this;
        }

        public VindRequestParams<TEntity> OrderByDescending<TPropValue>(Expression<Func<TEntity, TPropValue>> property)
        {
            if (property.Body.NodeType != ExpressionType.MemberAccess)
                throw new FilterExpressionException("Expected member access expression");

            var propertyName = (property.Body as MemberExpression).Member.Name;
            propertyName = _snakeCaseStrategy.GetPropertyName(propertyName, false);
            _paramSortBy = propertyName;
            _paramSortOrder = "desc";

            return this;
        }

        public VindRequestParamsRaw Build()
        {
            return new VindRequestParamsRaw
            {
                Page = _page,
                PerPage = _perPage,
                Query = _paramQuery,
                SortBy = _paramSortBy,
                SortOrder = _paramSortOrder
            };
        }
    }

}