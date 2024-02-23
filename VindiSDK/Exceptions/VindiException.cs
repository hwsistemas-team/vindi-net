using System;

namespace Vindi.SDK.Exceptions
{
    public abstract class VindiException : Exception
    {
        public VindiException(string message) : base(message) { }

        public VindiException(string message, Exception innerException) : base(message, innerException) { }
    }
}
