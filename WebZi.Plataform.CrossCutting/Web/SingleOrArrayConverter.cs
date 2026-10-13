using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace WebZi.Plataform.CrossCutting.Web
{
    public class SingleOrArrayConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<T>) || objectType == typeof(IList<T>) || objectType == typeof(IEnumerable<T>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return new List<T>();
            }

            JToken token = JToken.Load(reader);

            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<T>>(serializer) ?? new List<T>();
            }

            if (token.Type == JTokenType.Null || token.Type == JTokenType.Undefined)
            {
                return new List<T>();
            }

            T single = token.ToObject<T>(serializer);

            return single != null ? new List<T> { single } : new List<T>();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
