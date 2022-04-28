using System.Collections.Generic;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Web.Services;
using Newtonsoft.Json;

namespace Beverage_Buddy.Web.ViewModels
{
    public class DrinkListViewModel
    {
        public ICollection<Drink> Drinks { get; set; }

        public int Pages { get; set; }
        public int CurrentPage { get; set; }

        public void ConvertJsonResponse(string webResponse)
        {
            Drinks = JsonConvert.DeserializeObject<ICollection<Drink>>(webResponse);
        }
    }
}
