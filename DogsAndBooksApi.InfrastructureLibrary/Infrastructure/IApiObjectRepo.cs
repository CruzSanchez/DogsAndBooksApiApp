using DogsAndBooksApi.CoreLibrary.Models;

namespace DogsAndBooksApi.InfrastructureLibrary.Infrastructure
{
    public interface IApiObjectRepo<T> where T : IApiType
    {
        public IList<T> GetAll(string endpoint);
        public T GetById(string endpoint, int id);
        public T Create(string endpoint, string body);
        public bool UpdateById(string endpoint, int id, string body);
        public bool DeleteById(string endpoint, int id);
    }
}
