using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beverage_Buddy.Web.APIs.CocktailDb.Models
{
    public class Drink
    {
        public string Id { get; set; }
        public string DrinkName { get; set; }
        public string DrinkAlternate { get; set; }
        public string Tags { get; set; }
        public string Video { get; set; }
        public string Category { get; set; }
        public string IBA { get; set; }
        public bool Alcoholic { get; set; }
        public string Glass { get; set; }
        public string Instructions { get; set; }
        public string DrinkThumb { get; set; }
        public List<string> Ingredients { get; set; }
        public string ImageSource { get; set; }
        public string ImageAttribution { get; set; }
        public bool CreativeCommonsConfirmed { get; set; }
        public DateTime? Modified { get; set; }

        public Drink(DrinkResult drinkResult)
        {
            ConvertDrinkResult(drinkResult);
        }

        private void ConvertDrinkResult(DrinkResult dr)
        {
            Id = dr.IdDrink;
            DrinkName = dr.StrDrink;
            DrinkAlternate = dr.StrAlternate;
            Tags = dr.StrTag;
            Video = dr.StrVideo;
            Category = dr.StrCategory;
            IBA = dr.StrIBA;
            Alcoholic = dr.StrAlcoholic?.ToLower() == "alcoholic";
            Glass = dr.StrGlass;
            Instructions = dr.StrInstructions;
            DrinkThumb = dr.StrDrinkThumb;
            AddIngredients(dr);
            ImageSource = dr.StrImageSource;
            ImageAttribution = dr.StrImageAttribution;
            CreativeCommonsConfirmed = dr.StrCreativeCommonsConfirmed?.ToLower() == "yes";
            Modified = dr.DateModified;
        }

        private void AddIngredients(DrinkResult dr)
        {
            Ingredients = new List<string>();
            if (!string.IsNullOrEmpty(dr.StrIngredient1)) Ingredients.Add($"{dr.StrMeasure1} - {dr.StrIngredient1}");
            if (!string.IsNullOrEmpty(dr.StrIngredient2)) Ingredients.Add($"{dr.StrMeasure2} - {dr.StrIngredient2}");  
            if (!string.IsNullOrEmpty(dr.StrIngredient3)) Ingredients.Add($"{dr.StrMeasure3} - {dr.StrIngredient3}");
            if (!string.IsNullOrEmpty(dr.StrIngredient4)) Ingredients.Add($"{dr.StrMeasure4} - {dr.StrIngredient4}");
            if (!string.IsNullOrEmpty(dr.StrIngredient5)) Ingredients.Add($"{dr.StrMeasure5} - {dr.StrIngredient5}");
            if (!string.IsNullOrEmpty(dr.StrIngredient6)) Ingredients.Add($"{dr.StrMeasure6} - {dr.StrIngredient6}");
            if (!string.IsNullOrEmpty(dr.StrIngredient7)) Ingredients.Add($"{dr.StrMeasure7} - {dr.StrIngredient7}");
            if (!string.IsNullOrEmpty(dr.StrIngredient8)) Ingredients.Add($"{dr.StrMeasure8} - {dr.StrIngredient8}");
            if (!string.IsNullOrEmpty(dr.StrIngredient9)) Ingredients.Add($"{dr.StrMeasure9} - {dr.StrIngredient9}");
            if (!string.IsNullOrEmpty(dr.StrIngredient10)) Ingredients.Add($"{dr.StrMeasure10} - {dr.StrIngredient10}");
            if (!string.IsNullOrEmpty(dr.StrIngredient11)) Ingredients.Add($"{dr.StrMeasure11} - {dr.StrIngredient11}");
            if (!string.IsNullOrEmpty(dr.StrIngredient12)) Ingredients.Add($"{dr.StrMeasure12} - {dr.StrIngredient12}");
            if (!string.IsNullOrEmpty(dr.StrIngredient13)) Ingredients.Add($"{dr.StrMeasure13} - {dr.StrIngredient13}");
            if (!string.IsNullOrEmpty(dr.StrIngredient14)) Ingredients.Add($"{dr.StrMeasure14} - {dr.StrIngredient14}");
            if (!string.IsNullOrEmpty(dr.StrIngredient15)) Ingredients.Add($"{dr.StrMeasure15} - {dr.StrIngredient15}");
            if (!string.IsNullOrEmpty(dr.StrIngredient16)) Ingredients.Add($"{dr.StrMeasure16} - {dr.StrIngredient16}");
        }
    }
}
