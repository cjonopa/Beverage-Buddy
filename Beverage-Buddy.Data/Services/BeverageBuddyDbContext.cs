using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Beverage_Buddy.Data.Models;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Beverage_Buddy.Data.Services
{
    public class BeverageBuddyDbContext : IdentityDbContext<RecipeUser, IdentityRole, string>
    {
        private readonly IConfiguration config;

        public BeverageBuddyDbContext(IConfiguration config)
        {
            this.config = config;
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<DrinkIngredient> DrinkIngredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder
                .UseSqlServer(config["ConnectionStrings:BeverageBuddyContextDb"])
                .UseExceptionProcessor()
                .LogTo(
                    Console.WriteLine, 
                    new [] { DbLoggerCategory.Database.Command.Name },
                    LogLevel.Information
                );
        }
    }
}
