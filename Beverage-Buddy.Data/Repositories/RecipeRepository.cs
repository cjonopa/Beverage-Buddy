using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace Beverage_Buddy.Data.Repositories
{
    public class RecipeRepository : IRepository<Recipe, int>
    {
        private readonly BeverageBuddyDbContext db;
        private readonly ILogger<RecipeRepository> logger;

        public RecipeRepository(BeverageBuddyDbContext db, ILogger<RecipeRepository> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        public void Add(Recipe item)
        {
            try
            {
                logger.LogInformation("Recipe : Add was called.");

                db.Recipes.Add(item);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to add item: {ex}");
            }
        }

        public void Delete(int id)
        {
            try
            {
                logger.LogInformation("Recipe : Delete was called.");

                var recipe = db.Recipes.Find(id);
                db.Recipes.Remove(recipe);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete item: {ex}");
            }
        }

        public Recipe Get(int id)
        {
            try
            {
                logger.LogInformation("Recipe : Get was called.");

                return db.Recipes.FirstOrDefault(r => r.Id == id);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get item: {ex}");
                return null;
            }
        }

        public async Task<ICollection<Recipe>> GetAll()
        {
            try
            {
                logger.LogInformation("Recipe : GetAll was called.");

                var recipe = 
                    await db.Recipes.OrderBy(r => r.Name).ToListAsync();
                return recipe;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get all recipes: {ex}");
                return null;
            }
        }

        public void Update(Recipe item)
        {
            try
            {
                logger.LogInformation("Recipe : Update was called.");

                var entry = db.Entry(item);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update item: {ex}");
            }
        }
    }
}
