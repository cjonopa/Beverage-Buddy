using System.Collections.Generic;
using Beverage_Buddy.Data.Models;

namespace Beverage_Buddy.Data.Services
{
    public interface IRecipeRepository
    {
        IEnumerable<Recipe> GetAll();
        Recipe Get(int id);
        void Add(Recipe recipe);
        void Update(Recipe recipe);
        void Delete(int id);

        bool SaveAll();
    }
}
