using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestFixture]
    public class BacsValidatorTests
    {
        private Account _account;
        private MakePaymentRequest _makePaymentRequest;
        private BacsValidator _bacsValidator;

        [SetUp]
        public void Setup()
        {
            _account = new Account();
            _makePaymentRequest = new MakePaymentRequest();

            _bacsValidator = new BacsValidator();
        }

        [TestCase(AllowedPaymentSchemes.Chaps)]
        [TestCase(AllowedPaymentSchemes.FasterPayments)]
        public void IsValid_NotBacsPaymentSchemes_ReturnsFalse(AllowedPaymentSchemes allowedPaymentSchemes)
        {
            //Arrange
            _account.AllowedPaymentSchemes = allowedPaymentSchemes;
            
            //Act
            var isValid = _bacsValidator.IsValid(_account, _makePaymentRequest);

            //Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void IsValid_BacsPaymentSchemes_ReturnsTrue()
        {
            //Arrange
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs;

            //Act
            var isValid = _bacsValidator.IsValid(_account, _makePaymentRequest);

            //Assert
            Assert.That(isValid, Is.True);
        }
    }
}