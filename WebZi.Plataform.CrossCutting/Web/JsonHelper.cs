using Newtonsoft.Json;

namespace WebZi.Plataform.CrossCutting.Web
{
    public abstract class JsonHelper
    {
        /// <summary>
        /// Convert an object to a JSON string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>A JSON string</returns>
        public static string Serialize(object obj)
        {
            Console.WriteLine(JsonConvert.SerializeObject(obj));

            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Convert an object to a JSON string
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="jsonSerializerSettings"></param>
        /// <returns>A JSON string</returns>
        public static string Serialize(object obj, JsonSerializerSettings jsonSerializerSettings)
        {
            Console.WriteLine(JsonConvert.SerializeObject(obj, jsonSerializerSettings));

            return JsonConvert.SerializeObject(obj, jsonSerializerSettings);
        }

        /// <summary>
        /// Convert a JSON to a generic type parameter <T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns>T</returns>
        public static T DeserializeObject<T>(string json) where T : class
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}