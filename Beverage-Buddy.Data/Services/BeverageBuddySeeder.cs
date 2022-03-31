using Beverage_Buddy.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Beverage_Buddy.Data.Services
{
    public class BeverageBuddySeeder
    {
        private readonly BeverageBuddyDbContext ctx;
        private readonly IWebHostEnvironment env;

        public BeverageBuddySeeder(BeverageBuddyDbContext ctx, IWebHostEnvironment env)
        {
            this.ctx = ctx;
            this.env = env;
        }

        public void SeedSamples()
        {
            ctx.Database.EnsureCreated();

            // Need to create sample data
            if (!ctx.Ingredients.Any())
            {
                var filePath = Path.Combine(env.ContentRootPath, "SeedData/ingredients.json");
                var json = File.ReadAllText(filePath);

                var ingredients = JsonSerializer.Deserialize<IEnumerable<Ingredient>>(json);

                ctx.Ingredients.AddRange(ingredients);

                ctx.SaveChanges();
            }

            if (!ctx.Recipes.Any())
            {
                var filePath = Path.Combine(env.ContentRootPath, "SeedData/recipes.json");
                var json = File.ReadAllText(filePath);

                var recipes = JsonSerializer.Deserialize<IEnumerable<Recipe>>(json);

                ctx.Recipes.AddRange(recipes);

                ctx.SaveChanges();
            }

            if (!ctx.RecipeIngredients.Any())
            {
                var filePath = Path.Combine(env.ContentRootPath, "SeedData/recipeIngredients.json");
                var json = File.ReadAllText(filePath);

                var recipeIngredients = JsonSerializer.Deserialize<IEnumerable<RecipeIngredient>>(json);

                ctx.RecipeIngredients.AddRange(recipeIngredients);

                ctx.SaveChanges();
            }
        }
    }
}
