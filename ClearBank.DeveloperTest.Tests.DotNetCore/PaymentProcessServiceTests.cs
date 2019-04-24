using AutoFixture;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.DotNetCore
{
    public class PaymentProcessServiceTests
    {
        private IPaymentProcessService _sut;
        private IFixture _fixture;
        
        public PaymentProcessServiceTests()
        {
            _fixture = new Fixture();
            _sut = new PaymentProcessService();
        }

        [Fact]
        public void CalculateBalance_ReturnsNewBalance()
        {
            //ARRANGE
            var request = _fixture.Build<MakePaymentRequest>()                
                .Create();
            
            var account = _fixture.Build<Account>()
                .Create();

            var balance = _sut.CalculateBalance(request, account);
            
            Assert.True(balance == account.Balance - request.Amount);
        }
    }
}