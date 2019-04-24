using AutoFixture;
using ClearBank.DeveloperTest.DotNetCore.Services.Validation;
using ClearBank.DeveloperTest.Services.Validation;
using ClearBank.DeveloperTest.Types;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.DotNetCore
{
    public class ChapsPaymentRequestValidatorTests
    {
        private IPaymentRequestValidator _sut;
        private IFixture _fixture;

        public ChapsPaymentRequestValidatorTests()
        {
            _fixture = new Fixture();
            _sut = new ChapsPaymentRequestValidator();
        }

        [Fact]
        public void Validate_ReturnsTrueIfDebtorAllowedChaps()
        {
            //ARRANGE
            var request = _fixture.Build<MakePaymentRequest>()
                .With(x => x.PaymentScheme, PaymentScheme.Chaps)
                .Create();
            
            var account = _fixture.Build<Account>()
                .With(x => x.AllowedPaymentSchemes, AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.FasterPayments)
                .With(x => x.Status, AccountStatus.Live)
                .Create();
            
            //ACT
            var result = _sut.Validate(request, account);

            //ASSERT
            Assert.True(result);
        }
        
        [Fact]
        public void Validate_ReturnsFalseIfDebtorIsNotAllowedChaps()
        {
            //ARRANGE
            var request = _fixture.Build<MakePaymentRequest>()
                .With(x => x.PaymentScheme, PaymentScheme.Chaps)
                .Create();
            
            var account = _fixture.Build<Account>()
                .With(x => x.AllowedPaymentSchemes, AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.FasterPayments)
                .With(x => x.Status, AccountStatus.Live)
                .Create();
            
            //ACT
            var result = _sut.Validate(request, account);

            //ASSERT
            Assert.False(result);
        }
        
        [Fact]
        public void Validate_ReturnsFalseIfRequestIsNotChaps()
        {
            //ARRANGE
            var request = _fixture.Build<MakePaymentRequest>()
                .With(x => x.PaymentScheme, PaymentScheme.Bacs)
                .Create();
            
            var account = _fixture.Build<Account>()
                .With(x => x.AllowedPaymentSchemes, AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.FasterPayments)
                .With(x => x.Status, AccountStatus.Live)
                .Create();
            
            //ACT
            var result = _sut.Validate(request, account);

            //ASSERT
            Assert.False(result);
        }
        
        
        [Theory]
        [InlineData(AccountStatus.Disabled)]
        [InlineData(AccountStatus.InboundPaymentsOnly)]
        public void Validate_ReturnsFalseIfAccountIsNotLive(AccountStatus status)
        {
            //ARRANGE
            var request = _fixture.Build<MakePaymentRequest>()
                .With(x => x.PaymentScheme, PaymentScheme.Chaps)
                .Create();
            
            var account = _fixture.Build<Account>()
                .With(x => x.AllowedPaymentSchemes, AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.FasterPayments)
                .With(x => x.Status, status)
                .Create();
            
            //ACT
            var result = _sut.Validate(request, account);

            //ASSERT
            Assert.False(result);
        }
    }
}