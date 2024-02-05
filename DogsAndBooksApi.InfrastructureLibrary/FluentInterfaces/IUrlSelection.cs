namespace DogsAndBooksApi.InfrastructureLibrary.FluentInterfaces
{
    public interface IUrlSelection
    {
        public IEndpointSelection ForUrl(string url);
    }
}
