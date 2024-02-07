namespace DogsAndBooksApi.InfrastructureLibrary.Factories
{
    internal static class HttpMethodFactory
    {
        /// <summary>
        /// Factory for supplying a http method
        /// </summary>
        /// <param name="method">The type of method</param>
        /// <returns>A HttpMethod - GET, POST, PUT, DELETE</returns>
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
