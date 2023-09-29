using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace WebZi.Plataform.CrossCutting.Web
{
    public abstract class HttpClientHelper
    {
        private static readonly HttpClient httpClient = new();

        private static void InitializeHttpClient(string baseAddress, string parameters = "")
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            httpClient.DefaultRequestHeaders.Accept.Clear();

            if (httpClient.BaseAddress != null && httpClient.BaseAddress.ToString() == (baseAddress.EndsWith("/") ? baseAddress : baseAddress + "/"))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(parameters))
            {
                httpClient.BaseAddress = new Uri(baseAddress.EndsWith("/") ? baseAddress : baseAddress + "/");
            }
            else
            {
                httpClient.BaseAddress = new Uri(baseAddress.EndsWith("/") ? baseAddress + parameters : baseAddress + "/" + parameters);
            }
        }

        public static T GetBasicAuth<T>(string url, string username, string password, string parameters) where T : class
        {
            return Task.Run(async () => await GetBasicAuthAsync<T>(url, username, password, parameters)).Result;
        }

        private static async Task<T> GetBasicAuthAsync<T>(string url, string username, string password, string parameters) where T : class
        {
            InitializeHttpClient(url, parameters);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));

            using (HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(""))
            {
                string result = await httpResponseMessage.Content.ReadAsStringAsync();

                return JsonHelper.DeserializeObject<T>(result);
            }
        }

        public static T PostBasicAuth<T>(string url, string username, string password, object obj) where T : class
        {
            return Task.Run(async () => await PostBasicAuthAsync<T>(url, username, password, obj)).Result;
        }

        private static async Task<T> PostBasicAuthAsync<T>(string url, string username, string password, object obj) where T : class
        {
            InitializeHttpClient(url);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));

            using (StringContent stringContent = new(JsonHelper.Serialize(obj), Encoding.UTF8, "application/json"))
            {
                using (HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("", stringContent))
                {
                    string result = await httpResponseMessage.Content.ReadAsStringAsync();

                    return JsonHelper.DeserializeObject<T>(result);
                }
            }
        }

        public static T PostBearerAuth<T>(string url, string accessToken, object obj) where T : class
        {
            return Task.Run(async () => await PostBearerAuthAsync<T>(url, accessToken, obj)).Result;
        }

        private static async Task<T> PostBearerAuthAsync<T>(string url, string accessToken, object obj) where T : class
        {
            InitializeHttpClient(url);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (StringContent stringContent = new(JsonHelper.Serialize(obj), Encoding.UTF8, "application/json"))
            {
                using (HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("", stringContent))
                {
                    string result = await httpResponseMessage.Content.ReadAsStringAsync();

                    return JsonHelper.DeserializeObject<T>(result);
                }
            }
        }

        public static async Task<byte[]> DownloadFile(string url)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            using var response = await httpClient.GetAsync(url);
            
            return await response.Content
                .ReadAsByteArrayAsync()
                .ConfigureAwait(false);
        }
    }
}