using System.Linq;
using Beverage_Buddy.Data.Models;
using Newtonsoft.Json;

namespace Beverage_Buddy.Web.ViewModels
{
    public class FavoriteViewModel
    {
        public Drink Drink { get; private set; }
        public Recipe Recipe { get; set; }

        public FavoriteViewModel()
        {
            Drink = new Drink();
            Recipe = new Recipe();
        }

        public void ReadJsonResponseForDrink(string webResponse)
        {
            Drink = JsonConvert.DeserializeObject<Drink>(webResponse);
            ConvertDrinkToRecipe();
        }

        private void ConvertDrinkToRecipe()
        {
            Recipe = new Recipe
            {
                Name = Drink.DrinkName,
                Instructions = Drink.Instructions,
                RecipeThumb = Drink.DrinkThumb,
                Alcoholic = Drink.Alcoholic
            };


            var list = Drink.DrinkIngredients.Select(
                ingredient => new Ingredient
                {
                    Amount = ingredient.Measure, 
                    Name = ingredient.Name
                }).ToList();
            Recipe.Ingredients = list;
        }
    }
}
