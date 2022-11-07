using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Stockexchange.Service;
using Stockexchange.Service.Repository;

namespace fieldflake_coreflow.Services.GloContextRepository
{


    public class UnitofWorkRepository<TContext> : IRepositoryFactory, IUnitofWorkRepository<TContext>
        where TContext : DbContext, IDisposable
    {
        private Dictionary<(Type type, string name), object> _repositories;

        public UnitofWorkRepository(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class
        {
            return (IRepositoryAsync<TEntity>)GetOrAddRepository(typeof(TEntity),
                new RepositoryAsync<TEntity>(Context));
        }

        public TContext Context { get; }
        public IDbContextTransaction dbContextTransaction { get; set; }
        public int Commit()
        {

            return Context.SaveChanges();
        }
        public async Task<int> CommitAsync()
        {
            try
            {
                await Refresh();
                // Context.Entry(Context.Set<TEntity>()).State = EntityState.Detached;
                return await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Error in qruery " + ex.Message);
            }

        }

        public async Task<int> CommitAsyncWithTransaction()
        {
            try
            {
               
                try
                {

                    if (dbContextTransaction == null)
                        dbContextTransaction = Context.Database.BeginTransaction();
                    
                    int result = await Context.SaveChangesAsync();
                    //  transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Error in qruery " + ex.Message);
                }
               

            }
            catch (Exception ex)
            {
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Error in qruery " + ex.Message);
            }

        }

        public async Task Refresh()
        {
            try
            {
                Context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
            }
            catch (Exception ex)
            {
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Error in qruery " + ex.Message);
            }

        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        internal object GetOrAddRepository(Type type, object repo)
        {
            _repositories ??= new Dictionary<(Type type, string Name), object>();

            if (_repositories.TryGetValue((type, repo.GetType().FullName), out var repository)) return repository;
            _repositories.Add((type, repo.GetType().FullName), repo);
            return repo;
        }
    }
}
