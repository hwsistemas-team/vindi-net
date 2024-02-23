using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;
using Vindi.SDK.Exceptions;
using Vindi.SDK.Json;

namespace Vindi.SDK.Services
{
    public class BaseService<TEntity> where TEntity : class
    {
        private readonly RestClient client;

        public BaseService(VindiServiceContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy(),
                }
            };

            client = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri(context.BaseUrl)
            });

            client.Authenticator = new HttpBasicAuthenticator(context.ApiKey, "");
            client.UseNewtonsoftJson(Json.CustomJsonSerializer.GetJsonSerializerSettings());
        }

        public async Task<VindiResponseWithData<TResultData>> GetAsync<TResultData>(string resource, VindRequestParams<TEntity> parameters = null) where TResultData : class
        {
            var request = new RestRequest(resource, Method.Get);

            if (parameters != null)
                FillQueryParams(request, parameters);

            var response = await client.ExecuteAsync<TResultData>(request);
            ThrowIfResponseError(response);

            return MakeVindiResponse(response);
        }

        public async Task<VindiResponseWithData<TResultData>> PostAsync<TInputData, TResultData>(string resource, TInputData data) where TInputData : class where TResultData : class
        {
            var request = new RestRequest(resource, Method.Post);
            request.AddJsonBody(data);

            var response = await client.ExecuteAsync<TResultData>(request);
            ThrowIfResponseError(response);

            return MakeVindiResponse(response);
        }

        public async Task<VindiResponseWithData<TResultData>> PutAsync<TInputData, TResultData>(string resource, TInputData data) where TInputData : class where TResultData : class
        {
            var request = new RestRequest(resource, Method.Put);
            request.AddJsonBody(data);

            var response = await client.ExecuteAsync<TResultData>(request);
            ThrowIfResponseError(response);

            return MakeVindiResponse(response);
        }

        public async Task<VindiResponse> DeleteAsync(string resource)
        {
            var request = new RestRequest(resource, Method.Get);

            var response = await client.ExecuteAsync(request);
            ThrowIfResponseError(response);

            return new VindiResponse
            {
                StatusCode = response.StatusCode,
                Headers = GetVindResponseHeaders(response)
            };
        }

        public async Task<VindiResponseWithData<TResultData>> DeleteAsync<TResultData>(string resource) where TResultData : class
        {
            var request = new RestRequest(resource, Method.Get);

            var response = await client.ExecuteAsync<TResultData>(request);
            ThrowIfResponseError(response);

            return MakeVindiResponse(response);
        }

        private void FillQueryParams(RestRequest request, VindRequestParams<TEntity> parameters)
        {
            var values = parameters.Build();

            request.AddQueryParameter("page", values.Page.ToString());
            request.AddQueryParameter("per_page", values.PerPage.ToString());

            if (!String.IsNullOrEmpty(values.Query))
                request.AddQueryParameter("query", values.Query);

            if (!String.IsNullOrEmpty(values.SortBy))
                request.AddQueryParameter("sort_by", values.SortBy);

            if (!String.IsNullOrEmpty(values.SortOrder))
                request.AddQueryParameter("sort_order", values.SortOrder);
        }

        private void ThrowIfResponseError(RestResponse response)
        {
            if (response.StatusCode == 0)
            {
                if (response.ErrorException != null)
                    throw response.ErrorException;
                else
                    throw new RequestException("Failed to perform request. " + response.ErrorMessage, response.StatusCode);
            }

            int statusClass = (int)response.StatusCode / 100;

            if (statusClass == 2)
                return;

            switch ((int)response.StatusCode)
            {
                case 401:
                    throw new RequestException("Unauthorized", response.StatusCode);

                case 402:
                    throw new PaymentRequiredException();

                case 406:
                    throw new RequestException("The submitted format is not accepted", response.StatusCode);

                case 422:
                    var wrapperErrors = JsonConvert.DeserializeObject<WrapperErros>(response.Content);
                    throw new ValidateException(wrapperErrors.Errors);

                case 429:
                    var headers = GetVindResponseHeaders(response);

                    throw new RateLimitException(
                        headers.RateLimitLimit ?? default,
                        headers.RateLimitReset ?? default,
                        headers.RateLimitRemaining ?? default,
                        headers.RetryAfter ?? default);

                default:
                    throw new RequestException($"Request failed with status code {(int)response.StatusCode}", response.StatusCode);
            }
        }

        private VindResponseHeaders GetVindResponseHeaders(RestResponse restResponse)
        {
            var headers = restResponse.Headers.ToDictionary(x => x.Name, x => x.Value);
            object value;

            return new VindResponseHeaders
            {
                RateLimitLimit = headers.TryGetValue("Rate-Limit-Limit", out value) ? Convert.ToInt32(value) : default,
                RateLimitReset = headers.TryGetValue("Rate-Limit-Reset", out value) ? Convert.ToInt32(value) : default,
                RateLimitRemaining = headers.TryGetValue("Rate-Limit-Remaining", out value) ? Convert.ToInt32(value) : default,
                RetryAfter = headers.TryGetValue("Retry-After", out value) ? Convert.ToInt32(value) : default,
                Total = headers.TryGetValue("Total", out value) ? Convert.ToInt32(value) : default,
            };
        }

        private VindiResponseWithData<TResultData> MakeVindiResponse<TResultData>(RestResponse<TResultData> response) where TResultData : class
        {
            var headers = GetVindResponseHeaders(response);

            return new VindiResponseWithData<TResultData>
            {
                StatusCode = response.StatusCode,
                Headers = headers,
                Data = response.Data,
                Total = headers.Total ?? default
            };
        }
    }
}