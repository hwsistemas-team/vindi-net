using System;

namespace Vindi.SDK.Exceptions
{
    public class RateLimitException : VindiException
    {
        public int Limit { get; set; }
        public int Reset { get; set; }
        public int Remaining { get; set; }
        public int RetryAfter { get; set; }


        public RateLimitException(int limit, int reset, int remaining, int retryAfter) : base($"The limit of {limit} requests per minute for the API has been reached.")
        {
            this.Limit = limit;
            this.Reset = reset;
            this.Remaining = remaining;
            this.RetryAfter = retryAfter;
        }
    }
}
