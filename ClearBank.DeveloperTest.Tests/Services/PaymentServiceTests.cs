using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestFixture]
    public class PaymentServiceTests
    {
        private Mock<IAccountService> _accountServiceMock;
        private Mock<IValidationService> _validationServiceMock;
        private PaymentService _paymentService;

        [SetUp]
        public void Setup()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _validationServiceMock = new Mock<IValidationService>();
            _paymentService = new PaymentService(_accountServiceMock.Object, _validationServiceMock.Object);
        }

        [Test]
        public void MakePayment_ValidRequest_ReturnsTrue()
        {
            //Arrange
            _validationServiceMock.Setup(x => x.IsRequestValid(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>())).Returns(true);

            //Act
            var result = _paymentService.MakePayment(new MakePaymentRequest());

            //Assert
            Assert.That(result.Success, Is.True);
        }

        [Test]
        public void MakePayment_InvalidRequest_ReturnsFalse()
        {
            //Arrange
            _validationServiceMock.Setup(x => x.IsRequestValid(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>())).Returns(false);

            //Act
            var result = _paymentService.MakePayment(new MakePaymentRequest());

            //Assert
            Assert.That(result.Success, Is.False);
        }

        //MakePayment_ValidRequest_AccountIsUpdated
        [Test]
        public void MakePayment_ValidRequest_AccountIsUpdated()
        {
            //Arrange
            _validationServiceMock.Setup(x => x.IsRequestValid(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>())).Returns(true);

            //Act
            _paymentService.MakePayment(new MakePaymentRequest());

            //Assert
            _accountServiceMock.Verify(x => x.UpdateAccount(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>()), Times.Once);
        }
    }
}