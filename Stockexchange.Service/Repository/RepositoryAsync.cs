
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Stockexchange.Service.Paging;
using System.Linq.Expressions;

namespace Stockexchange.Service.Repository
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public RepositoryAsync(DbContext dbContext)
        {

            _dbSet = dbContext.Set<T>();
        }

        #region SingleOrDefault

        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool enableTracking = true,
            bool ignoreQueryFilters = false)
        {
            IQueryable<T> query = _dbSet;

            if (!enableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (ignoreQueryFilters) query = query.IgnoreQueryFilters();

            if (orderBy != null) return await orderBy(query).FirstOrDefaultAsync();
            return await query.FirstOrDefaultAsync();
        }

        #endregion

        #region FirstOrDefault

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool enableTracking = true,
            bool ignoreQueryFilters = false)
        {
            IQueryable<T> query = _dbSet;

            if (!enableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (ignoreQueryFilters) query = query.IgnoreQueryFilters();

            if (orderBy != null) return await orderBy(query).FirstOrDefaultAsync();

            return await query.FirstOrDefaultAsync();
        }

        #endregion

        #region GetListAsync

        public Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 20,
            bool enableTracking = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;
            if (!enableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return orderBy(query).ToPaginateAsync(index, size, 0, cancellationToken);
            return query.ToPaginateAsync(index, size, 0, cancellationToken);
        }


        public Task<IList<T>> GetAllListAsync(Expression<Func<T, bool>> predicate = null,
    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,

    bool enableTracking = true,
    CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;
            if (!enableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return orderBy(query).ToGetAllAsync(cancellationToken);
            return query.ToGetAllAsync(cancellationToken);
        }

        public Task<IPaginate<TResult>> GetListAsync<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 20,
            bool enableTracking = true,
            CancellationToken cancellationToken = default,
            bool ignoreQueryFilters = false)
            where TResult : class
        {
            IQueryable<T> query = _dbSet;

            if (!enableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (ignoreQueryFilters) query = query.IgnoreQueryFilters();

            if (orderBy != null)
                return orderBy(query).Select(selector).ToPaginateAsync(index, size, 0, cancellationToken);

            return query.Select(selector).ToPaginateAsync(index, size, 0, cancellationToken);
        }

        #endregion

        #region Insert Functions

        public virtual ValueTask<EntityEntry<T>> InsertAsync(T entity, CancellationToken cancellationToken = default)
        {

            _dbSet.AsNoTracking<T>();
            return _dbSet.AddAsync(entity, cancellationToken);
        }


        public virtual Task InsertAsync(params T[] entities)
        {
            return _dbSet.AddRangeAsync(entities);
        }


        public virtual Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            return _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        #endregion

        #region Update Functions

        public virtual EntityEntry<T> Update(T entity)
        {
            _dbSet.Attach(entity);
            //_dbSet.Entry(entity).State = EntityState.Modified;
            return _dbSet.Update(entity);
        }

        public virtual void Update(T[] entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public virtual void Update(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }
        #endregion

        #region Delete Functions
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(params T[] entities)
        {
            _dbSet.RemoveRange(entities);
        }


        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        #endregion

        //#region calling SP
        //public IEnumerable<T> SQLQuery<T>(string sql, params object[] parameters) => (IList<T>)_dbSet.FromSqlRaw(sql, parameters).AsNoTracking().ToList();
        //#endregion
    }
}