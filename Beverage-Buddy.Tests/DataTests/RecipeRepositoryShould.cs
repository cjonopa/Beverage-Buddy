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

        private ICollection<Recipe> recipes;
        private int expected;

        public RecipeRepositoryShould()
        {
            mockRecipeRepo = new Mock<IRepository<Recipe, string>>();
            SetupDrinkList();
        }

        private void SetupDrinkList()
        {
            recipes = new List<Recipe>
            {
                new Recipe {Id = 1, Name = "Recipe 1"},
                new Recipe {Id = 2, Name = "Recipe 2"}
            };

            expected = 2;

            mockRecipeRepo.Setup(mdr => mdr.GetAllAsync())
                .Returns(Task.FromResult(recipes));
        }

        [Fact]
        public async void Return_A_Collection_of_Recipes()
        {
            //-- Arrange
            var repo = mockRecipeRepo.Object;

            //-- Act
            var result = await repo.GetAllAsync();

            //-- Assert
            Assert.IsAssignableFrom<ICollection<Recipe>>(result);
            Assert.Equal(expected, result.Count);
        }
    }
}

