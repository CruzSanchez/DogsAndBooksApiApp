namespace DogsAndBooksApi.InfrastructureLibrary.Factories
{
    public static class HttpClientFactory
    {
        /// <summary>
        /// Creates a newly instantiated HttpClient
        /// </summary>
        /// <returns>A fresh HttpClient</returns>
        public static HttpClient Create()
        {
            return new HttpClient();
        }
    }
}
