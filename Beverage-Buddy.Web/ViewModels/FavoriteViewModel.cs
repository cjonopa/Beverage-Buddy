using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Models;

namespace Beverage_Buddy.Web.ViewModels
{
    public class FavoriteViewModel
    {
        public Drink Drink { get; set; }
        public Recipe Recipe { get; set; }

        public FavoriteViewModel(Drink drink, Recipe recipe)
        {
            Drink = drink;
            Recipe = recipe;
        }
    }
}
