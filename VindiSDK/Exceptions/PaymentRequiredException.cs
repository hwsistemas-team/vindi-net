using System;

namespace Vindi.SDK.Exceptions
{
    public class PaymentRequiredException : VindiException
    {
        public PaymentRequiredException() : base("Payment Required")
        {
        }
    }
}