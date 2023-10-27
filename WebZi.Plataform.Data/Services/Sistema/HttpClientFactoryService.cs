namespace WebZi.Plataform.Data.Services.Sistema
{
    public class HttpClientFactoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientFactoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> ResourceExists(string url)
        {
            HttpClient client = _httpClientFactory.CreateClient();
 
            var response = await client.GetAsync(url);
            
            return response.IsSuccessStatusCode;
        }
    }
}