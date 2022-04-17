using Beverage_Buddy.Web.APIs.Edamam;
using Beverage_Buddy.Web.APIs.Edamam.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Beverage_Buddy.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly EdamamAPICaller apiCall;

        public SearchController(EdamamAPICaller apiCall)
        {
            this.apiCall = apiCall;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string cont)
        {
            Result model;
            if (string.IsNullOrEmpty(cont))
            {
                model = await apiCall.RetrieveDrinkRecipes();
            } else
            {
                model = await apiCall.RetrieveDrinkRecipes(cont);
            }

            return View(model);
        }
    }
}
