using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Web.ViewModels;

namespace Beverage_Buddy.Web.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        public static string BaseUrl { get; } = "http://localhost:5000/";

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

            var pageResults = 20;
            var pageCount = Math.Ceiling(model.Recipes.Count() / (double)pageResults);

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
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Details", new { id = recipe.Id });
            }

            return View();
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
