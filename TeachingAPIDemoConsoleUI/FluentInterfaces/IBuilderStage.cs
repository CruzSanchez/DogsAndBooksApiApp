namespace TeachingAPIDemoConsoleUI.FluentInterfaces
{
    public interface IBuilderStage
    {
        public HttpRequestMessage Build();
        public IBuilderStage WithId(int? id);
        public IBuilderStage WithBody(string body);
    }
}
