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
                    Ingredients = new List<Ingredient>() {
                        new Ingredient() {Id = 1, Name = "Orange Concentrate", Amount = 1}
                    }
                },
                new Recipe {
                    Id = 2,
                    Name = "ScrewDriver",
                    Alcoholic = true,
                    DrinkType = DrinkType.Mixed,
                    Ingredients = new List<Ingredient>() {
                        new Ingredient() {Id = 1, Name = "Orange Concentrate", Amount = 1},
                        new Ingredient() {Id = 2, Name = "Vodka", Amount = 1}
                    }
                },
                new Recipe {
                    Id = 3,
                    Name = "Fuzzy Navel",
                    Alcoholic = true,
                    DrinkType = DrinkType.Mixed,
                    Ingredients = new List<Ingredient>() {
                        new Ingredient() {Id = 1, Name = "Orange Concentrate", Amount = 1},
                        new Ingredient() {Id = 3, Name = "Peach Schnapps", Amount = 1}
                    }
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
