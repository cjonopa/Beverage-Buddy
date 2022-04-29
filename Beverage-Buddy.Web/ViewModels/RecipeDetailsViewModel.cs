using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Models;
using Newtonsoft.Json;

namespace Beverage_Buddy.Web.ViewModels
{
    public class RecipeDetailsViewModel
    {
        public Recipe Recipe { get; set; }
        public bool IsAuthenticated { get; set; }

        public void ConvertJsonResponse(string webResponse)
        {
            Recipe = JsonConvert.DeserializeObject<Recipe>(webResponse);
        }
    }
}
