using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Data.Repositories;
using Beverage_Buddy.Web.ViewModels;

namespace Beverage_Buddy.Web.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        private readonly IRepository<Recipe, int> db;

        public RecipeController(IRepository<Recipe, int> db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchName, int page)
        {
            ViewData["CurrentNameFilter"] = searchName;

            var recipes = await db.GetAllAsync();

            if (recipes == null) return View();

            if (!string.IsNullOrEmpty(searchName))
            {
                recipes = recipes.Where(s => s.Name.ToLower().Contains(searchName.ToLower())).ToList();
            }

            var pageResults = 20;
            var pageCount = Math.Ceiling(recipes.Count() / (double)pageResults);

            recipes = recipes
                .Skip((page - 1) * pageResults)
                .Take(pageResults).ToList();

            var model = new RecipeListViewModel(recipes);
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = db.Get(id);
            if (model == null)
            {
                return View("NotFound");
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
                db.Add(recipe);
                return RedirectToAction("Details", new { id = recipe.Id });
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = db.Get(id);
            if (model == null)
            {
                return View("NotFound");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Update(recipe);
                return RedirectToAction("Details", new { id = recipe.Id });
            }

            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = db.Get(id);
            if (model == null)
            {
                return View("NotFound");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Recipe recipe)
        {
            db.Delete(recipe.Id);
            return RedirectToAction("Index");
        }
    }
}
