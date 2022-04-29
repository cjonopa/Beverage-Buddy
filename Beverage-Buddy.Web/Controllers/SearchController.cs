using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Web.ViewModels;

namespace Beverage_Buddy.Web.Controllers
{
    /// <summary>
    ///   SearchController is a controller class that is used to handle the search functions for Beverage Buddy.
    /// </summary>
    public class SearchController : Controller
    {
        public static string BaseUrl { get; } = "http://localhost:5000/";

        /// <summary>Index is used to display a list of recipes.</summary>
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
            var model = new DrinkListViewModel();

            ViewData["CurrentNameFilter"] = searchName;
            ViewData["CurrentIngredientFilter"] = searchIngredient;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("api/drinks");

                if (response.IsSuccessStatusCode)
                {
                    var webResponse = response.Content.ReadAsStringAsync().Result;
                    model.ConvertJsonResponse(webResponse);
                }
            }

            if (!string.IsNullOrEmpty(searchName))
            {
                model.Drinks = model.Drinks.Where(s => s.DrinkName.ToLower().Contains(searchName.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(searchIngredient))
            {
                model.Drinks = model.Drinks.Where(
                    s => s.DrinkIngredients.Any(
                        di => di.Name.ToLower().Contains(searchIngredient.ToLower()))
                    ).ToList();
            }

            var pageResults = 20;
            var pageCount = Math.Ceiling(model.Drinks.Count / (double)pageResults);

            model.Drinks = model.Drinks
                .Skip((page - 1) * pageResults)
                .Take(pageResults).ToList();

            model.CurrentPage = page;
            model.Pages = (int) pageCount;

            return View(model);
        }

        /// <summary>
        /// Details of the specified drink.
        /// </summary>
        /// <param name="id">The id of the drink.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var model = new DrinkDetailsViewModel
            {
                IsAuthenticated = User.Identity != null && User.Identity.IsAuthenticated
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/drinks/{id}");

                if (!response.IsSuccessStatusCode) return View(model);

                var webResponse = response.Content.ReadAsStringAsync().Result;
                model.ConvertJsonResponse(webResponse);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Favorite(string id)
        {
            var model = new FavoriteViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/drinks/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var webResponse = response.Content.ReadAsStringAsync().Result;
                    model.ReadJsonResponseForDrink(webResponse);
                }
            }
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Favorite(string id, int drinkType)
        {
            var model = new FavoriteViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/drinks/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var webResponse = response.Content.ReadAsStringAsync().Result;
                    model.ReadJsonResponseForDrink(webResponse);

                    model.Recipe.DrinkType = (DrinkType) drinkType;

                    if (model.Recipe.DrinkType != DrinkType.None)
                    {
                        using var client2 = new HttpClient
                        {
                            BaseAddress = new Uri(BaseUrl)
                        };

                        client2.DefaultRequestHeaders.Clear();
                        client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var result = await client2.PostAsJsonAsync("api/recipes", model.Recipe);

                        if (result.IsSuccessStatusCode)
                        {
                            if (result.Headers.Location != null)
                                return Redirect(result.Headers.Location.OriginalString);
                        }
                    }
                }
            }

            ModelState.AddModelError("", "Failed to Add Favorite.");

            return View(model);
        }
    }
}
