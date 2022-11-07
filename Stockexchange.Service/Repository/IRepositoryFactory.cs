

namespace Stockexchange.Service.Repository
{
    public interface IRepositoryFactory
    {
        IRepositoryAsync<T> RepositoryAsync<T>() where T : class;
    }
}