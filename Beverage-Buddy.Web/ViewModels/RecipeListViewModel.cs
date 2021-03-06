using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Models;
using Newtonsoft.Json;

namespace Beverage_Buddy.Web.ViewModels
{
    public class RecipeListViewModel
    {
        public ICollection<Recipe> Recipes { get; set; }

        public int Pages { get; set; }
        public int CurrentPage { get; set; }

        public void ConvertJsonResponse(string webResponse)
        {
            Recipes = JsonConvert.DeserializeObject<ICollection<Recipe>>(webResponse);
        }

    }
}
