namespace DogsAndBooksApi.ConsoleUI.FluentInterfaces
{
    public interface IMethodSelection
    {
        public IUrlSelection AsMethod(HttpMethod method);
    }
}
