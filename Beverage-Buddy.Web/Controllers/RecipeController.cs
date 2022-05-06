using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Beverage_Buddy.Web.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        private readonly UserManager<RecipeUser> userManager;
        public static string BaseUrl { get; } = "http://localhost:5000/";

        public RecipeController(UserManager<RecipeUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchName, int page)
        {
            var model = new RecipeListViewModel();

            ViewData["CurrentNameFilter"] = searchName;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("api/recipes");

                if (response.IsSuccessStatusCode)
                {
                    var webResponse = response.Content.ReadAsStringAsync().Result;
                    model.ConvertJsonResponse(webResponse);
                }
            }

            if (model.Recipes == null) return View();

            if (!string.IsNullOrEmpty(searchName))
            {
                model.Recipes = model.Recipes.Where(s => s.Name.ToLower().Contains(searchName.ToLower())).ToList();
            }

            const int pageResults = 20;
            var count = model.Recipes.Count;
            var pageCount = Math.Ceiling(count / (double)pageResults);

            model.Recipes = model.Recipes
                .Skip((page - 1) * pageResults)
                .Take(pageResults).ToList();

            model.CurrentPage = page;
            model.Pages = (int)pageCount;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = new RecipeDetailsViewModel
            {
                IsAuthenticated = User.Identity != null && User.Identity.IsAuthenticated
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/recipes/{id}");

                if (!response.IsSuccessStatusCode) return View(model);

                var webResponse = response.Content.ReadAsStringAsync().Result;
                model.ConvertJsonResponse(webResponse);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new RecipeCreateViewModel();
            var user = await userManager.GetUserAsync(User);
            model.Recipe.User = user;
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecipeCreateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            using var client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await client.PostAsJsonAsync("api/recipes", model.Recipe);

            if (!result.IsSuccessStatusCode) return View(model);
            if (result.Headers.Location != null)
                return Redirect(result.Headers.Location.OriginalString);

            return View(model);
        }


        public ActionResult IngredientEntryRow()
        {
            return PartialView("_Ingredients");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = new RecipeDetailsViewModel()
            {
                IsAuthenticated = User.Identity != null && User.Identity.IsAuthenticated
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/recipes/{id}");

                if (!response.IsSuccessStatusCode) return View(model);

                var webResponse = response.Content.ReadAsStringAsync().Result;
                model.ConvertJsonResponse(webResponse);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Details", new { id = recipe.Id });
            }

            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = new Recipe();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Recipe recipe)
        {

            return RedirectToAction("Index");
        }
    }
}
