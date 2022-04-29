using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Data.Repositories;
using Moq;
using Xunit;

namespace Beverage_Buddy.Tests.DataTests
{
    public class DrinkRepositoryShould
    {
        private readonly Mock<IRepository<Drink, string>> mockDrinkRepo;

        private ICollection<Drink> drinks;
        private int expected;

        public DrinkRepositoryShould()
        {
            mockDrinkRepo = new Mock<IRepository<Drink, string>>();
            SetupDrinkList();
        }

        private void SetupDrinkList()
        {
            drinks = new List<Drink>
            {
                new Drink {Id = "1", DrinkName = "Drink 1"},
                new Drink {Id = "2", DrinkName = "Drink 2"}
            };

            expected = 2;
        }

        [Fact]
        public async void Return_A_Collection_of_Drinks()
        {
            //-- Arrange
            mockDrinkRepo.Setup(mdr => mdr.GetAllAsync())
                .Returns(Task.FromResult(drinks));
            var repo = mockDrinkRepo.Object;

            //-- Act
            var result = await repo.GetAllAsync();

            //-- Assert
            Assert.IsAssignableFrom<ICollection<Drink>>(result);
            Assert.Equal(expected, result.Count);
        }

        [Fact]
        public async void Add_A_Drink()
        {
            //-- Arrange
            mockDrinkRepo.Setup(mdr => mdr.GetAllAsync())
                .Returns(Task.FromResult(drinks));
            var drink = new Drink { Id = "3", DrinkName = "Drink 3" };
            var repo = mockDrinkRepo.Object;

            //-- Act
            var result = await repo.GetAllAsync();
            result.Add(drink);

            //-- Assert
            Assert.IsAssignableFrom<ICollection<Drink>>(result);
            Assert.Equal(expected + 1, result.Count);
        }

        [Fact]
        public async void Add_Complex_Drink()
        {
            //-- Arrange
            mockDrinkRepo.Setup(mdr => mdr.GetAllAsync())
                .Returns(Task.FromResult(drinks));
            List<DrinkIngredient> ingredients = new List<DrinkIngredient>();
            DrinkIngredient ingredient1 = new DrinkIngredient();
            DrinkIngredient ingredient2 = new DrinkIngredient();

            ingredients.Add(ingredient1);
            ingredients.Add(ingredient2);

            var drink = new Drink { Id = "3", DrinkName = "Drink 3", Alcoholic = true, Category = "Odd Drink", DrinkIngredients = ingredients };
            var repo = mockDrinkRepo.Object;

            //-- Act
            var result = await repo.GetAllAsync();
            result.Add(drink);

            //-- Assert
            Assert.IsAssignableFrom<ICollection<Drink>>(result);
            Assert.Equal(expected + 1, result.Count);
        }

        [Fact]
        public async void Add_Multiple_Drinks()
        {
            //-- Arrange
            mockDrinkRepo.Setup(mdr => mdr.GetAllAsync())
                .Returns(Task.FromResult(drinks));
            var drink1 = new Drink { Id = "3", DrinkName = "Drink 3" };
            var drink2 = new Drink { Id = "4", DrinkName = "Drink 4" };
            var repo = mockDrinkRepo.Object;

            //-- Act
            var result = await repo.GetAllAsync();
            result.Add(drink1);
            result.Add(drink2);


            //-- Assert
            Assert.IsAssignableFrom<ICollection<Drink>>(result);
            Assert.Equal(expected + 2, result.Count);
        }
       
    }
}
