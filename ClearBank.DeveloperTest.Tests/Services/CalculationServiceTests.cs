using ClearBank.DeveloperTest.Services;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestFixture]
    public class CalculationServiceTests
    {
        [Test]
        public void GetDeductedBalance_GivenBalanceAndAmount_DeductsAmountFromBalance()
        {
            //Arrange
            var calculationService = new CalculationService();

            //Act
            var deductedBalance = calculationService.GetDeductedBalance(3, 2);

            //Assert
            Assert.That(deductedBalance, Is.EqualTo(1));
        }
    }
}