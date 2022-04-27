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
    }
}
