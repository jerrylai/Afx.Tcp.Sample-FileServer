using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Client.Common
{
    public static class JsonUtils
    {
        private static Lazy<JsonSerializerSettings> _serializeSettings = new Lazy<JsonSerializerSettings>(() => 
        {
            var setting = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            return setting;
        }, false);

        public static JsonSerializerSettings SerializeSettings => _serializeSettings.Value;

        public static string Serialize<T>(T value)
        {
            if (value == null) return null;

            return JsonConvert.SerializeObject(value, SerializeSettings);
        }

        private static Lazy<JsonSerializerSettings> _deserializeSettings = new Lazy<JsonSerializerSettings>(() =>
        {
            var setting = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            return setting;
        }, false);

        public static JsonSerializerSettings DeserializeSettings => _deserializeSettings.Value;

        public static T Deserialize<T>(string json)
        {
            if (string.IsNullOrEmpty(json)) return default(T);

            return JsonConvert.DeserializeObject<T>(json, DeserializeSettings);
        }
    }
}
