using System;
using ClearBank.DeveloperTest.Services;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.DotNetCore
{
    public class PaymentServiceTests
    {
        private IPaymentService _sut;
        
        public PaymentServiceTests()
        {
            _sut = new PaymentService();
        }
        
        [Fact]
        public void Test1()
        {
            
        }
    }
}