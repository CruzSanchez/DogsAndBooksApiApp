namespace DogsAndBooksApi.InfrastructureLibrary.Factories
{
    internal static class HttpMethodFactory
    {
        public static HttpMethod GetHttpMethod(string method)
        {
            switch (method.ToLower())
            {
                case "get":
                case "read":
                    return HttpMethod.Get;

                case "post":
                case "create":
                    return HttpMethod.Post;

                case "put":
                case "update":
                    return HttpMethod.Put;

                case "delete":
                    return HttpMethod.Delete;

                default:
                    return HttpMethod.Get;
            }
        }
    }
}
