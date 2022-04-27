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

        public DrinkRepositoryShould()
        {
            mockDrinkRepo = new Mock<IRepository<Drink, string>>();
        }

        [Fact]
        public void Returns_A_Collection_of_Drinks()
        {
            //-- Arrange

            //-- Act

            //-- Assert
        }
    }
}
