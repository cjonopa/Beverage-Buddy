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
                new Recipe {Id = 1, Name = "Recipe 1", DrinkType = DrinkType.Beer},
                new Recipe {Id = 2, Name = "Recipe 2", DrinkType = DrinkType.Beer},
            };

            expected = 2;
        }

        [Fact]
        public async void Return_A_Collection_of_Recipes()
        {
            //-- Arrange
            mockRecipeRepo.Setup(mdr => mdr.GetAllAsync())
              .Returns(Task.FromResult(recipes));
            var repo = mockRecipeRepo.Object;

            //-- Act
            var result = await repo.GetAllAsync();

            //-- Assert
            Assert.IsAssignableFrom<ICollection<Recipe>>(result);
            Assert.Equal(expected, result.Count);
        }

        [Fact]
        public async void Add_A_Recipe_To_The_Collection()
        {
            //-- Arrange
            var AddRecipe = new Recipe { Id = 3, Name = "Recipe 3", DrinkType = DrinkType.Beer};
            var repo = mockRecipeRepo.Object;

            //-- Act
            try
            {
                repo.Add(AddRecipe);
            }
            catch 
            {
                Assert.False(false);
            }
        }
    }
}

