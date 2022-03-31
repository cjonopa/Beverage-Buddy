using Beverage_Buddy.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Beverage_Buddy.Data.Services
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly BeverageBuddyDbContext db;
        private readonly ILogger<RecipeRepository> logger;

        public RecipeRepository(BeverageBuddyDbContext db, ILogger<RecipeRepository> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        public void Add(Recipe recipe)
        {
            try
            {
                logger.LogInformation("Recipe : Add was called.");

                db.Recipes.Add(recipe);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to add recipe: {ex}");
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
                logger.LogError($"Failed to delete recipe: {ex}");
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
                logger.LogError($"Failed to get recipe: {ex}");
                return null;
            }
        }

        public IEnumerable<Recipe> GetAll()
        {
            try
            {
                logger.LogInformation("Recipe : GetAll was called.");

                return from r in db.Recipes
                       orderby r.Name
                       select r;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get all recipes: {ex}");
                return null;
            }
        }

        public void Update(Recipe recipe)
        {
            try
            {
                logger.LogInformation("Recipe : Update was called.");

                var entry = db.Entry(recipe);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update recipe: {ex}");
            }
        }

        public bool SaveAll()
        {
            try
            {
                logger.LogInformation("Recipe : SaveAll was called.");

                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to save all recipes: {ex}");
                return false;
            }
        }
    }
}
