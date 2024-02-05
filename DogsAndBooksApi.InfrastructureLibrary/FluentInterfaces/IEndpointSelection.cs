namespace DogsAndBooksApi.InfrastructureLibrary.FluentInterfaces
{
    public interface IEndpointSelection
    {
        public IBuilderStage AtEndpoint(string endpoint);
    }
}
