using BeverageBuddy.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace BeverageBuddy.Data.Services
{
    public class InMemoryRecipeData : IRecipeData
    {
        private List<Recipe> recipes;

        public InMemoryRecipeData()
        {
            recipes = new List<Recipe>()
            {
                new Recipe {
                    Id = 1,
                    Name = "Orange Juice",
                    Alcoholic = false,
                    DrinkType = DrinkType.Juice,
                    Ingredients = new List<string>() {
                        "orange concentrate"
                    }
                },
                new Recipe {
                    Id = 2,
                    Name = "ScrewDriver",
                    Alcoholic = true,
                    DrinkType = DrinkType.Mixed,
                    Ingredients = new List<string>() {
                        "orange concentrate", "vodka"
                    }
                },
                new Recipe {
                    Id = 3,
                    Name = "Fuzzy Navel",
                    Alcoholic = true,
                    DrinkType = DrinkType.Mixed,
                    Ingredients = new List<string>() {
                        "orange concentrate", "peach schnapps"
                    }
                }
            };
        }

        public IEnumerable<Recipe> GetAll()
        {
            return recipes.OrderBy(r => r.Name);
        }
    }
}
