namespace DogsAndBooksApi.InfrastructureLibrary.FluentInterfaces
{
    public interface IMethodSelection
    {
        public IUrlSelection AsMethod(HttpMethod method);
    }
}
