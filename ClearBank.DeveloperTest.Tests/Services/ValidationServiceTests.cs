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
        private Mock<BacsValidator> _bacsValidatorMock;
        private Mock<FasterPaymentsValidator> _fasterPaymentsValidatorMoq;
        private Mock<ChapsValidator> _chapsValidatorMoq;
        
        [SetUp]
        public void Setup()
        {
            _bacsValidatorMock = new Mock<BacsValidator>();
            _fasterPaymentsValidatorMoq = new Mock<FasterPaymentsValidator>();
            _chapsValidatorMoq = new Mock<ChapsValidator>();

            _validationService = new ValidationService
            {
                Validators = new Dictionary<PaymentScheme, IValidator>
                {
                    { PaymentScheme.Bacs, _bacsValidatorMock.Object },
                    { PaymentScheme.FasterPayments, _fasterPaymentsValidatorMoq.Object },
                    { PaymentScheme.Chaps, _chapsValidatorMoq.Object }
                }
            };
        }

        [Test]
        public void IsRequestValid_NoPaymentScheme_UsesFasterPaymentsValidator()
        {
            //Arrange            

            //Act
            _validationService.IsRequestValid(new Account(), new MakePaymentRequest());

            //Assert
            _fasterPaymentsValidatorMoq.Verify(x => x.IsValid(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>()), Times.Once);
        }

        [Test]
        public void IsRequestValid_FasterPayments_UsesFasterPaymentsValidator()
        {
            //Arrange
            var makePaymentRequest = new MakePaymentRequest { PaymentScheme = PaymentScheme.FasterPayments};

            //Act
            _validationService.IsRequestValid(new Account(), makePaymentRequest);

            //Assert
            _fasterPaymentsValidatorMoq.Verify(x => x.IsValid(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>()), Times.Once);
        }

        [Test]
        public void IsRequestValid_Chaps_UsesChapsValidator()
        {
            //Arrange
            var makePaymentRequest = new MakePaymentRequest { PaymentScheme = PaymentScheme.Chaps };

            //Act
            _validationService.IsRequestValid(new Account(), makePaymentRequest);

            //Assert
            _chapsValidatorMoq.Verify(x => x.IsValid(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>()), Times.Once);
        }

        [Test]
        public void IsRequestValid_Bacs_UsesBacsValidator()
        {
            //Arrange
            var makePaymentRequest = new MakePaymentRequest { PaymentScheme = PaymentScheme.Bacs };

            //Act
            _validationService.IsRequestValid(new Account(), makePaymentRequest);

            //Assert
            _bacsValidatorMock.Verify(x => x.IsValid(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>()), Times.Once);
        }
    }
}