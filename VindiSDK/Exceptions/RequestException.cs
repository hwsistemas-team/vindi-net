using System;
using System.Net;

namespace Vindi.SDK.Exceptions
{
    public class RequestException : VindiException
    {
        public HttpStatusCode StatusCode { get; private set; }

        public RequestException(string message, HttpStatusCode statusCode) : base(message)
        {
            this.StatusCode = statusCode;
        }
    }
}
