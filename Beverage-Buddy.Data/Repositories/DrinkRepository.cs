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
                return null;
            }
        }

        public Drink Get(string id)
        {
            try
            {
                logger.LogInformation("Drink : Get was called.");

                return db.Drinks.Include(d => d.DrinkIngredients).FirstOrDefault(d => d.Id == id);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get drink: {ex}");
                return null;
            }
        }

        public void Add(Drink item)
        {
            try
            {
                logger.LogInformation("Recipe : Add was called.");

                db.Drinks.Add(item);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to add drink: {ex}");
            }
        }

        public void Update(Drink item)
        {
            try
            {
                logger.LogInformation("Drink : Update was called.");

                var entry = db.Entry(item);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update drink: {ex}");
            }
        }

        public void Delete(string id)
        {
            try
            {
                logger.LogInformation("Drink : Delete was called.");

                var drink = db.Drinks.Find(id);
                db.Drinks.Remove(drink);
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
    }
}
