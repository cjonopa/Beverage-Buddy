using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Controllers;
using Beverage_Buddy.Data.Models;
using Beverage_Buddy.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Beverage_Buddy.Tests.DataTests
{
    public class DrinksControllerShould
    {
        private ICollection<Drink> Drinks { get; set; }
        private int Expected { get; set; }
        private Drink Drink1000 { get; set; }
        private Drink Drink2000 { get; set; }

        private readonly DrinksController drinksController;
        private readonly Mock<IRepository<Drink, string>> mockDrinkRepo;
        private readonly Mock<ILogger<DrinksController>> mockLogger;

        public DrinksControllerShould()
        {
            mockDrinkRepo = new Mock<IRepository<Drink, string>>();
            mockLogger = new Mock<ILogger<DrinksController>>();

            drinksController = new DrinksController(mockDrinkRepo.Object, mockLogger.Object);

            SetupDrinks();
        }

        private void SetupDrinks()
        {
            Drink1000 = new Drink {Id = "1000", DrinkName = "Test Drink 1", Instructions = "Test Instructions 1"};
            Drink2000 = new Drink {Id = "2000", DrinkName = "Test Drink 2", Instructions = "Test Instructions 2"};
            
            Drinks = new List<Drink> {Drink1000, Drink2000};

            Expected = Drinks.Count;
        }

        [Fact]
        public async void Get_An_ActionResult_As_Collection_Of_Drinks()
        {
            //-- Arrange

            //-- Act
            var result = await drinksController.Get();

            //-- Assert
            Assert.IsType<ActionResult<ICollection<Drink>>>(result);
        }

        [Fact]
        public async void Get_An_OkObjectResult()
        {
            //-- Arrange
                mockDrinkRepo.Setup(repo => repo.GetAllAsync())
                    .Returns(Task.FromResult(Drinks));

            //-- Act
            var action = await drinksController.Get();
            var result = action.Result;

            //-- Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Get_Exact_Number_Of_Drinks_In_Repo()
        {
            //-- Arrange
            mockDrinkRepo.Setup(repo => repo.GetAllAsync())
                .Returns(Task.FromResult(Drinks));

            //-- Act
            var action = await drinksController.Get();
            var result = action.Result;

            //-- Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actual = Assert.IsAssignableFrom<ICollection<Drink>>(okResult.Value);
            Assert.NotEmpty(actual);
            Assert.Equal(Expected, actual.Count);
        }

        [Fact]
        public async void Return_Requested_Drink()
        {
            //-- Arrange
            mockDrinkRepo.Setup(repo => repo.GetAsync("1000"))
                .Returns(Task.FromResult(Drink1000));

            //-- Act
            var action = await drinksController.Get("1000");
            var result = action.Result;

            //-- Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actual = Assert.IsType<Drink>(okResult.Value);
            Assert.NotNull(actual);
            Assert.NotEqual(Drink2000, actual);
            Assert.Equal(Drink1000, actual);
        }
    }
}
