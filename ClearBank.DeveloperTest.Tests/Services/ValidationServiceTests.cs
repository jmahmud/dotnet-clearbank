using System.Collections.Generic;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestFixture]
    public class ValidationServiceTests
    {
        private ValidationService _validationService;
        private Mock<IValidator> _validatorMock;

        [SetUp]
        public void Setup()
        {
            _validatorMock = new Mock<IValidator>();

            _validationService = new ValidationService
            {
                Validators = new Dictionary<string, IValidator>
                {
                    { PaymentScheme.Bacs.ToString(), _validatorMock.Object },
                    { PaymentScheme.FasterPayments.ToString(), _validatorMock.Object },
                    { PaymentScheme.Chaps.ToString(), _validatorMock.Object }
                }
            };
        }

        [Test]
        public void IsRequestValid_NoPaymentScheme_UsesDefaultValidator()
        {
            //Arrange            

            //Act
            _validationService.IsRequestValid(new Account(), new MakePaymentRequest());

            //Assert
            _validatorMock.Verify(x => x.IsValid(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>()), Times.Once);
        }

        [TestCase(PaymentScheme.FasterPayments)]
        [TestCase(PaymentScheme.Chaps)]
        [TestCase(PaymentScheme.Bacs)]
        public void IsRequestValid_SpecifiedPaymentScheme_UsesValidator(PaymentScheme paymentScheme)
        {
            //Arrange
            var makePaymentRequest = new MakePaymentRequest { PaymentScheme = paymentScheme};

            //Act
            _validationService.IsRequestValid(new Account(), makePaymentRequest);

            //Assert
            _validatorMock.Verify(x => x.IsValid(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>()), Times.Once);
        }
    }
}