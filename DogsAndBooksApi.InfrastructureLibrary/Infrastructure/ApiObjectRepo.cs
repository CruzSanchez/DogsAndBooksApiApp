using DogsAndBooksApi.CoreLibrary.Models;
using DogsAndBooksApi.InfrastructureLibrary.Factories;
using Newtonsoft.Json;

namespace DogsAndBooksApi.InfrastructureLibrary.Infrastructure
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

        public bool UpdateById(string url, string endpoint, int id, string body)
        {
            var request = RequestMessageBuilder.CreateRequestBuilder()
                                            .AsMethod(HttpMethodFactory.GetHttpMethod("put"))
                                            .ForUrl(url)
                                            .AtEndpoint(endpoint)
                                            .WithId(id)
                                            .WithBody(body)
                                            .Build();

            return SendRequest(request).Result.IsSuccessStatusCode;
        }

        public bool DeleteById(string url, string endpoint, int id)
        {
            var request = RequestMessageBuilder.CreateRequestBuilder()
                                            .AsMethod(HttpMethodFactory.GetHttpMethod("delete"))
                                            .ForUrl(url)
                                            .AtEndpoint(endpoint)
                                            .WithId(id)
                                            .Build();

            return SendRequest(request).Result.IsSuccessStatusCode;
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
