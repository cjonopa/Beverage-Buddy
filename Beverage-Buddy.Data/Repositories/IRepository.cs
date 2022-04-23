using System.Collections.Generic;

namespace Beverage_Buddy.Data.Repositories
{

    /// <summary>IRepository is an interface that contracts out CRUD methods for the database.</summary>
    /// <typeparam name="T">the type of object to be used for the repository</typeparam>
    /// <typeparam name="TK">The type of the key to be used for the repository</typeparam>
    public interface IRepository<T, in TK>
    {
        IEnumerable<T> GetAll();
        T Get(TK id);
        void Add(T item);
        void Update(T item);
        void Delete(TK id);
    }
}
