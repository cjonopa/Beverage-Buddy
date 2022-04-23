using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Models;

namespace Beverage_Buddy.Web.ViewModels
{
    public class RecipeListViewModel
    {
        public IEnumerable<Recipe> Recipes { get; set; }

        public int Pages { get; set; }
        public int CurrentPage { get; set; }

        public RecipeListViewModel(IEnumerable<Recipe> recipes)
        {
            Recipes = recipes;
        }
    }
}
