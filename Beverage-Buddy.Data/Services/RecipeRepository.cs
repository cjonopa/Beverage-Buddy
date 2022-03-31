using Beverage_Buddy.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Beverage_Buddy.Data.Services
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly BeverageBuddyDbContext db;

        public RecipeRepository(BeverageBuddyDbContext db)
        {
            this.db = db;
        }

        public void Add(Recipe recipe)
        {
            db.Recipes.Add(recipe);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var recipe = db.Recipes.Find(id);
            db.Recipes.Remove(recipe);
            db.SaveChanges();
        }

        public Recipe Get(int id)
        {
            return db.Recipes.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Recipe> GetAll()
        {
            return from r in db.Recipes
                   orderby r.Name
                   select r;
        }

        public void Update(Recipe recipe)
        {
            var entry = db.Entry(recipe);
            db.SaveChanges();
        }

        public bool SaveAll()
        {
            return db.SaveChanges() > 0;
        }
    }
}
