
using Newtonsoft.Json;
using TeachingAPIDemoConsoleUI.Factory;

namespace TeachingAPIDemoConsoleUI.Infrastructure
{
    public class ApiObjectRepo<T>(HttpClient httpClient) : IApiObjectRepo<T> where T : IApiType
    {
        private readonly HttpClient _httpClient = httpClient;

        public IList<T> GetAll(string url, string endpoint)
        {
            var request = RequestMessageBuilder.CreateRequestBuilder()
                                            .AsMethod(HttpMethodFactory.GetHttpMethod("get"))
                                            .ForUrl(url)
                                            .AtEndpoint(endpoint + "s") //I know there is a better way to add an s but it is 1AM
                                            .Build();

            var response = SendRequest(request).Result;

            var jsonString = GetJsonString(response).Result;

            var deserializedJson = DeserializeJson<IList<T>>(jsonString);

            return deserializedJson;
        }

        public T GetById(string url, string endpoint, int id)
        {
            var request = RequestMessageBuilder.CreateRequestBuilder()
                                            .AsMethod(HttpMethodFactory.GetHttpMethod("get"))
                                            .ForUrl(url)
                                            .AtEndpoint(endpoint)
                                            .WithId(id)
                                            .Build();

            var response = SendRequest(request).Result;

            var jsonString = GetJsonString(response).Result;

            var deserializedJson = DeserializeJson<T>(jsonString);

            return deserializedJson;
        }
        public T Create(string url, string endpoint, string body)
        {
            var request = RequestMessageBuilder.CreateRequestBuilder()
                                            .AsMethod(HttpMethodFactory.GetHttpMethod("post"))
                                            .ForUrl(url)
                                            .AtEndpoint(endpoint)
                                            .WithBody(body)
                                            .Build();

            var response = SendRequest(request).Result;

            var jsonString = GetJsonString(response).Result;

            var deserializedJson = DeserializeJson<T>(jsonString);

            return deserializedJson;
        }

        public void UpdateById(string url, string endpoint, int id, string body)
        {
            var request = RequestMessageBuilder.CreateRequestBuilder()
                                            .AsMethod(HttpMethodFactory.GetHttpMethod("put"))
                                            .ForUrl(url)
                                            .AtEndpoint(endpoint)
                                            .WithId(id)
                                            .WithBody(body)
                                            .Build();

            _ = SendRequest(request).Result;
        }

        public void DeleteById(string url, string endpoint, int id)
        {
            var request = RequestMessageBuilder.CreateRequestBuilder()
                                            .AsMethod(HttpMethodFactory.GetHttpMethod("delete"))
                                            .ForUrl(url)
                                            .AtEndpoint(endpoint)
                                            .WithId(id)
                                            .Build();

            _ = SendRequest(request).Result;
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
