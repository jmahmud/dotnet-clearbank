using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestFixture]
    public class FasterPaymentsValidatorTests
    {
        private FasterPaymentsValidator _fasterPaymentsValidator;
        private Account _account;
        private MakePaymentRequest _makePaymentRequest;

        [SetUp]
        public void Setup()
        {
            _account = new Account();
            _makePaymentRequest = new MakePaymentRequest();

            _fasterPaymentsValidator = new FasterPaymentsValidator();
        }

        [TestCase(AllowedPaymentSchemes.Bacs)]
        [TestCase(AllowedPaymentSchemes.Chaps)]
        public void IsValid_NonPaymentFasterPaymentsSchemes_ReturnsFalse(AllowedPaymentSchemes paymentSchemes)
        {
            //Arrange
            _account.AllowedPaymentSchemes = paymentSchemes;

            //Act
            var isValid = _fasterPaymentsValidator.IsValid(_account, _makePaymentRequest);

            //Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void IsValid_BalanceHigherThanAmount_ReturnsTrue()
        {
            //Arrange
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;
            _account.Balance = 2;
            _makePaymentRequest.Amount = 1;

            //Act
            var isValid = _fasterPaymentsValidator.IsValid(_account, _makePaymentRequest);

            //Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void IsValid_BalanceLowerThanAmount_ReturnsFalse()
        {
            //Arrange
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;
            _account.Balance = 1;
            _makePaymentRequest.Amount = 2;

            //Act
            var isValid = _fasterPaymentsValidator.IsValid(_account, _makePaymentRequest);

            //Assert
            Assert.That(isValid, Is.False);
        }
    }
}