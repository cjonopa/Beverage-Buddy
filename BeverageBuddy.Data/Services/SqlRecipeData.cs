using BeverageBuddy.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageBuddy.Data.Services
{
    public class SqlRecipeData : IRecipeData
    {
        private readonly BeverageBuddyDbContext db;

        public SqlRecipeData(BeverageBuddyDbContext db)
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
            entry.State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
