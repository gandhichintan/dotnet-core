using TestApp.DomainModel.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestApp.Repository
{
    public class DataRepository<T> : IDataRepository<T> where T : class
    {
        private TestAppTestDbContext _TestAppTestDbContext;
        private DbSet<T> _dbSet;

        public TestAppTestDbContext TestAppTestDbContext
        {
            get
            {
                return _TestAppTestDbContext;
            }
        }

        public DataRepository(TestAppTestDbContext context)
        {
            _TestAppTestDbContext = context;
            _dbSet = _TestAppTestDbContext.Set<T>();
        }

        /// <summary>
        /// Adds entity into the context.
        /// </summary>
        /// <param name="entity"></param>
        public async Task<T> AddAsync(T entity)
        {
            var newEntity = await _dbSet.AddAsync(entity);
            return newEntity.Entity;
        }

        /// <summary>
        /// Update model entry
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            var entry = _TestAppTestDbContext.Entry(entity);
            _dbSet.Attach(entity);
            entry.State = EntityState.Modified;
        }

        /// <summary>
        /// Method fetches the IQueryable based on expression.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<T> Fetch(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }

        /// <summary>
        /// Method fetches the IQueryable based on expression.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FetchAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Method fetches the first or default item from the datacontext based on the the supplied function.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Method Fetches all the data before executing query.
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        /// <summary>
        /// Method Checks whether dbset has anything entity in it or not.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }

        /// <summary>
        /// Method save the changes into the context
        /// </summary>
        public async Task<int> SaveChangesAsync()
        {
            return await _TestAppTestDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Method save the changes into the context
        /// </summary>
        public int SaveChanges()
        {
            return _TestAppTestDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _TestAppTestDbContext.Dispose();
        }
    }
}