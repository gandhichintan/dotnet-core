using TestApp.DomainModel.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestApp.Repository
{
    public interface IDataRepository<T>: IDisposable where T : class 
    {
        /// <summary>
        /// Application db context
        /// </summary>
        TestAppTestDbContext TestAppTestDbContext { get; }

        /// <summary>
        /// Add the new entity into the datacontext
        /// </summary>
        /// <param name="entity"></param>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Update the entity into the datacontext.
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        ///   Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate"> Specified a filter </param>
        Task<IEnumerable<T>> FetchAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///   Find object by specified expression.
        /// </summary>
        /// <param name="predicate"> </param>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Fetches all the item from the datacontext.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        ///   Gets the object(s) is exists in database by specified filter.
        /// </summary>
        /// <param name="predicate"> Specified the filter expression </param>
        bool Contains(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Save changes into the database.
        /// </summary>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Save changes into the database.
        /// </summary>
        int SaveChanges();


    }

}