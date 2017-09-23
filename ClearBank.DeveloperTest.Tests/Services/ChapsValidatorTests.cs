using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestFixture]
    public class ChapsValidatorTests
    {
        private Account _account;
        private MakePaymentRequest _makePaymentRequest;
        private ChapsValidator _chapsValidator;

        [SetUp]
        public void Setup()
        {
            _account = new Account();
            _makePaymentRequest = new MakePaymentRequest();

            _chapsValidator = new ChapsValidator();
        }

        [TestCase(AllowedPaymentSchemes.Bacs)]
        [TestCase(AllowedPaymentSchemes.FasterPayments)]
        public void IsValid_NonChapsPaymentSchemes_ReturnsFalse(AllowedPaymentSchemes paymentSchemes)
        {
            //Arrange
            _account.AllowedPaymentSchemes = paymentSchemes;

            //Act
            var isValid = _chapsValidator.IsValid(_account, _makePaymentRequest);

            //Assert
            Assert.That(isValid, Is.False);
        }

        [TestCase(AccountStatus.Disabled)]
        [TestCase(AccountStatus.InboundPaymentsOnly)]
        public void IsValid_AccountStatusNotLive_ReturnsFalse(AccountStatus accountStatus)
        {
            //Arrange
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;
            _account.Status = accountStatus;

            //Act
            var isValid = _chapsValidator.IsValid(_account, _makePaymentRequest);

            //Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void IsValid_AccountStatusLive_ReturnsTrue()
        {
            //Arrange
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;
            _account.Status = AccountStatus.Live;

            //Act
            var isValid = _chapsValidator.IsValid(_account, _makePaymentRequest);

            //Assert
            Assert.That(isValid, Is.True);
        }
    }
}