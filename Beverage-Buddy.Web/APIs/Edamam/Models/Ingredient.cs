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
        public int Quantity { get; set; }
        public string Measure { get; set; }
        public string Food { get; set; }
        public double Weight { get; set; }
        public string Image { get; set; }
    }
}
