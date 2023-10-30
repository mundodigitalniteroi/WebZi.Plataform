using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using WebZi.Plataform.CrossCutting.Web;

namespace WebZi.Plataform.Data.Services.Sistema
{
    public class HttpClientFactoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientFactoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        public async Task<bool> ResourceExists(string url)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            using HttpResponseMessage response = await client.GetAsync(url);
            
            return response.IsSuccessStatusCode;
        }

        public T Get<T>(string url) where T : class
        {
            return Task.Run(async () => await GetAsync<T>(url)).Result;
        }

        public async Task<T> GetAsync<T>(string url) where T : class
        {
            using HttpClient client = _httpClientFactory.CreateClient();

            return await client.GetFromJsonAsync<T>(url,
                new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }

        public T Post<T>(string url, object obj) where T : class
        {
            return Task.Run(async () => await PostAsync<T>(url, obj)).Result;
        }

        public async Task<T> PostAsync<T>(string url, object obj) where T : class
        {
            using HttpClient client = _httpClientFactory.CreateClient();

            using StringContent stringContent = new(JsonHelper.Serialize(obj), Encoding.UTF8, "application/json");

            using HttpResponseMessage result = await client.PostAsync(url, stringContent);

            string json = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json);
        }

        public T PostBasicAuth<T>(string url, string username, string password, object obj) where T : class
        {
            return Task.Run(async () => await PostBasicAuthAsync<T>(url, username, password, obj)).Result;
        }

        public async Task<T> PostBasicAuthAsync<T>(string url, string username, string password, object obj) where T : class
        {
            using HttpClient client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));

            using StringContent stringContent = new(JsonHelper.Serialize(obj), Encoding.UTF8, "application/json");

            using HttpResponseMessage result = await client.PostAsync(url, stringContent);

            string json = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json);
        }

        public T PostBearerAuth<T>(string url, string accessToken, object obj) where T : class
        {
            return Task.Run(async () => await PostBearerAuthAsync<T>(url, accessToken, obj)).Result;
        }

        public async Task<T> PostBearerAuthAsync<T>(string url, string accessToken, object obj) where T : class
        {
            using HttpClient client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using StringContent stringContent = new(JsonHelper.Serialize(obj), Encoding.UTF8, "application/json");

            using HttpResponseMessage result = await client.PostAsync(url, stringContent);

            string json = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json);
        }

        public T DeleteBasicAuth<T>(string url, string username, string password, object obj) where T : class
        {
            return Task.Run(async () => await DeleteBasicAuthAsync<T>(url, username, password, obj)).Result;
        }

        public async Task<T> DeleteBasicAuthAsync<T>(string url, string username, string password, object obj) where T : class
        {
            using HttpClient client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));

            using HttpRequestMessage request = new()
            {
                Content = JsonContent.Create(obj),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url)
            };

            using HttpResponseMessage response = await client.SendAsync(request);

            return JsonHelper.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public byte[] DownloadFile(string url)
        {
            return Task.Run(async () => await DownloadFileAsync(url)).Result;
        }

        public async Task<byte[]> DownloadFileAsync(string url)
        {
            using HttpClient client = _httpClientFactory.CreateClient();

            using HttpResponseMessage response = await client.GetAsync(url);

            return await response.Content
                .ReadAsByteArrayAsync()
                .ConfigureAwait(false);
        }
    }
}