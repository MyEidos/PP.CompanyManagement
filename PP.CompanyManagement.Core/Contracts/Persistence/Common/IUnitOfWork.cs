using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Core.Interfaces.Persistence.Common
{
    /// <summary>
    /// Defines functionality of Unit of Work.
    /// </summary>
    /// <typeparam name="T">Type of Data context.</typeparam>
    /// <seealso cref="System.IDisposable" />
    public interface IUnitOfWork<T> : IDisposable
        where T : IDataContext
    {
        /// <summary>
        /// Gets the data context.
        /// </summary>
        T DataContext { get; }

        /// <summary>
        /// Saves all changes within unit of work.
        /// </summary>
        /// <returns>The number of objects written to the underlying database.</returns>
        /// <exception cref="System.ApplicationException">Validation Errors collection.</exception>
        /// <exception cref="System.Exception">Updating database error.</exception>
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}
