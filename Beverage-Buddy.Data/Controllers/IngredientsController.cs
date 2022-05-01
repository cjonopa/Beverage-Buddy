using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Beverage_Buddy.Data.Controllers
{
    public class IngredientsController : ControllerBase
    {
        private readonly IRepository<Ingredient, int> repository;
        private readonly ILogger<IngredientsController> logger;
        private readonly LinkGenerator linkGenerator;

        public IngredientsController(IRepository<Ingredient, int> repository, ILogger<IngredientsController> logger,
            LinkGenerator linkGenerator)
        {
            this.repository = repository;
            this.logger = logger;
            this.linkGenerator = linkGenerator;
        }


        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ICollection<Ingredient>>> Get()
        {
            try
            {
                logger.LogInformation("Recipe : GetAll was called.");
                var results = await repository.GetAllAsync();

                return Ok(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get all recipes: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the specified recipe identifier.
        /// </summary>
        /// <param name="recipeId">The recipe identifier.</param>
        /// <returns></returns>
        [HttpGet("{recipeId}")]
        public ActionResult<Recipe> Get(int recipeId)
        {
            try
            {
                logger.LogInformation("Recipe : GetAll was called.");
                var results = repository.GetAsync(recipeId);
                if (results == null) return NotFound($"No recipe with id, {recipeId}, was found.");

                return Ok(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get recipe: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Ingredient>> Post([FromBody] Ingredient model)
        {
            try
            {
                var existing = repository.CheckForExisting(model.Name);

                if (existing) return BadRequest($"The recipe name, {model.Name}, already exists in Favorites.");


                repository.Add(model);
                if (await repository.SaveAllAsync())
                {
                    var location = linkGenerator.GetPathByAction("Details", "Ingredient", new { id = model.Id });
                    return Created(location, model);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to save Favorite recipe: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest("Failed to save the favorite recipe.");
        }
    }
}
