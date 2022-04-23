using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Beverage_Buddy.Data.APIs.CocktailDb;
using Beverage_Buddy.Data.Models;

namespace Beverage_Buddy.Data.Services
{
    public class BeverageBuddySeeder
    {
        private readonly BeverageBuddyDbContext ctx;
        private readonly IWebHostEnvironment env;
        private readonly UserManager<RecipeUser> userManager;
        private readonly CocktailDbApiCaller apiCaller;

        public BeverageBuddySeeder(BeverageBuddyDbContext ctx, 
            IWebHostEnvironment env, 
            UserManager<RecipeUser> userManager, 
            CocktailDbApiCaller apiCaller)
        {
            this.ctx = ctx;
            this.env = env;
            this.userManager = userManager;
            this.apiCaller = apiCaller;
        }

        public async Task SeedSamplesAsync()
        {
            await ctx.Database.EnsureCreatedAsync();

            var user = await userManager.FindByEmailAsync("guest@beveragebuddy.com");

            if (user == null)
            {
                user = new RecipeUser
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
                var json = await File.ReadAllTextAsync(filePath);

                var ingredients = JsonSerializer.Deserialize<IEnumerable<Ingredient>>(json);

                await ctx.Ingredients.AddRangeAsync(ingredients);

                await ctx.SaveChangesAsync();
            }

            if (!ctx.Recipes.Any())
            {
                var filePath = Path.Combine(env.ContentRootPath, "SeedData/recipes.json");
                var json = await File.ReadAllTextAsync(filePath);

                var recipes = JsonSerializer.Deserialize<IEnumerable<Recipe>>(json);
                var enumerable = recipes as Recipe[] ?? recipes.ToArray();

                foreach (var r in enumerable)
                {
                    r.User = user;
                }

                await ctx.Recipes.AddRangeAsync(enumerable);

                await ctx.SaveChangesAsync();
            }

            if (!ctx.RecipeIngredients.Any())
            {
                var filePath = Path.Combine(env.ContentRootPath, "SeedData/recipeIngredients.json");
                var json = await File.ReadAllTextAsync(filePath);

                var recipeIngredients = JsonSerializer.Deserialize<IEnumerable<RecipeIngredient>>(json);

                await ctx.RecipeIngredients.AddRangeAsync(recipeIngredients);

                await ctx.SaveChangesAsync();
            }

            if (!ctx.Drinks.Any())
            {
                var cocktails = await apiCaller.GetDrinkList();

                await ctx.Drinks.AddRangeAsync(cocktails);
                await ctx.SaveChangesAsync();
            }
        }
    }
}
