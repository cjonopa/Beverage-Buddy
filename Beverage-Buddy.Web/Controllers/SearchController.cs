using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Beverage_Buddy.Web.APIs.CocktailDb;
using Beverage_Buddy.Web.ViewModels;

namespace Beverage_Buddy.Web.Controllers
{
    /// <summary>
    ///   SearchController is a controller class that is used to handle the search functions for Beverage Buddy.
    /// </summary>
    public class SearchController : Controller
    {
        private readonly CocktailDbAPICaller apiCall;

        /// <summary>Initializes a new instance of the <see cref="CocktailDbAPICaller" /> class.</summary>
        /// <param name="apiCall">The Edamam API caller for retrieving recipes.</param>
        public SearchController(CocktailDbAPICaller apiCall)
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
        public async Task<IActionResult> Index(string search)
        {
            var model = new DrinkListViewModel(await apiCall.GetDrinkList());

            if (!string.IsNullOrEmpty(search))
            {
                model.Drinks = model.Drinks.Where(s => s.DrinkName.Contains(search));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var model = await apiCall.GetDrinkDetails(id);

            return View(model);
        }
    }
}
