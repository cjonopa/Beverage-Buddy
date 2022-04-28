using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Data.Repositories;
using Beverage_Buddy.Web.Services;
using Beverage_Buddy.Web.ViewModels;

namespace Beverage_Buddy.Web.Controllers
{
    /// <summary>
    ///   SearchController is a controller class that is used to handle the search functions for Beverage Buddy.
    /// </summary>
    public class SearchController : Controller
    {
        private readonly IRepository<Drink, string> drinkRepo;
        private readonly IRepository<Recipe, int> recipeRepo;

        public SearchController(IRepository<Drink, string> drinkRepo, IRepository<Recipe, int> recipeRepo)
        {
            this.drinkRepo = drinkRepo;
            this.recipeRepo = recipeRepo;
        }

        /// <summary>Index is used to display a list of recipes.</summary>
        /// <param name="cont">is a string containing a reference to the next
        /// page of the results to be loaded.</param>
        /// <param name="searchName"></param>
        /// <param name="searchIngredient"></param>
        /// <param name="page"></param>
        /// <returns>
        ///   <see cref="Task{IActionResult}"/> containing a <see cref="ViewResult"/> with a model
        /// of <see cref="DrinkListViewModel"/> with the list of recipes.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Index(string searchName, string searchIngredient, int page)
        {
            ViewData["CurrentNameFilter"] = searchName;
            ViewData["CurrentIngredientFilter"] = searchIngredient;

            var drinks = await drinkRepo.GetAll();
            
            if (!string.IsNullOrEmpty(searchName))
            {
                drinks = drinks.Where(s => s.DrinkName.ToLower().Contains(searchName.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(searchIngredient))
            {
                drinks = drinks.Where(
                    s => s.DrinkIngredients.Any(
                        di => di.Name.ToLower().Contains(searchIngredient.ToLower()))
                    ).ToList();
            }

            var pageResults = 20;
            var pageCount = Math.Ceiling(drinks.Count() / (double)pageResults);

            drinks = drinks
                .Skip((page - 1) * pageResults)
                .Take(pageResults).ToList();
            
            var model = new DrinkListViewModel(drinks)
            {
                CurrentPage = page, 
                Pages = (int) pageCount
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var model = drinkRepo.Get(id);

            return View(model);
        }

        [HttpGet]
        public IActionResult Favorite(string id)
        {
            var drink = drinkRepo.Get(id);
            var model = new FavoriteViewModel(drink, new Recipe());

            return View(model);
        }

        [HttpPost]
        public IActionResult Favorite(Recipe recipe)
        {
            var model = new FavoriteViewModel(null, recipe);

            return View(model);
        }
    }
}
