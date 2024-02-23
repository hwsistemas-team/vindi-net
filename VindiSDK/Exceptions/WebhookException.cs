using System;

namespace Vindi.SDK.Exceptions
{
    public class WebhookException : VindiException
    {
        public WebhookException(string message) : base(message) { }

        public WebhookException(string message, Exception innerException) : base(message, innerException) { }
    }
}