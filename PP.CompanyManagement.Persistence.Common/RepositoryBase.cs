using Microsoft.EntityFrameworkCore;
using PP.CompanyManagement.Core.Entities.Shared;
using PP.CompanyManagement.Core.Interfaces.Persistence.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Persistence.Common
{
    /// <summary>
    /// Implements the base repository.
    /// </summary>
    /// <typeparam name="T">The type of Entity.</typeparam>
    /// <typeparam name="C">The type of data context.</typeparam>
    public abstract class RepositoryBase<T, C> : IRepositoryBase<T>
        where T : EntityBase
        where C : IDataContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{T, C}"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        protected RepositoryBase(IUnitOfWork<C> unitOfWork)
        {
            this.DataContext = unitOfWork.DataContext as DbContext;
        }

        /// <summary>
        /// Gets the data context.
        /// </summary>
        protected DbContext DataContext
        {
            get;
            private set;
        }

        /// <summary>
        /// Adds a new entity to database Set.
        /// </summary>
        /// <param name="entity">The Entity instance.</param>
        public void Insert(T entity)
        {
            this.DataContext.Set<T>().Add(entity);
        }

        /// <summary>
        /// Adds the given collection of entities into context underlying the set with
        /// each entity being put into the Added state such that it will be inserted
        /// into the database when SaveChanges is called.
        /// </summary>
        /// <param name="entities">The collection of entities to insert.</param>
        public void InsertRange(IEnumerable<T> entities)
        {
            this.DataContext.Set<T>().AddRange(entities);
        }

        /// <summary>
        /// Updates existing entity.
        /// </summary>
        /// <param name="entity">The Entity instance.</param>
        public void Update(T entity)
        {
            var entry = this.DataContext.Entry<T>(entity);

            if (entry != null)
            {
                if (entry.State == EntityState.Detached)
                {
                    this.DataContext.Set<T>().Attach(entity);
                }

                entry.State = EntityState.Modified;
            }
        }

        /// <summary>
        /// Updates the given collection of entities.
        /// </summary>
        /// <param name="entities">The collection of entities to update.</param>
        public void UpdateRange(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                this.Update(entity);
            }
        }

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="entity">The Entity instance.</param>
        public void Delete(T entity)
        {
            this.DataContext.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Removes the given collection of entities from the context underlying the
        /// set with each entity being put into the Deleted state such that it will be
        /// deleted from the database when SaveChanges is called.
        /// </summary>
        /// <param name="entities">The collection of entities to delete.</param>
        public void DeleteRange(IEnumerable<T> entities)
        {
            this.DataContext.Set<T>().RemoveRange(entities);
        }

        /// <summary>
        /// Find entity by key.
        /// </summary>
        /// <param name="key">Primary key value.</param>
        /// <returns>The Entity instance.</returns>
        public async Task<T> FindAsync(object key, bool includeInactive = false)
        {
            var query = this.Select().AsQueryable();

            if (includeInactive)
            {
                query = query.IgnoreQueryFilters();
            }

            return await query.SingleOrDefaultAsync(this.GetByIdExpression(key));
        }

        /// <summary>
        /// Select all entities.
        /// </summary>
        /// <returns>List of entities.</returns>
        public async Task<IEnumerable<T>> SelectAllAsync(bool includeInactive = false)
        {
            var query = this.Select().AsQueryable();

            if (includeInactive)
            {
                query = query.IgnoreQueryFilters();
            }

            return await query.ToArrayAsync();
        }

        /// <summary>
        /// Select entities.
        /// </summary>
        /// <returns>List of entities.</returns>
        protected DbSet<T> Select()
        {
            return this.DataContext.Set<T>();
        }

        /// <summary>
        /// Select entities with filter.
        /// </summary>
        /// <param name="filter">Filter expression.</param>
        /// <returns>Collection of entities.</returns>
        protected IQueryable<T> Where(Expression<Func<T, bool>> filter)
        {
            return this.Select().Where(filter);
        }

        private Expression<Func<T, bool>> GetByIdExpression(object id)
        {
            // TODO: optimise perfomance?
            var entityType = this.DataContext.Model.FindEntityType(typeof(T));
            var primaryKey = entityType.FindPrimaryKey();

            if (primaryKey.Properties.Count != 1)
                throw new NotSupportedException("Only a single primary key is supported.");

            var pkPropertyName = primaryKey.Properties[0].Name;
            var pkPropertyType = primaryKey.Properties[0].ClrType;

            var param = Expression.Parameter(typeof(T), "p");
            var exp = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(
                    Expression.Property(param, pkPropertyName),
                    Expression.Constant(id, pkPropertyType)
                ),
                param
            );
            return exp;
        }
    }
}
