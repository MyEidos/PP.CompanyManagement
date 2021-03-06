using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PP.CompanyManagement.Core.Interfaces.Persistence.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Persistence.Common
{
    /// <summary>
    /// Implements Unit of Work.
    /// </summary>
    /// <typeparam name="T">The type of data context.</typeparam>
    public class UnitOfWork<T> : IUnitOfWork<T>
        where T : IDataContext
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly DbContext context;

        /// <summary>
        /// The disposed flag.
        /// </summary>
        private bool disposed;
        private int transactionsCount = 0;

        private IDbContextTransaction transaction;

        public bool TransactionOpened
        {
            get
            {
                return this.transaction != null;
            }
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if(this.transaction == null) this.transaction = await this.context.Database.BeginTransactionAsync(cancellationToken);

            this.transactionsCount++;
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (this.transaction == null)
            {
                throw new InvalidOperationException("There is no open transaction to be committed.");
            }
            else if (this.transactionsCount == 1)
            {
                await this.transaction.CommitAsync();

                this.transaction.Dispose();
                this.transaction = null;
            }

            this.transactionsCount--;
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            await this.transaction.RollbackAsync(cancellationToken);
            this.transaction.Dispose();
            this.transaction = null;
            this.transactionsCount = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{T}" /> class.
        /// </summary>
        /// <param name="context">The data context.</param>
        /// <exception cref="ArgumentException">Entity.DbContext instance is expected as a dbContext parameter.</exception>
        /// <exception cref="System.ArgumentException">Entity Database Context instance is expected as a database Context parameter.</exception>
        public UnitOfWork(T context)
        {
            this.context = context as DbContext;

            if (context == null)
            {
                throw new ArgumentException("Entity.DbContext instance is expected as a dbContext parameter.");
            }

            this.context.ChangeTracker.LazyLoadingEnabled = false;
        }

        /// <summary>
        /// Gets the data context.
        /// </summary>
        public T DataContext
        {
            get
            {
                return (T)(object)this.context;
            }
        }

        /// <summary>
        /// Saves all changes within unit of work.
        /// </summary>
        /// <returns>The number of objects written to the underlying database.</returns>
        /// <exception cref="System.ApplicationException">Validation Errors collection.</exception>
        /// <exception cref="System.Exception">Updating database error.</exception>
        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await this.context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                if (this.TransactionOpened)
                {
                    this.transaction.Rollback();
                    this.transaction.Dispose();
                }

                this.context.Dispose();
            }

            this.disposed = true;
        }
    }
}
