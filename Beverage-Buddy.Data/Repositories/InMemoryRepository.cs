using System.Collections.Generic;
using System.Linq;
using Beverage_Buddy.Data.Models;

namespace Beverage_Buddy.Data.Repositories
{
    public class InMemoryRepository : IRepository<Recipe, int>
    {
        private readonly List<Recipe> recipes;

        public InMemoryRepository()
        {
            recipes = new List<Recipe>()
            {
                new Recipe {
                    Id = 1,
                    Name = "Orange Juice",
                    Alcoholic = false,
                    DrinkType = DrinkType.Juice
                },
                new Recipe {
                    Id = 2,
                    Name = "ScrewDriver",
                    Alcoholic = true,
                    DrinkType = DrinkType.Mixed
                },
                new Recipe {
                    Id = 3,
                    Name = "Fuzzy Navel",
                    Alcoholic = true,
                    DrinkType = DrinkType.Mixed
                }
            };
        }

        public void Add(Recipe item)
        {
            recipes.Add(item);
            item.Id = recipes.Max(r => r.Id) + 1;
        }

        public void Delete(int id)
        {
            var recipe = Get(id);
            if (recipe != null)
            {
                recipes.Remove(recipe);
            }
        }

        public Recipe Get(int id)
        {
            return recipes.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Recipe> GetAll()
        {
            return recipes.OrderBy(r => r.Name);
        }

        public void Update(Recipe item)
        {
            var existing = Get(item.Id);
            if (existing != null)
            {
                existing.Name = item.Name;
                existing.DrinkType = item.DrinkType;
                existing.Alcoholic = item.Alcoholic;
            }
        }

    }
}
