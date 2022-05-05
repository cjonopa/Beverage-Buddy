using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Data.Repositories;

namespace Beverage_Buddy.Web.ViewModels
{
    public class RecipeCreateViewModel
    {
        public Recipe Recipe { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public RecipeCreateViewModel()
        {
            Recipe = new Recipe();
            Ingredients = new List<Ingredient>();
        }
    }
}
