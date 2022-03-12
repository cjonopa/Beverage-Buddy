using BeverageBuddy.Data.Models;
using System.Data.Entity;

namespace BeverageBuddy.Data.Services
{
    public class BeverageBuddyDbContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
    }
}
