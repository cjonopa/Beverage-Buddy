using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beverage_Buddy.Web.APIs.Edamam.Models
{
    public class Ingredient
    {
        public string Text { get; set; }
        public float Quantity { get; set; }
        public string Food { get; set; }
        public string Measure { get; set; }
        public float Weight { get; set; }
        public string FoodId { get; set; }
        public string FoodCategory { get; set; }
    }
}
