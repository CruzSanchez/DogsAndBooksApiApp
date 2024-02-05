using DogsAndBooksApi.CoreLibrary.Models;

namespace DogsAndBooksApi.InfrastructureLibrary.Infrastructure
{
    public interface IApiObjectRepo<T> where T : IApiType
    {
        public IList<T> GetAll(string url, string endpoint);
        public T GetById(string url, string endpoint, int id);
        public T Create(string url, string endpoint, string body);
        public bool UpdateById(string url, string endpoint, int id, string body);
        public bool DeleteById(string url, string endpoint, int id);
    }
}
