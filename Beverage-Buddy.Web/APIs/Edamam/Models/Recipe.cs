using System;
using System.Collections.Generic;

namespace Beverage_Buddy.Web.APIs.Edamam.Models
{
    public class Recipe
    {
        public string Uri { get; set; }
        public string Label { get; set; }
        public string Image { get; set; }
        public Image Images { get; set; }
        public string Source { get; set; }
        public string Url { get; set; }
        public string ShareAs { get; set; }
        public double Yield { get; set; }
        public ICollection<string> IngredientLines { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public float Calories { get; set; }
        public float TotalWeight { get; set; }
        public float TotalTime { get; set; }
        public ICollection<string> DietLabels { get; set; }
        public ICollection<string> HealthLabels { get; set; }
        public ICollection<string> CuisineType { get; set; }
        public ICollection<string> MealType { get; set; }
        public ICollection<string> DishType { get; set; }

        public string Id
        {
            get
            {
                var indexOf = Uri?.IndexOf("#recipe_", StringComparison.Ordinal);
                return indexOf != null ? Uri?[((int)indexOf + 8)..] : null;
            }
        }
    }
}
