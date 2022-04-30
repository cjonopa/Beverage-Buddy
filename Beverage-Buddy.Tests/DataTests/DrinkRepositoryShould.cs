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

        private Drink Drink1 { get; set; }
        private Drink Drink2 { get; set; }
        private ICollection<Drink> Drinks { get; set; }
        private int Expected { get; set; }

        public DrinkRepositoryShould()
        {
            mockDrinkRepo = new Mock<IRepository<Drink, string>>();
            SetupDrinkList();
        }

        private void SetupDrinkList()
        {
            Drink1 = new Drink {Id = "1", DrinkName = "Drink 1"};
            Drink2 = new Drink {Id = "2", DrinkName = "Drink 2"};

            Drinks = new List<Drink>
            { Drink1, Drink2 };

            Expected = Drinks.Count;
        }

        [Fact]
        public async void Return_A_Collection_of_Drinks()
        {
            //-- Arrange
            mockDrinkRepo.Setup(mdr => mdr.GetAllAsync())
                .Returns(Task.FromResult(Drinks));
            var repo = mockDrinkRepo.Object;

            //-- Act
            var result = await repo.GetAllAsync();

            //-- Assert
            Assert.IsAssignableFrom<ICollection<Drink>>(result);
            Assert.Equal(Expected, result.Count);
        }

        [Fact]
        public async void Add_A_Drink()
        {
            //-- Arrange
            var drink3 = new Drink { Id = "3", DrinkName = "Drink 3" };
            mockDrinkRepo.Setup(mdr => mdr.Add(drink3))
                .Returns(drink3);
            mockDrinkRepo.Setup(mdr => mdr.SaveAllAsync())
                .Returns(Task.FromResult(true));

            var repo = mockDrinkRepo.Object;

            //-- Act
            var returned =repo.Add(drink3);
            var success = await repo.SaveAllAsync();

            //-- Assert
            Assert.True(success);
            Assert.IsType<Drink>(returned);
            Assert.Equal(drink3, returned);
        }

        [Fact]
        public async void Delete_A_Drink()
        {
            //-- Arrange
            mockDrinkRepo.Setup(mdr => mdr.Delete(Drink1.Id))
                .Returns(Drink1);
            mockDrinkRepo.Setup(mdr => mdr.SaveAllAsync())
                .Returns(Task.FromResult(true));

            var repo = mockDrinkRepo.Object;

            //-- Act
            var returned = repo.Delete(Drink1.Id);
            var success = await repo.SaveAllAsync();

            //-- Assert
            Assert.True(success);
            Assert.IsType<Drink>(returned);
            Assert.Equal(Drink1, returned);
        }

        [Fact]
        public async void Update_A_Drink()
        {
            //-- Arrange
            mockDrinkRepo.Setup(mdr => mdr.Update(Drink1))
                .Returns(Drink1);
            mockDrinkRepo.Setup(mdr => mdr.SaveAllAsync())
                .Returns(Task.FromResult(true));

            var repo = mockDrinkRepo.Object;

            //-- Act
            Drink1.Instructions = "Drink Instructions 1";
            var returned = repo.Update(Drink1);
            var success = await repo.SaveAllAsync();

            //-- Assert
            Assert.True(success);
            Assert.IsType<Drink>(returned);
            Assert.Equal(Drink1, returned);
        }
    }
}
