using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Beverage_Buddy.Data.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        private readonly IRepository<Drink, string> drinkRepository;
        private readonly ILogger<DrinksController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrinksController"/> class.
        /// </summary>
        /// <param name="drinkRepository">The drink repository.</param>
        /// <param name="logger">The logger.</param>
        public DrinksController(IRepository<Drink, string> drinkRepository, ILogger<DrinksController> logger)
        {
            this.drinkRepository = drinkRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ICollection<Drink>>> Get()
        {
            try
            {
                logger.LogInformation("Drink : GetAll was called.");
                var results = await drinkRepository.GetAllAsync();

                return Ok(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get all drinks: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the specified drink identifier.
        /// </summary>
        /// <param name="drinkId">The drink identifier.</param>
        /// <returns></returns>
        [HttpGet("{drinkId}")]
        public ActionResult<Drink> Get(string drinkId)
        {
            try
            {
                logger.LogInformation("Drink : GetAll was called.");
                var results = drinkRepository.Get(drinkId);
                if (results == null) return NotFound($"No drink with id, {drinkId}, was found.");

                return Ok(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get drink: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
