using System;
using System.Collections.Generic;
using System.Linq;
using Beverage_Buddy.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Beverage_Buddy.Web.ViewModels
{
    public class RecipeCreateUpdateModel
    {
        public Recipe Recipe { get; set; }
        public IEnumerable<SelectListItem> DrinkTypes { get; set; }
        public bool IsAuthenticated { get; set; }

        public RecipeCreateUpdateModel()
        {
            Recipe = new Recipe
            {
                Ingredients = new List<Ingredient>()
            };
            PopulateDrinkTypes();
        }

        public void ConvertJsonResponse(string webResponse)
        {
            Recipe = JsonConvert.DeserializeObject<Recipe>(webResponse);
        }

        private void PopulateDrinkTypes()
        {
            var items = (
                from int value in Enum.GetValues(typeof(DrinkType)) 
                select new SelectListItem
                {
                    Text = Enum.GetName(typeof(DrinkType), value), 
                    Value = value.ToString()
                }).ToList();

            DrinkTypes = items;
        }
    }
}
