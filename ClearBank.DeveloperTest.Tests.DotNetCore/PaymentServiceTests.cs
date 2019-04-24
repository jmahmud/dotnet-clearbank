using System;
using AutoFixture;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.DotNetCore
{
    public class PaymentServiceTests
    {
        private IPaymentService _sut;
        private Mock<IAccountService> _accountService;
        private Mock<IPaymentValidationService> _paymentValidationService;
        private Mock<IPaymentProcessService> _paymentProcessService;
        private IFixture _fixture;
        public PaymentServiceTests()
        {
            _fixture = new Fixture();
            _accountService = new Mock<IAccountService>();
            _paymentValidationService = new Mock<IPaymentValidationService>();
            _paymentProcessService = new Mock<IPaymentProcessService>();
            _sut = new PaymentService(_accountService.Object, _paymentValidationService.Object, _paymentProcessService.Object);
            
        }
        
        [Fact]
        public void MakePayment_EnsureAccountIsRetrievedFromAccountService()
        {
            //ARRANGE
            var request = _fixture.Create<MakePaymentRequest>();
            
            //ACT
            _sut.MakePayment(request);
            
            //ASSERT
            _accountService.Verify(x => x.GetAccount(request.DebtorAccountNumber), Times.Once);
        }

        [Fact]
        public void MakePayment_IfAccountIsNullReturnFalse()
        {
            //ARRANGE
            var request = _fixture.Create<MakePaymentRequest>();
            Account account = null;
            _accountService.Setup(x => x.GetAccount(request.DebtorAccountNumber)).Returns(account);
            
            //ACT
            var result = _sut.MakePayment(request);
            
            //ASSERT
            Assert.False(result.Success);
        }
        
        [Fact]
        public void MakePayment_EnsureAccountServiceIsUsedToUpdateAccount()
        {

            //ARRANGE
            var request = _fixture.Build<MakePaymentRequest>()
                .Create();
            
            var account = _fixture.Build<Account>()
                .With(x => x.AllowedPaymentSchemes, AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.FasterPayments)
                .Create();
            
            _accountService.Setup(x => x.GetAccount(request.DebtorAccountNumber)).Returns(account);
            _paymentValidationService.Setup(x => x.Validate(request, account)).Returns(true);

            var newBalance = _fixture.Create<int>();
            _paymentProcessService.Setup(x => x.CalculateBalance(request, account)).Returns(newBalance);
            
            
            //ACT
            var result = _sut.MakePayment(request);
            
            //ASSERT
            Assert.True(result.Success);
            _accountService.Verify(x => x.UpdateAccount(It.Is<Account>(a => a.Balance == newBalance)), Times.Once);
            _paymentValidationService.Verify(x => x.Validate(request, account), Times.Once);
            _paymentProcessService.Verify(x => x.CalculateBalance(request, account), Times.Once);
        }
       
    }
}