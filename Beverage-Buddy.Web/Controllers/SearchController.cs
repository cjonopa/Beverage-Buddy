using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Data.Repositories;
using Beverage_Buddy.Web.ViewModels;

namespace Beverage_Buddy.Web.Controllers
{
    /// <summary>
    ///   SearchController is a controller class that is used to handle the search functions for Beverage Buddy.
    /// </summary>
    public class SearchController : Controller
    {
        private readonly IRepository<Drink, string> db;

        public SearchController(IRepository<Drink, string> db)
        {
            this.db = db;
        }

        /// <summary>Index is used to display a list of recipes.</summary>
        /// <param name="cont">is a string containing a reference to the next
        /// page of the results to be loaded.</param>
        /// <returns>
        ///   <see cref="Task{IActionResult}"/> containing a <see cref="ViewResult"/> with a model
        /// of <see cref="Result"/> with the list of recipes.
        /// </returns>
        [HttpGet]
        public IActionResult Index(string search)
        {
            var model = new DrinkListViewModel(db.GetAll());

            if (!string.IsNullOrEmpty(search))
            {
                model.Drinks = model.Drinks.Where(s => s.DrinkName.Contains(search));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var model = db.Get(id);

            return View(model);
        }
    }
}
