using System.Linq;
using Beverage_Buddy.Web.APIs.Edamam;
using Beverage_Buddy.Web.APIs.Edamam.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Beverage_Buddy.Web.Controllers
{
    /// <summary>
    ///   SearchController is a controller class that is used to handle the search functions for Beverage Buddy.
    /// </summary>
    public class SearchController : Controller
    {
        private readonly EdamamApiCaller apiCall;

        /// <summary>Initializes a new instance of the <see cref="SearchController" /> class.</summary>
        /// <param name="apiCall">The Edamam API caller for retrieving recipes.</param>
        public SearchController(EdamamApiCaller apiCall)
        {
            this.apiCall = apiCall;
        }

        /// <summary>Index is used to display a list of recipes.</summary>
        /// <param name="cont">is a string containing a reference to the next
        /// page of the results to be loaded.</param>
        /// <returns>
        ///   <see cref="Task{IActionResult}"/> containing a <see cref="ViewResult"/> with a model
        /// of <see cref="Result"/> with the list of recipes.
        /// </returns>
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

        [HttpGet]
        public IActionResult Details(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
