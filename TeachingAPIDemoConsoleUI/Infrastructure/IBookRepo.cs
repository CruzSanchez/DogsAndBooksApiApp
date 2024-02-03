namespace TeachingAPIDemoConsoleUI.Infrastructure
{
    internal interface IBookRepo
    {
        public IList<Book> GetAllBooks(string url, string endpoint);
        public Book GetBookById(string url, string endpoint, int id);
    }
}
