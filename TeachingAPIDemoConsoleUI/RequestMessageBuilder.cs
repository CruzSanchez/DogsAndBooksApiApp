using TeachingAPIDemoConsoleUI.FluentInterfaces;

namespace TeachingAPIDemoConsoleUI
{
    internal class RequestMessageBuilder : IMethodStage, IUrlSelection, IEndpointSelection, IBuilderStage
    {
        private HttpMethod _method;
        private string _url;
        private string _endpoint;

        private RequestMessageBuilder() { }

        public static IMethodStage CreateRequest()
        {
            return new RequestMessageBuilder();
        }

        public IUrlSelection AsMethod(HttpMethod method)
        {
            _method = method;
            return this;
        }

        public IEndpointSelection ForUrl(string url)
        {
            _url = url;
            return this;
        }

        public IBuilderStage AtEndpoint(string endpoint)
        {
            _endpoint = endpoint;
            return this;
        }

        public HttpRequestMessage Build()
        {
            return new HttpRequestMessage(_method, $"{_url}/{_endpoint}");
        }
    }
}
