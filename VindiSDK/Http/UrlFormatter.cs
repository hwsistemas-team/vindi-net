using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Vindi.SDK.Http
{
    public static class UrlFormatter
    {
        /// <summary>
        /// </summary>
        /// <param name="urlTemplate">Sample: https://external-api/resourse/{id}/sub</param>
        /// <param name="urlParams">Sample: { { "id", "10" }, { "param1", "value1" }, { "param2", "value2" } }</param>
        /// <returns>Sample: https://external-api/resourse/10/sub?param1=value1&param2=value2</returns>
        public static string Format(string urlTemplate, IEnumerable<KeyValuePair<string, string>> urlParams, bool ignoreParamsNull = true)
        {
            var url = urlTemplate;
            var queryParams = new StringBuilder();

            foreach(var urlParam in urlParams)
            {
                if (urlParam.Value == null && ignoreParamsNull)
                    continue;

                var newUrl = url.Replace("{" + urlParam.Key + "}", urlParam.Value);
                var isUriParam = url != newUrl;

                if (isUriParam)
                {
                    url = newUrl;
                    continue;
                }

                var key = HttpUtility.UrlEncode(urlParam.Key);
                var value = HttpUtility.UrlEncode(urlParam.Value);

                if (queryParams.Length > 0)
                    queryParams.Append("&");

                queryParams.Append($"{key}={value}");
            }

            if (queryParams.Length > 0)
                url = url + "?" + queryParams.ToString();

            return url;
        }
    }
}