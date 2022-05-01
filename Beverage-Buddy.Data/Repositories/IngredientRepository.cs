using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Beverage_Buddy.Data.Repositories
{
    public class IngredientRepository : IRepository<Ingredient, int>
    {
        private readonly BeverageBuddyDbContext dbContext;
        private readonly ILogger<IngredientRepository> logger;

        public IngredientRepository(BeverageBuddyDbContext dbContext, ILogger<IngredientRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<ICollection<Ingredient>> GetAllAsync()
        {
            try
            {
                logger.LogInformation("Ingredient : GetAllAsync was called.");

                var ingredients =
                    await dbContext.Ingredients
                        .OrderBy(ingredient => ingredient.Name)
                        .ToListAsync();

                return ingredients;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get all items: {ex}");
                throw new Exception("Error retrieving data from repository", ex);
            }
        }

        public async Task<Ingredient> GetAsync(int id)
        {
            try
            {
                logger.LogInformation("Ingredients : GetAsync was called.");

                var ingredients = dbContext.Ingredients
                    .Where(r => r.Id == id);

                return await ingredients.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get item: {ex}");
                throw new Exception("Error retrieving data from repository", ex);
            }
        }

        public Ingredient Add(Ingredient item)
        {
            try
            {
                logger.LogInformation("Ingredients : Add was called.");

                dbContext.Ingredients.Add(item);

                return item;
            }
            catch (DbUpdateException ex)
            {
                logger.LogError($"Failed to add item: {ex}");
                throw new Exception("Error adding data to repository", ex);
            }
        }

        public Ingredient Update(Ingredient item)
        {
            try
            {
                logger.LogInformation("Ingredient : Update was called.");
                var entity = dbContext.Ingredients.Attach(item);
                entity.State = EntityState.Modified;

                return item;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update item: {ex}");
                throw new Exception("Error updating data to repository", ex);
            }
        }

        public Ingredient Delete(int id)
        {
            try
            {
                logger.LogInformation("Recipe : Delete was called.");

                var ingredient = dbContext.Ingredients.Find(id);
                if (ingredient != null)
                {
                    dbContext.Ingredients.Remove(ingredient);
                }

                return ingredient;
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
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to save changes to the repository: {ex}");
                throw new DbUpdateException("Error saving changes to repository.", ex);
            }
        }

        public bool CheckForExisting(string name)
        {
            return false;
        }
    }
}
