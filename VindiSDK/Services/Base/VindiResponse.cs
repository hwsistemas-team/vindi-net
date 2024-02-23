using System.Net;

namespace Vindi.SDK.Services
{
    public class VindiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public VindResponseHeaders Headers { get; set; }
    }

    public class VindiResponseWithData<TData> : VindiResponse
    {
        public int Total { get; set; }
        public TData Data { get; set; }

        public VindiResponseWithData<T> MakeNewData<T>(T data) where T : class
        {
            return new VindiResponseWithData<T>
            {
                StatusCode = StatusCode,
                Headers = Headers,
                Total = Total,
                Data = data
            };
        }
    }

    public class VindResponseHeaders
    {
        public int? RateLimitLimit { get; set; }
        public int? RateLimitReset { get; set; }
        public int? RateLimitRemaining { get; set; }
        public int? RetryAfter { get; set; }
        public int? Total { get; set; }
    }
}