
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Stockexchange.Service.Repository;

namespace fieldflake_coreflow.Services.GloContextRepository
{


    public interface IUnitofWorkRepository : IDisposable
    {
        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class;
        int Commit();
        Task<int> CommitAsync();
        Task<int> CommitAsyncWithTransaction();
        Task Refresh();

        IDbContextTransaction dbContextTransaction { get; set; }
    }

    public interface IUnitofWorkRepository<TContext> : IUnitofWorkRepository where TContext : DbContext
    {
        TContext Context { get; }
    }
}
