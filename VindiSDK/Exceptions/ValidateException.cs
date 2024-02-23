using System;
using System.Collections.Generic;
using Vindi.SDK.Services;

namespace Vindi.SDK.Exceptions
{
    public class ValidateException : VindiException
    {
        public IEnumerable<ErrorDetail> Errors { get; set; }

        public ValidateException (IEnumerable<ErrorDetail> errors) : base("The request has invalid parameters. For more details, check the errors attribute.")
        {
            this.Errors = errors;
        }
    }
}
