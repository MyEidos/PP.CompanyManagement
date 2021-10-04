using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Core.Interfaces.Persistence.Common
{
    /// <summary>
    ///  Defines functionality of the base repository.
    /// </summary>
    /// <typeparam name="T">The type of Entity.</typeparam>
    public interface IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Adds a new entity to database Set.
        /// </summary>
        /// <param name="entity">The Entity instance.</param>
        void Insert(T entity);

        /// <summary>
        /// Adds the given collection of entities into context underlying the set with
        /// each entity being put into the Added state such that it will be inserted
        /// into the database when SaveChanges is called.
        /// </summary>
        /// <param name="entities">The collection of entities to insert.</param>
        void InsertRange(IEnumerable<T> entities);

        /// <summary>
        /// Updates  existing entity.
        /// </summary>
        /// <param name="entity">The Entity instance.</param>
        void Update(T entity);

        /// <summary>
        /// Updates the given collection of entities.
        /// </summary>
        /// <param name="entities">The collection of entities to update.</param>
        void UpdateRange(IEnumerable<T> entities);

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="entity">The Entity instance.</param>
        void Delete(T entity);

        /// <summary>
        /// Removes the given collection of entities from the context underlying the
        /// set with each entity being put into the Deleted state such that it will be
        /// deleted from the database when SaveChanges is called.
        /// </summary>
        /// <param name="entities">The collection of entities to delete.</param>
        void DeleteRange(IEnumerable<T> entities);

        /// <summary>
        /// Find entity by key.
        /// </summary>
        /// <param name="key">Primary key value.</param>
        /// <param name="includeInactive">if set to <c>true</c> include inactive.</param>
        /// <returns>
        /// The Entity instance.
        /// </returns>
        Task<T> FindAsync(object key, bool includeInactive = false);

        /// <summary>
        /// Select all entities.
        /// </summary>
        /// <param name="includeInactive">if set to <c>true</c> include inactive.</param>
        /// <returns>
        /// List of entities.
        /// </returns>
        Task<IEnumerable<T>> SelectAllAsync(bool includeInactive = false);
    }
}
