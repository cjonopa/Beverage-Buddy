using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beverage_Buddy.Web.APIs.Edamam.Models
{
    public class Recipe
    {
        public string url { get; set; }
        public string Label { get; set; }
        public string image { get; set; }
        public string Source { get; set; }
        public int Yeild { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public double Calories { get; set; }
        public double TotalWeight { get; set; }
        public double Time { get; set; }
    }
}
