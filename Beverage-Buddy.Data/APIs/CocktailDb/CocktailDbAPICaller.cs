using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Beverage_Buddy.Data.APIs.CocktailDb.Models;
using Beverage_Buddy.Data.APIs.CocktailDb.Settings;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Data.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Beverage_Buddy.Data.APIs.CocktailDb
{
    public class CocktailDbApiCaller
    {
        private readonly IConverterService<Drink, DrinkResult> converterService;
        public string BaseUrl { get; }

        public CocktailDbApiCaller(IOptions<ApiSettings> apiSettings, IConverterService<Drink, DrinkResult> converterService)
        {
            this.converterService = converterService;
            BaseUrl = $"{apiSettings.Value.AccessPoint}/{apiSettings.Value.AppKey}/";
        }

        public async Task<IEnumerable<Drink>> GetDrinkList()
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

                drinks.AddRange(apiResult.Drinks.Select(
                    drinkResult => converterService.ConvertResult(new Drink(), drinkResult)));
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

            var drink = converterService.ConvertResult(new Drink(), apiResult.Drinks.FirstOrDefault());

            return drink;
        }
    }
}
