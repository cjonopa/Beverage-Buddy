using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
                throw new Exception("Error retrieving data from repository", ex);
            }
        }

        public async Task<Recipe> GetAsync(int id)
        {
            try
            {
                logger.LogInformation("Recipe : GetAsync was called.");

                var recipe = db.Recipes
                    .Include(r => r.Ingredients)
                    .Where(r => r.Id == id);

                return await recipe.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get item: {ex}");
                throw new Exception("Error retrieving data from repository", ex);
            }
        }

        /// <summary>
        /// Adds the specified recipe to the database.
        /// </summary>
        /// <param name="item">A <see cref="Recipe"/> to be added.</param>
        /// <exception cref="DbUpdateException">Unable to add item due to missing requirements.</exception>
        public Recipe Add(Recipe item)
        {
            try
            {
                logger.LogInformation("Recipe : Add was called.");

                db.Recipes.Add(item);

                return item;
            }
            catch (DbUpdateException ex)
            {
                logger.LogError($"Failed to add item: {ex}");
                throw new Exception("Error adding data to repository", ex);
            }
        }

        public Recipe Update(Recipe item)
        {
            try
            {
                logger.LogInformation("Recipe : Update was called.");
                var entity = db.Recipes.Attach(item);
                entity.State = EntityState.Modified;

                return item;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update item: {ex}");
                throw new Exception("Error updating data to repository", ex);
            }
        }

        public Recipe Delete(int id)
        {
            try
            {
                logger.LogInformation("Recipe : Delete was called.");

                var recipe = db.Recipes.Find(id);
                if (recipe != null)
                {
                    db.Recipes.Remove(recipe);
                }

                return recipe;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete item: {ex}");
                throw new Exception("Error deleting data from repository", ex);
            }
        }

        public async Task<bool> SaveAllAsync()
        {
            try
            {
                return await db.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to save changes to the repository: {ex}");
                throw new DbUpdateException("Error saving changes to repository.", ex);
            }
        }

        public bool CheckForExisting(string name)
        {
            return db.Recipes.Any(m => m.Name == name);
        }
    }
}
