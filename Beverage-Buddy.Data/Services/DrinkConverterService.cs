using Beverage_Buddy.Data.APIs.CocktailDb.Models;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Data.Services.Interfaces;

namespace Beverage_Buddy.Data.Services
{
    public class DrinkConverterService : IConverterService<Drink, DrinkResult>
    {
        public Drink ConvertResult(Drink item, DrinkResult itemResult)
        {
            item.Id = itemResult.IdDrink;
            item.DrinkName = itemResult.StrDrink;
            item.DrinkAlternate = itemResult.StrAlternate;
            item.Tags = itemResult.StrTag;
            item.Video = itemResult.StrVideo;
            item.Category = itemResult.StrCategory;
            item.IBA = itemResult.StrIBA;
            item.Alcoholic = itemResult.StrAlcoholic?.ToLower() == "alcoholic";
            item.Glass = itemResult.StrGlass;
            item.Instructions = itemResult.StrInstructions;
            item.DrinkThumb = itemResult.StrDrinkThumb;
            AddIngredients(item, itemResult);
            item.ImageSource = itemResult.StrImageSource;
            item.ImageAttribution = itemResult.StrImageAttribution;
            item.CreativeCommonsConfirmed = itemResult.StrCreativeCommonsConfirmed?.ToLower() == "yes";
            item.Modified = itemResult.DateModified;

            return item;
        }

        private void AddIngredients(Drink item, DrinkResult itemResult)
        {
            if (!string.IsNullOrEmpty(itemResult.StrIngredient1))
                item.DrinkIngredients.Add(
                    new DrinkIngredient
                    {
                        Measure = itemResult.StrMeasure1, 
                        Name = itemResult.StrIngredient1, 
                        DrinkId = item.Id
                    });
            if (!string.IsNullOrEmpty(itemResult.StrIngredient2))
                item.DrinkIngredients.Add(
                    new DrinkIngredient
                    {
                        Measure = itemResult.StrMeasure2, 
                        Name = itemResult.StrIngredient2, 
                        DrinkId = item.Id
                    });
            if (!string.IsNullOrEmpty(itemResult.StrIngredient3))
                item.DrinkIngredients.Add(
                    new DrinkIngredient
                    {
                        Measure = itemResult.StrMeasure3, 
                        Name = itemResult.StrIngredient3, 
                        DrinkId = item.Id
                    });
            if (!string.IsNullOrEmpty(itemResult.StrIngredient4))
                item.DrinkIngredients.Add(
                    new DrinkIngredient
                    {
                        Measure = itemResult.StrMeasure4, 
                        Name = itemResult.StrIngredient4, 
                        DrinkId = item.Id
                    });
            if (!string.IsNullOrEmpty(itemResult.StrIngredient5))
                item.DrinkIngredients.Add(
                    new DrinkIngredient
                    {
                        Measure = itemResult.StrMeasure5, 
                        Name = itemResult.StrIngredient5, 
                        DrinkId = item.Id
                    });
            if (!string.IsNullOrEmpty(itemResult.StrIngredient6))
                item.DrinkIngredients.Add(
                    new DrinkIngredient
                    {
                        Measure = itemResult.StrMeasure6, 
                        Name = itemResult.StrIngredient6, 
                        DrinkId = item.Id
                    });
            if (!string.IsNullOrEmpty(itemResult.StrIngredient6))
                item.DrinkIngredients.Add(
                    new DrinkIngredient
                    {
                        Measure = itemResult.StrMeasure6, 
                        Name = itemResult.StrIngredient6, 
                        DrinkId = item.Id
                    });
            if (!string.IsNullOrEmpty(itemResult.StrIngredient7))
                item.DrinkIngredients.Add(
                    new DrinkIngredient
                    {
                        Measure = itemResult.StrMeasure7, 
                        Name = itemResult.StrIngredient7, 
                        DrinkId = item.Id
                    });
            if (!string.IsNullOrEmpty(itemResult.StrIngredient8))
                item.DrinkIngredients.Add(
                    new DrinkIngredient
                    {
                        Measure = itemResult.StrMeasure8, 
                        Name = itemResult.StrIngredient8, 
                        DrinkId = item.Id
                    });
            if (!string.IsNullOrEmpty(itemResult.StrIngredient9))
                item.DrinkIngredients.Add(
                    new DrinkIngredient
                    {
                        Measure = itemResult.StrMeasure9, 
                        Name = itemResult.StrIngredient9, 
                        DrinkId = item.Id
                    });
            if (!string.IsNullOrEmpty(itemResult.StrIngredient10))
                item.DrinkIngredients.Add(
                    new DrinkIngredient
                    {
                        Measure = itemResult.StrMeasure10, 
                        Name = itemResult.StrIngredient10, 
                        DrinkId = item.Id
                    });
            if (!string.IsNullOrEmpty(itemResult.StrIngredient11))
                item.DrinkIngredients.Add(
                    new DrinkIngredient
                    {
                        Measure = itemResult.StrMeasure11, 
                        Name = itemResult.StrIngredient11, 
                        DrinkId = item.Id
                    });
            if (!string.IsNullOrEmpty(itemResult.StrIngredient12))
                item.DrinkIngredients.Add(
                    new DrinkIngredient
                    {
                        Measure = itemResult.StrMeasure12, 
                        Name = itemResult.StrIngredient12, 
                        DrinkId = item.Id
                    });
            if (!string.IsNullOrEmpty(itemResult.StrIngredient13))
                item.DrinkIngredients.Add(
                    new DrinkIngredient
                    {
                        Measure = itemResult.StrMeasure13, 
                        Name = itemResult.StrIngredient13, 
                        DrinkId = item.Id
                    });
            if (!string.IsNullOrEmpty(itemResult.StrIngredient14))
                item.DrinkIngredients.Add(
                    new DrinkIngredient
                    {
                        Measure = itemResult.StrMeasure14, 
                        Name = itemResult.StrIngredient14, 
                        DrinkId = item.Id
                    });
            if (!string.IsNullOrEmpty(itemResult.StrIngredient15))
                item.DrinkIngredients.Add(
                    new DrinkIngredient
                    {
                        Measure = itemResult.StrMeasure15, 
                        Name = itemResult.StrIngredient15, 
                        DrinkId = item.Id
                    });
            if (!string.IsNullOrEmpty(itemResult.StrIngredient16))
                item.DrinkIngredients.Add(
                    new DrinkIngredient
                    {
                        Measure = itemResult.StrMeasure16, 
                        Name = itemResult.StrIngredient16, 
                        DrinkId = item.Id
                    });
        }
    }
}
