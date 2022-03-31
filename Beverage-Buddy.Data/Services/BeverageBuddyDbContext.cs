﻿using Beverage_Buddy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Beverage_Buddy.Data.Services
{
    public class BeverageBuddyDbContext : DbContext
    {
        private readonly IConfiguration config;

        public BeverageBuddyDbContext(IConfiguration config)
        {
            this.config = config;
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(config["ConnectionStrings:BeverageBuddyContextDb"]);
        }
    }
}