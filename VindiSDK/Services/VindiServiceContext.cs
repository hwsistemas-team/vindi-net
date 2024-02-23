using System;

namespace Vindi.SDK.Services
{
    public class VindiServiceContext
    {
        public string ApiKey { get; private set; }
        public string BaseUrl { get; private set; }

        public VindiServiceContext(string baseUrl, string apiKey)
        {
            BaseUrl = baseUrl;
            ApiKey = apiKey;
        }
    }
}