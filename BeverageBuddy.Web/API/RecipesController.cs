using BeverageBuddy.Data.Models;
using BeverageBuddy.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BeverageBuddy.Web.API
{
    public class RecipesController : ApiController
    {
        private readonly IRecipeData db;

        public RecipesController(IRecipeData db)
        {
            this.db = db;
        }

        public IEnumerable<Recipe> Get()
        {
            var model = db.GetAll();
            return model;
        }
    }
}
