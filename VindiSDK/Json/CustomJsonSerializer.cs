using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Vindi.SDK.Json
{
    public class CustomJsonSerializer
    {
        public static JsonSerializerSettings GetJsonSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy(),
                }
            };
        }

        public static string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data, GetJsonSerializerSettings());
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, GetJsonSerializerSettings());
        }
    }
}