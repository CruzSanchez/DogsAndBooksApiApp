namespace DogsAndBooksApi.InfrastructureLibrary.Factories
{
    public static class HttpClientFactory
    {
        public static HttpClient Create()
        {
            return new HttpClient();
        }
    }
}
