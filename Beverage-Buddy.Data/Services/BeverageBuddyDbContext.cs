using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Beverage_Buddy.Data.Models;

namespace Beverage_Buddy.Data.Services
{
    public class BeverageBuddyDbContext : IdentityDbContext<RecipeUser, IdentityRole, string>
    {
        private readonly IConfiguration config;

        public BeverageBuddyDbContext()
        {
        }

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

            optionsBuilder.UseSqlServer(config["ConnectionStrings:BeverageBuddyContextDb"]);
        }
    }
}
