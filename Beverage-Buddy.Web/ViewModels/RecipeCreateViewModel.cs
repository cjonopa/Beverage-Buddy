using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Data.Repositories;

namespace Beverage_Buddy.Web.ViewModels
{
    public class RecipeCreateViewModel
    {
        public Recipe Recipe { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }

        public RecipeCreateViewModel()
        {
            Recipe = new Recipe();
            Ingredients = new List<Ingredient>();
        }

        public void AddIngredient(Ingredient ingredient)
        {

        }
    }
}
