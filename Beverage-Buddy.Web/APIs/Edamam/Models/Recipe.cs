using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int Yeild { get; set; }
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
    }
}
