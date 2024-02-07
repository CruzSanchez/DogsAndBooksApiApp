using DogsAndBooksApi.CoreLibrary.Models;
using DogsAndBooksApi.InfrastructureLibrary.Factories;
using Newtonsoft.Json;

namespace DogsAndBooksApi.InfrastructureLibrary.Infrastructure
{
    public class ApiObjectRepo<T>(HttpClient httpClient) : IApiObjectRepo<T> where T : IApiType
    {
        private readonly HttpClient _httpClient = httpClient;
        private const string BASE_URL = "http://localhost:5000";

        /// <summary>
        /// This method will perform a "Get All" request from the dogs and books api. 
        /// It builds a request message with the RequestMessageBuilder fluent api and passes the correct endpoint in.
        /// </summary>
        /// <param name="endpoint">The endpoint to "get all" the data from. This should always either be Dog or Book</param>
        /// <typeparam name="T">The data this method shall return, Dog or Book</typeparam>
        /// <returns>An IList of the specified type <typeparamref name="T"/></returns>
        public IList<T> GetAll(string endpoint)
        {
            var getAllEndpoint = endpoint.EndsWith("s") ? endpoint : $"{endpoint}s";

            var request = RequestMessageBuilder.CreateRequestBuilder()
                                            .AsMethod(HttpMethodFactory.GetHttpMethod("get"))
                                            .ForUrl(BASE_URL)
                                            .AtEndpoint(getAllEndpoint) //I know there is a better way to add an s but it is 1AM
                                            .Build();

            var response = SendRequest(request).Result;

            var jsonString = GetJsonString(response).Result;

            var deserializedJson = DeserializeJson<IList<T>>(jsonString);

            return deserializedJson;
        }

        /// <summary>
        /// This method will perform a "Get Single" request based on the endpoint and id passed in.
        /// </summary>
        /// <param name="endpoint">The endpoint to the data from. This should always either be Dog or Book.</param>
        /// <param name="id">The id of the object to request.</param>
        /// <typeparam name="T">The data this method shall return, Dog or Book</typeparam>
        /// <returns>The data from the api call. <typeparamref name="T"/> will always be a Dog or Book type.</returns>
        public T GetById(string endpoint, int id)
        {
            var request = RequestMessageBuilder.CreateRequestBuilder()
                                            .AsMethod(HttpMethodFactory.GetHttpMethod("get"))
                                            .ForUrl(BASE_URL)
                                            .AtEndpoint(endpoint)
                                            .WithId(id)
                                            .Build();

            var response = SendRequest(request).Result;

            var jsonString = GetJsonString(response).Result;

            var deserializedJson = DeserializeJson<T>(jsonString);

            return deserializedJson;
        }

        /// <summary>
        /// Creates a Dog or Book type object
        /// </summary>
        /// <param name="endpoint">The api endpoint to send the request to</param>
        /// <param name="body">The data for the new object</param>
        /// <returns>The object that was just created</returns>
        public T Create(string endpoint, string body)
        {
            var request = RequestMessageBuilder.CreateRequestBuilder()
                                            .AsMethod(HttpMethodFactory.GetHttpMethod("post"))
                                            .ForUrl(BASE_URL)
                                            .AtEndpoint(endpoint)
                                            .WithBody(body)
                                            .Build();

            var response = SendRequest(request).Result;

            var jsonString = GetJsonString(response).Result;

            var deserializedJson = DeserializeJson<T>(jsonString);

            return deserializedJson;
        }

        /// <summary>
        /// Updates a specified object based on the ID.
        /// </summary>
        /// <param name="endpoint">The api endpoint to send the request to.</param>
        /// <param name="id">The id of the object to update.</param>
        /// <param name="body">The object to update the original with.</param>
        /// <returns><c>true</c> if the request returns a success status code or <c>false</c> 
        /// if the request does not return a success status code.</returns>
        public bool UpdateById(string endpoint, int id, string body)
        {
            var request = RequestMessageBuilder.CreateRequestBuilder()
                                            .AsMethod(HttpMethodFactory.GetHttpMethod("put"))
                                            .ForUrl(BASE_URL)
                                            .AtEndpoint(endpoint)
                                            .WithId(id)
                                            .WithBody(body)
                                            .Build();

            return SendRequest(request).Result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Deletes a specified object based on the ID.
        /// </summary>
        /// <param name="endpoint">The api endpoint to send the request to.</param>
        /// <param name="id">The id of the object to delete.</param>
        /// <returns><c>true</c> if the request returns a success status code or <c>false</c> 
        /// if the request does not return a success status code.</returns>
        public bool DeleteById(string endpoint, int id)
        {
            var request = RequestMessageBuilder.CreateRequestBuilder()
                                            .AsMethod(HttpMethodFactory.GetHttpMethod("delete"))
                                            .ForUrl(BASE_URL)
                                            .AtEndpoint(endpoint)
                                            .WithId(id)
                                            .Build();

            return SendRequest(request).Result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Sends a HTTP request over the network to retrieve api data.
        /// </summary>
        /// <param name="request">The request to send</param>
        /// <returns>A Task containing the HTTP response message</returns>
        private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);
            return response;
        }

        /// <summary>
        /// Transforms a http response into a string for further refinement
        /// </summary>
        /// <param name="response">The response message from an api call</param>
        /// <returns>The string representation of the json data</returns>
        private async Task<string> GetJsonString(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        /// <summary>
        /// Deserializes the json into a specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the json data into</typeparam>
        /// <param name="json">The json string to deserialize</param>
        /// <returns>The deserialized object from the Json string</returns>
        private T DeserializeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
