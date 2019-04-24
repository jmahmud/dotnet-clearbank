using System.Collections.Generic;
using AutoFixture;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Services.Validation;
using ClearBank.DeveloperTest.Types;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.DotNetCore
{
    public class PaymentValidationServiceTests
    {
        private IPaymentValidationService _sut;
        private IFixture _fixture;
        private Dictionary<PaymentScheme, IPaymentRequestValidator> _validators;
        
        public PaymentValidationServiceTests()
        {
            _fixture = new Fixture();
            _validators = new Dictionary<PaymentScheme, IPaymentRequestValidator>();
            
        }

        [Fact]
        public void ValidateReturnsTrueWhenAValidatorReturnsTrue()
        {
            //ARRANGE
            var request = _fixture.Build<MakePaymentRequest>()
                .With(x => x.PaymentScheme, PaymentScheme.Bacs)
                .Create();
            
            var account = _fixture.Build<Account>()
                .Create();

            var validatorTrue = new Mock<IPaymentRequestValidator>();
            validatorTrue.Setup(x => x.Validate(request, account)).Returns(true);
            _validators.Add(PaymentScheme.Bacs, validatorTrue.Object);
            
            var validatorFalse = new Mock<IPaymentRequestValidator>();
            validatorFalse.Setup(x => x.Validate(request, account)).Returns(false);
            _validators.Add(PaymentScheme.Chaps, validatorFalse.Object);

            _sut = new PaymentValidationService(_validators);
            
            //ACT
            var result = _sut.Validate(request, account);
            
            //ASSERT
            Assert.True(result);
            
        }
        
        
        [Fact]
        public void ValidateReturnsFalseWhenAllValidatorsReturnsFalse()
        {
            //ARRANGE
            var request = _fixture.Build<MakePaymentRequest>()
                .With(x => x.PaymentScheme, PaymentScheme.Bacs)
                .Create();
            
            var account = _fixture.Build<Account>()
                .Create();

            var validator1 = new Mock<IPaymentRequestValidator>();
            validator1.Setup(x => x.Validate(request, account)).Returns(false);
            _validators.Add(PaymentScheme.Bacs, validator1.Object);
            
            var validator2 = new Mock<IPaymentRequestValidator>();
            validator2.Setup(x => x.Validate(request, account)).Returns(false);
            _validators.Add(PaymentScheme.Chaps, validator2.Object);

            _sut = new PaymentValidationService(_validators);
            
            //ACT
            var result = _sut.Validate(request, account);
            
            //ASSERT
            Assert.False(result);   
        }
        
        [Fact]
        public void ValidateReturnsFalseWhenNoValidatorFound()
        {
            //ARRANGE
            var request = _fixture.Build<MakePaymentRequest>()
                .With(x => x.PaymentScheme, PaymentScheme.FasterPayments)
                .Create();
            
            var account = _fixture.Build<Account>()
                .Create();

            var validator1 = new Mock<IPaymentRequestValidator>();
            validator1.Setup(x => x.Validate(request, account)).Returns(true);
            _validators.Add(PaymentScheme.Bacs, validator1.Object);
            
            var validator2 = new Mock<IPaymentRequestValidator>();
            validator2.Setup(x => x.Validate(request, account)).Returns(true);
            _validators.Add(PaymentScheme.Chaps, validator2.Object);

            _sut = new PaymentValidationService(_validators);
            
            //ACT
            var result = _sut.Validate(request, account);
            
            //ASSERT
            Assert.False(result);
            
        }
    }
}