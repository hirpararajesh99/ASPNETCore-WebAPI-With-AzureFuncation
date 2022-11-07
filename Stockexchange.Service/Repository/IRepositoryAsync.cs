using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Stockexchange.Service.Paging;
using System.Linq.Expressions;

namespace Stockexchange.Service.Repository
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool enableTracking = true,
            bool ignoreQueryFilters = false);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool enableTracking = true,
            bool ignoreQueryFilters = false);

        Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 20,
            bool enableTracking = true,
            CancellationToken cancellationToken = default);

        Task<IList<T>> GetAllListAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool enableTracking = true,
            CancellationToken cancellationToken = default);

        #region Insert Functions

        ValueTask<EntityEntry<T>> InsertAsync(T entity,
            CancellationToken cancellationToken = default);

        Task InsertAsync(params T[] entities);

        Task InsertAsync(IEnumerable<T> entities,
            CancellationToken cancellationToken = default);

        #endregion

        #region Delete Functions
        void Delete(T entity);

        void Delete(params T[] entities);

        void Delete(IEnumerable<T> entities);
        #endregion


        #region Update
        EntityEntry<T> Update(T entity);
        void Update(T[] entities);
        void Update(IEnumerable<T> entities);
        #endregion

        //#region calling SP
        //IEnumerable<T> SQLQuery<T>(string sql, params object[] parameters);
        //#endregion
    }
}