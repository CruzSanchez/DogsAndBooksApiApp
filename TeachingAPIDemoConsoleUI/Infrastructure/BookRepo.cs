
using Newtonsoft.Json;
using TeachingAPIDemoConsoleUI.Factory;

namespace TeachingAPIDemoConsoleUI.Infrastructure
{
    internal class BookRepo : IBookRepo
    {
        private readonly HttpClient _httpClient;

        public BookRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IList<Book> GetAllBooks(string url, string endpoint)
        {
            var request = RequestMessageBuilder.CreateRequestBuilder()
                                            .AsMethod(HttpMethodFactory.GetHttpMethod("get"))
                                            .ForUrl(url)
                                            .AtEndpoint(endpoint + "s")
                                            .Build();

            var response = SendRequest(request).Result;

            var json = GetJsonString(response).Result;

            var books = DeserializeJson<IList<Book>>(json);

            return books;
        }

        public Book GetBookById(string url, string endpoint, int id)
        {
            throw new NotImplementedException();
        }

        private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);
            return response;
        }

        private async Task<string> GetJsonString(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        private T DeserializeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
