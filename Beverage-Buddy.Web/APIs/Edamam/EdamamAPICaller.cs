using Beverage_Buddy.Web.APIs.Edamam.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Beverage_Buddy.Web.APIs.Edamam.Settings;

namespace Beverage_Buddy.Web.APIs.Edamam
{
    /// <summary>
    ///   EdamamApiCaller is a class used to call the Edamam Recipe Search API V2.
    ///   <see href="https://developer.edamam.com/edamam-docs-recipe-api"/>
    /// </summary>
    public class EdamamApiCaller
    {
        private readonly ApiSettings apiSettings;

        private string BaseUrl { get; }


        /// <summary>Initializes a new instance of the <see cref="EdamamApiCaller" /> class.</summary>
        /// <param name="apiSettings">The API settings.</param>
        public EdamamApiCaller(IOptions<ApiSettings> apiSettings)
        {
            this.apiSettings = apiSettings.Value;
            BaseUrl = this.apiSettings.AccessPoint;
        }

        /// <summary>Retrieves all drink recipes from the Edamam Recipe Search API.</summary>
        /// <returns>
        ///   <see cref="Task{T}" /> containing the hits of recipes found from the Edamam API.
        /// </returns>
        public Task<Result> RetrieveDrinkRecipes()
        {
            return RetrieveDrinkRecipes(null);
        }


        /// <summary>Retrieves all drink recipes from the Edamam Recipe Search API.</summary>
        /// <returns>
        ///   <see cref="Task{T}" /> containing the hits of recipes found from the Edamam API.
        /// </returns>
        public async Task<Result> RetrieveDrinkRecipes(string cont)
        {
            var query = $"?type={apiSettings.Type}&app_key={apiSettings.AppKey}" +
                        $"&app_id={apiSettings.AppId}&dishType={apiSettings.DishType}";

            if (!string.IsNullOrEmpty(cont)) query += $"&_cont={cont}";

            using var client = new HttpClient {BaseAddress = new Uri(BaseUrl)};

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using var response = await client.GetAsync(query);

            var apiResponse = response.Content.ReadAsStringAsync().Result;
            var apiResult = JsonConvert.DeserializeObject<Result>(apiResponse);

            return apiResult;
        }
    }
}
