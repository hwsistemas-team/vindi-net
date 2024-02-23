using System;
using System.Collections.Generic;

namespace Vindi.SDK.Services
{
    public class ErrorDetail
    {
        public string Id { get; set; }
        public string Parameter { get; set; }
        public string Message { get; set; }
    }

    class WrapperErros
    {
        public IEnumerable<ErrorDetail> Errors { get; set; }
    }
}
