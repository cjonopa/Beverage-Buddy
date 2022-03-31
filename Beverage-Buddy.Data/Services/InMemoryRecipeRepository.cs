using Beverage_Buddy.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Beverage_Buddy.Data.Services
{
    public class InMemoryRecipeRepository : IRecipeRepository
    {
        private List<Recipe> recipes;

        public InMemoryRecipeRepository()
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

        public void Add(Recipe recipe)
        {
            recipes.Add(recipe);
            recipe.Id = recipes.Max(r => r.Id) + 1;
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

        public bool SaveAll()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Recipe recipe)
        {
            var existing = Get(recipe.Id);
            if (existing != null)
            {
                existing.Name = recipe.Name;
                existing.DrinkType = recipe.DrinkType;
                existing.Alcoholic = recipe.Alcoholic;
            }
        }

    }
}
