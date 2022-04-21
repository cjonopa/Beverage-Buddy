using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beverage_Buddy.Web.APIs.CocktailDb.Models;

namespace Beverage_Buddy.Web.ViewModels
{
    public class DrinkListViewModel
    {
        public IEnumerable<Drink> Drinks { get; set; }

        public DrinkListViewModel(IEnumerable<Drink> drinks)
        {
            Drinks = drinks;
        }
    }
}
