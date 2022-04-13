using Beverage_Buddy.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<RecipeUser> userManager;

        public BeverageBuddySeeder(BeverageBuddyDbContext ctx, IWebHostEnvironment env, UserManager<RecipeUser> userManager)
        {
            this.ctx = ctx;
            this.env = env;
            this.userManager = userManager;
        }

        public async Task SeedSamplesAsync()
        {
            ctx.Database.EnsureCreated();

            RecipeUser user = await userManager.FindByEmailAsync("guest@beveragebuddy.com");

            if (user == null)
            {
                user = new RecipeUser()
                {
                    FirstName = "Guest",
                    LastName = "User",
                    Email = "guest@beveragebuddy.com",
                    UserName = "guest@beveragebuddy.com"
                };

                var result = await userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create a new user in seeder");
                }
            }

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
                foreach (var r in recipes)
                {
                    r.User = user;
                }

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
