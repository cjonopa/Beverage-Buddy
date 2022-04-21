using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Beverage_Buddy.Web.APIs.CocktailDb.Models;
using Beverage_Buddy.Web.APIs.CocktailDb.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Beverage_Buddy.Web.APIs.CocktailDb
{
    public class CocktailDbAPICaller
    {
        private readonly ApiSettings apiSettings;
        public string BaseUrl { get; }

        public CocktailDbAPICaller(IOptions<ApiSettings> apiSettings)
        {
            this.apiSettings = apiSettings.Value;
            BaseUrl = $"{this.apiSettings.AccessPoint}/{this.apiSettings.AppKey}/";
        }

        public async Task<List<Drink>> GetDrinkList()
        {
            var drinks = new List<Drink>();

            for (var i = 0; i < 123; i++)
            {
                if (i == 10) i = 97;

                var query = i < 10 ? $"search.php?f={i}" : $"search.php?f={(char) i}";

                using var client = new HttpClient { BaseAddress = new Uri(BaseUrl) };

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using var response = await client.GetAsync(query);
                var apiResponse = response.Content.ReadAsStringAsync().Result;
                var apiResult = JsonConvert.DeserializeObject<CocktailResult>(apiResponse);

                if (apiResult?.Drinks == null) continue;

                drinks.AddRange(apiResult.Drinks.Select(drinkResult => new Drink(drinkResult)));
            }

            return drinks;
        }

        public async Task<Drink> GetDrinkDetails(string id)
        {
            var query = $"lookup.php?i={id}";

            using var client = new HttpClient { BaseAddress = new Uri(BaseUrl) };

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using var response = await client.GetAsync(query);
            var apiResponse = response.Content.ReadAsStringAsync().Result;
            var apiResult = JsonConvert.DeserializeObject<CocktailResult>(apiResponse);

            var drink = new Drink(apiResult.Drinks[0]);

            return drink;
        }
    }
}
