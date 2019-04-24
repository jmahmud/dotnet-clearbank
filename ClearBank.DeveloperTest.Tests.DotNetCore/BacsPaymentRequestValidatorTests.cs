using AutoFixture;
using ClearBank.DeveloperTest.DotNetCore.Services.Validation;
using ClearBank.DeveloperTest.Services.Validation;
using ClearBank.DeveloperTest.Types;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.DotNetCore
{
    public class BacsPaymentRequestValidatorTests
    {
        private IPaymentRequestValidator _sut;
        private IFixture _fixture;

        public BacsPaymentRequestValidatorTests()
        {
            _fixture = new Fixture();
            _sut = new BacsPaymentRequestValidator();
            
        }

        [Fact]
        public void Validate_ReturnsTrueIfDebtorAllowedBacs()
        {
            //ARRANGE
            var request = _fixture.Build<MakePaymentRequest>()
                .With(x => x.PaymentScheme, PaymentScheme.Bacs)
                .Create();
            
            var account = _fixture.Build<Account>()
                .With(x => x.AllowedPaymentSchemes, AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.FasterPayments)
                .Create();
            
            //ACT
            var result = _sut.Validate(request, account);

            //ASSERT
            Assert.True(result);
        }
        
        [Fact]
        public void Validate_ReturnsFalseIfDebtorIsNotAllowedBacs()
        {
            //ARRANGE
            var request = _fixture.Build<MakePaymentRequest>()
                .With(x => x.PaymentScheme, PaymentScheme.Bacs)
                .Create();
            
            var account = _fixture.Build<Account>()
                .With(x => x.AllowedPaymentSchemes, AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.FasterPayments)
                .Create();
            
            //ACT
            var result = _sut.Validate(request, account);

            //ASSERT
            Assert.False(result);
        }
        
        [Fact]
        public void Validate_ReturnsFalseIfRequestIsNotBacs()
        {
            //ARRANGE
            var request = _fixture.Build<MakePaymentRequest>()
                .With(x => x.PaymentScheme, PaymentScheme.Chaps)
                .Create();
            
            var account = _fixture.Build<Account>()
                .With(x => x.AllowedPaymentSchemes, AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.FasterPayments)
                .Create();
            
            //ACT
            var result = _sut.Validate(request, account);

            //ASSERT
            Assert.False(result);
        }
    }
}