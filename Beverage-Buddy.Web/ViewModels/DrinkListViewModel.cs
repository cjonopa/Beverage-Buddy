using System.Collections.Generic;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Web.Services;

namespace Beverage_Buddy.Web.ViewModels
{
    public class DrinkListViewModel
    {
        public IEnumerable<Drink> Drinks { get; set; }

        public int Pages { get; set; }
        public int CurrentPage { get; set; }

        public DrinkListViewModel(IEnumerable<Drink> drinks)
        {
            Drinks = drinks;
        }
    }
}
