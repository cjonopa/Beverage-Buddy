using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beverage_Buddy.Data.Repositories
{

    /// <summary>IRepository is an interface that contracts out CRUD methods for the database.</summary>
    /// <typeparam name="T">the type of object to be used for the repository</typeparam>
    /// <typeparam name="TK">The type of the key to be used for the repository</typeparam>
    public interface IRepository<T, in TK> where T : class
    {
        Task<ICollection<T>> GetAllAsync();
        /// <summary>
        /// Gets the specified identifier of the <see cref="T"/>.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="T"/>.</returns>
        Task<T> GetAsync(TK id);
        /// <summary>
        /// Adds the specified <see cref="T"/>.
        /// </summary>
        /// <param name="item">A <see cref="T"/>.</param>
        T Add(T item);
        /// <summary>
        /// Updates the specified <see cref="T"/>.
        /// </summary>
        /// <param name="item">A <see cref="T"/>.</param>
        T Update(T item);
        /// <summary>
        /// Deletes the specified identifier of the <see cref="T"/>.
        /// </summary>
        /// <param name="id">The identifier.</param>
        T Delete(TK id);
        Task<bool> SaveAllAsync();
        bool CheckForExisting(string name);
    }
}
