using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Beverage_Buddy.Data.Repositories
{
    public class DrinkRepository : IRepository<Drink, string>
    {
        private readonly BeverageBuddyDbContext db;
        private readonly ILogger<DrinkRepository> logger;

        public DrinkRepository(BeverageBuddyDbContext db, ILogger<DrinkRepository> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        public async Task<ICollection<Drink>> GetAllAsync()
        {
            try
            {
                logger.LogInformation("Drink : GetAllAsync was called.");

                var drinks = await db
                    .Drinks
                    .OrderBy(d => d.DrinkName)
                    .Include(d => d.DrinkIngredients)
                    .ToListAsync();

                return drinks;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get all drinks: {ex}");
                throw new Exception("Error retrieving data from repository", ex);
            }
        }

        public async Task<Drink> GetAsync(string id)
        {
            try
            {
                logger.LogInformation("Drink : GetAsync was called.");

                var drink = db.Drinks
                    .Include(d => d.DrinkIngredients)
                    .Where(d => d.Id == id);

                return await drink.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get drink: {ex}");
                throw new Exception("Error retrieving data from repository", ex);
            }
        }

        public Drink Add(Drink item)
        {
            try
            {
                logger.LogInformation("Recipe : Add was called.");
                db.Drinks.Add(item);
                return item;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to add drink: {ex}");
                throw new Exception("Error adding data to repository", ex);
            }
        }

        public Drink Update(Drink item)
        {
            try
            {
                logger.LogInformation("Drink : Update was called.");
                var entity = db.Drinks.Attach(item);
                entity.State = EntityState.Modified;
                return item;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update drink: {ex}");
                throw new Exception("Error updating data to repository", ex);
            }
        }

        public Drink Delete(string id)
        {
            try
            {
                logger.LogInformation("Drink : Delete was called.");

                var drink = db.Drinks.Find(id);
                if (drink != null)
                {
                    db.Drinks.Remove(drink);
                }

                return drink;
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
                var saved = await db.SaveChangesAsync();
                return saved > 0;
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
