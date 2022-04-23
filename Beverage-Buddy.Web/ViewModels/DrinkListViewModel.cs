using System.Collections.Generic;
using Beverage_Buddy.Data.Models;

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
