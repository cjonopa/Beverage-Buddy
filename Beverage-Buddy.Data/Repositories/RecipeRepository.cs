using Microsoft.Extensions.Logging;
using System;
using System.Data;
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

        /// <summary>
        /// Adds the specified recipe to the database.
        /// </summary>
        /// <param name="item">A <see cref="Recipe"/> to be added.</param>
        /// <exception cref="DbUpdateException">Unable to add item due to missing requirements.</exception>
        public void Add(Recipe item)
        {
            try
            {
                logger.LogInformation("Recipe : Add was called.");

                db.Recipes.Add(item);
            }
            catch (DbUpdateException ex)
            {
                logger.LogError($"Failed to add item: {ex}");
                throw new DbUpdateException("Unable to add item due to missing requirements.", ex);
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

        public async Task<bool> SaveAllAsync()
        {
            return await db.SaveChangesAsync() > 0;
        }

        public bool CheckForExisting(string name)
        {
            return db.Recipes.Any(m => m.Name == name); 
        }

        public Recipe Get(int id)
        {
            try
            {
                logger.LogInformation("Recipe : Get was called.");

                return db.Recipes                
                    .Include(r => r.Ingredients)
                    .FirstOrDefault(r => r.Id == id);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get item: {ex}");
                return null;
            }
        }

        public async Task<ICollection<Recipe>> GetAllAsync()
        {
            try
            {
                logger.LogInformation("Recipe : GetAllAsync was called.");

                var recipe = 
                    await db.Recipes
                        .OrderBy(r => r.Name)                    
                        .Include(r => r.Ingredients)                        
                        .ToListAsync();

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
