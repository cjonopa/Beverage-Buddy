using System;
using System.IO;
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
using Microsoft.AspNetCore.Http;

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
            var model = new RecipeCreateUpdateModel();
            var user = await userManager.GetUserAsync(User);
            model.Recipe.User = user;
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecipeCreateUpdateModel model, IFormFile file)
        {
            await LoadFileInformation(model, file);

            if (!ModelState.IsValid) return View(model);
            
            using var client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await client.PostAsJsonAsync("api/recipes", model.Recipe);

            if (!result.IsSuccessStatusCode)
            {
                ModelState.AddModelError("Recipe", $"{result.StatusCode}");
                return View(model);
            }
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
            var model = new RecipeCreateUpdateModel
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
        public async Task<ActionResult> Edit(RecipeCreateUpdateModel model, IFormFile file)
        {
            await LoadFileInformation(model, file);

            if (ModelState.IsValid)
            {
                if (!ModelState.IsValid) return View(model);

                using var client = new HttpClient
                {
                    BaseAddress = new Uri(BaseUrl)
                };

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await client.PutAsJsonAsync("api/recipes", model.Recipe);

                if (!result.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("Recipe", $"{result.StatusCode}");
                    return View(model);
                }
                if (result.Headers.Location != null)
                    return Redirect(result.Headers.Location.OriginalString);

                return View(model);
            }

            return View(model);
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

        private async Task LoadFileInformation(RecipeCreateUpdateModel model, IFormFile file)
        {
            if (file != null && file.Length != 0)
            {
                var path = AppDomain.CurrentDomain.BaseDirectory;
                var info = new DirectoryInfo(path);
                var imageLocation = $"{info.Parent?.Parent?.Parent}\\wwwroot\\lib\\images";
                var fileName = $"{model.Recipe.Name}{Path.GetExtension(file.FileName)}";

                var savedFileName = Path.Combine(imageLocation, fileName);

                await using var stream = new FileStream(savedFileName, FileMode.Create);
                await file.CopyToAsync(stream);
                model.Recipe.RecipeThumb = $"\\lib\\images\\{fileName}";
            }
        }

    }
}
