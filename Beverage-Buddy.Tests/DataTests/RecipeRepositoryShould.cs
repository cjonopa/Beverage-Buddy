using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Data.Repositories;
using Moq;
using Xunit;

namespace Beverage_Buddy.Tests.DataTests
{
    public class RecipeRepositoryShould
    {
        private readonly Mock<IRepository<Recipe, string>> mockRecipeRepo;
        private Recipe Recipe1 { get; set; }
        private Recipe Recipe2 { get; set; }
        private ICollection<Recipe> Recipes { get; set; }
        private int ExpectedCount { get; set; }

        public RecipeRepositoryShould()
        {
            mockRecipeRepo = new Mock<IRepository<Recipe, string>>();
            SetupDrinkList();
        }

        private void SetupDrinkList()
        {
            Recipe1 = new Recipe { Id = 1, Name = "Recipe 1", DrinkType = DrinkType.Water };
            Recipe2 = new Recipe { Id = 2, Name = "Recipe 2", DrinkType = DrinkType.Beer };

            Recipes = new List<Recipe>
            { Recipe1, Recipe2 };

            ExpectedCount = 2;
        }

        [Fact]
        public async void Return_A_Collection_of_Recipes()
        {
            //-- Arrange
            mockRecipeRepo.Setup(mdr => mdr.GetAllAsync())
              .Returns(Task.FromResult(Recipes));
            var repo = mockRecipeRepo.Object;

            //-- Act
            var result = await repo.GetAllAsync();

            //-- Assert
            Assert.IsAssignableFrom<ICollection<Recipe>>(result);
            Assert.Equal(ExpectedCount, result.Count);
        }

        [Fact]
        public async void Add_A_Recipe_To_The_Repsitory()
        {
            //-- Arrange
            var recipe = new Recipe { Id = 3, Name = "Recipe 3" };
            mockRecipeRepo.Setup(mdr => mdr.Add(recipe))
                .Returns(recipe);
            mockRecipeRepo.Setup(mdr => mdr.SaveAllAsync())
                .Returns(Task.FromResult(true));

            var repo = mockRecipeRepo.Object;

            //-- Act
            var returned = repo.Add(recipe);
            var success = await repo.SaveAllAsync();

            //-- Assert
            Assert.True(success);
            Assert.IsType<Recipe>(returned);
            Assert.Equal(recipe, returned);
        }

        [Fact]
        public async void Delete_A_Recipe_To_The_Repsitory()
        {
            //-- Arrange
            mockRecipeRepo.Setup(mdr => mdr.Delete("Recipe1"))
                .Returns(Recipe1);
            mockRecipeRepo.Setup(mdr => mdr.SaveAllAsync())
                .Returns(Task.FromResult(true));

            var repo = mockRecipeRepo.Object;

            //-- Act
            var returned = repo.Delete("Recipe1");
            var success = await repo.SaveAllAsync();

            //-- Assert
            Assert.True(success);
            Assert.IsType<Recipe>(returned);
            Assert.Equal(Recipe1, returned);
        }

        [Fact]
        public async void Update_A_Recipe_To_The_Repsitory()
        {
            {
                //-- Arrange
                mockRecipeRepo.Setup(mdr => mdr.Update(Recipe1))
                    .Returns(Recipe1);
                mockRecipeRepo.Setup(mdr => mdr.SaveAllAsync())
                    .Returns(Task.FromResult(true));

                var repo = mockRecipeRepo.Object;

                //-- Act
                Recipe1.DrinkType = DrinkType.Wine;
                var returned = repo.Update(Recipe1);
                var success = await repo.SaveAllAsync();

                //-- Assert
                Assert.True(success);
                Assert.IsType<Recipe>(returned);
                Assert.Equal(Recipe1, returned);
            }
        }
    }
}

