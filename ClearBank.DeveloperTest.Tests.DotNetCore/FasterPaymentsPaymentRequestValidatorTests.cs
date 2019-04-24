using AutoFixture;
using ClearBank.DeveloperTest.DotNetCore.Services.Validation;
using ClearBank.DeveloperTest.Services.Validation;
using ClearBank.DeveloperTest.Types;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.DotNetCore
{
    public class FasterPaymentsPaymentRequestValidatorTests
    {
        private IPaymentRequestValidator _sut;
        private IFixture _fixture;

        public FasterPaymentsPaymentRequestValidatorTests()
        {
            _fixture = new Fixture();
            _sut = new FasterPaymentsPaymentRequestValidator();
        }

        [Fact]
        public void Validate_ReturnsTrueIfDebtorAllowedFasterPayments()
        {
            //ARRANGE
            var request = _fixture.Build<MakePaymentRequest>()
                .With(x => x.PaymentScheme, PaymentScheme.FasterPayments)
                .With(x => x.Amount, 10)
                .Create();
            
            var account = _fixture.Build<Account>()
                .With(x => x.AllowedPaymentSchemes, AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.FasterPayments | AllowedPaymentSchemes.FasterPayments)
                .With(x => x.Balance, 100)
                .Create();
            
            //ACT
            var result = _sut.Validate(request, account);

            //ASSERT
            Assert.True(result);
        }
        
        [Fact]
        public void Validate_ReturnsFalseIfDebtorIsNotAllowedFasterPayments()
        {
            //ARRANGE
            var request = _fixture.Build<MakePaymentRequest>()
                .With(x => x.PaymentScheme, PaymentScheme.FasterPayments)
                .With(x => x.Amount, 10)
                .Create();
            
            var account = _fixture.Build<Account>()
                .With(x => x.AllowedPaymentSchemes, AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps)
                .With(x => x.Balance, 100)
                .Create();
            
            //ACT
            var result = _sut.Validate(request, account);

            //ASSERT
            Assert.False(result);
        }
        
        [Fact]
        public void Validate_ReturnsFalseIfRequestIsNotFasterPayments()
        {
            //ARRANGE
            var request = _fixture.Build<MakePaymentRequest>()
                .With(x => x.PaymentScheme, PaymentScheme.Bacs)
                .With(x => x.Amount, 10)
                .Create();
            
            var account = _fixture.Build<Account>()
                .With(x => x.AllowedPaymentSchemes, AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.FasterPayments)
                .With(x => x.Balance, 100)
                .Create();
            
            //ACT
            var result = _sut.Validate(request, account);

            //ASSERT
            Assert.False(result);
        }


        [Fact] 
        public void Validate_ReturnsFalseIfAmountIsGreaterThanBalance()
        {
            //ARRANGE
            var request = _fixture.Build<MakePaymentRequest>()
                .With(x => x.PaymentScheme, PaymentScheme.FasterPayments)
                .With(x => x.Amount, 100)
                .Create();
            
            var account = _fixture.Build<Account>()
                .With(x => x.AllowedPaymentSchemes, AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.FasterPayments)
                .With(x => x.Balance, 10)
                .Create();
            
            //ACT
            var result = _sut.Validate(request, account);

            //ASSERT
            Assert.False(result);
        }
    }
}