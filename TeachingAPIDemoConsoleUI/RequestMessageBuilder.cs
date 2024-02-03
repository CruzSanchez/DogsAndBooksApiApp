using TeachingAPIDemoConsoleUI.FluentInterfaces;

namespace TeachingAPIDemoConsoleUI
{
    internal class RequestMessageBuilder : IMethodSelection, IUrlSelection, IEndpointSelection, IBuilderStage
    {
        private HttpMethod? _method = null;
        private string _url = string.Empty;
        private string _endpoint = string.Empty;
        private int? _id = null;
        private string? _body = null;

        private RequestMessageBuilder() { }

        /// <summary>
        /// Entry point to the fluent API. This starts the chain of method calls.
        /// </summary>
        /// <returns>IMethodSelection</returns>
        public static IMethodSelection CreateRequestBuilder()
        {
            return new RequestMessageBuilder();
        }

        /// <summary>
        /// Sets the HttpRequestMethod to the appropriate type.
        /// </summary>
        /// <param name="method">Appropriate parameters to pass in are 'get, post, put, delete'</param>
        /// <returns>IUrlSelection</returns>
        public IUrlSelection AsMethod(HttpMethod method)
        {
            _method = method;
            return this;
        }

        /// <summary>
        /// Sets the URL for the request
        /// </summary>
        /// <param name="url">Pass in the Base URL that you want to go to</param>
        /// <returns>IEndpointSelection</returns>
        public IEndpointSelection ForUrl(string url)
        {
            _url = url;
            return this;
        }

        /// <summary>
        /// Sets the specified endpoint to go to. 
        /// </summary>
        /// <param name="endpoint">
        /// Accepted parameters are dog, dogs, dog/{id}, dog/random, book, books, book/{id}, book/random
        /// </param>
        /// <returns>IBuilderStage</returns>
        public IBuilderStage AtEndpoint(string endpoint)
        {
            _endpoint = endpoint;
            return this;
        }

        /// <summary>
        /// OPTIONAL method! Call this method if you are using an endpoint that requires adding an id to the URL. 
        /// You can call Build after this if you want
        /// </summary>
        /// <param name="id">The id of the object you are wanting to select.</param>
        /// <returns>IBuilderStage</returns>
        public IBuilderStage WithId(int? id = null)
        {
            if (id is null)
            {
                return this;
            }

            _id = id;
            return this;
        }

        /// <summary>
        /// OPTIONAL method! Call this method if you need to supply a request body
        /// </summary>
        /// <param name="body">The object you want to send to the server</param>
        /// <returns>IBuilderStage</returns>
        public IBuilderStage WithBody(string? body = null)
        {
            if (_body is null)
            {
                return this;
            }

            _body = body;
            return this;
        }

        public HttpRequestMessage Build()
        {
            if (_body is not null)
                return new HttpRequestMessage(_method, $"{_url}/{_endpoint}/{_id}") { Content = new StringContent(_body) };
            else
                return new HttpRequestMessage(_method, $"{_url}/{_endpoint}/{_id}");
        }

    }
}
