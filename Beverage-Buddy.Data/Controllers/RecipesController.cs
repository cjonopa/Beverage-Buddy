﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Beverage_Buddy.Data.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRepository<Recipe, int> recipeRepository;
        private readonly ILogger<RecipesController> logger;
        private readonly LinkGenerator linkGenerator;

        public RecipesController(IRepository<Recipe, int> recipeRepository, ILogger<RecipesController> logger, 
            LinkGenerator linkGenerator)
        {
            this.recipeRepository = recipeRepository;
            this.logger = logger;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Recipe>>> Get()
        {
            try
            {
                logger.LogInformation("Recipe : GetAll was called.");
                var results = await recipeRepository.GetAllAsync();

                return Ok(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get all recipes: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{recipeId}")]
        public ActionResult<Recipe> Get(int recipeId)
        {
            try
            {
                logger.LogInformation("Recipe : GetAll was called.");
                var results = recipeRepository.Get(recipeId);
                if (results == null) return NotFound($"No recipe with id, {recipeId}, was found.");

                return Ok(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get recipe: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Recipe>> Post([FromBody]Recipe model)
        {
            try
            {
                var existing = recipeRepository.CheckForExisting(model.Name);

                if (existing) return BadRequest($"The recipe name, {model.Name}, already exists in Favorites.");


                recipeRepository.Add(model);
                if (await recipeRepository.SaveAllAsync())
                {
                    var location = linkGenerator.GetPathByAction("Details", "Recipe", new { recipeId = model.Id });
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
