using AutoFixture;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.DotNetCore
{
    public class AccountServiceTests
    {
        private IAccountService _sut;
        private Mock<IAccountDataStore> _accountDataStore;
        private IFixture _fixture; 
        
        public AccountServiceTests()
        {
            _fixture = new Fixture();
            _accountDataStore = new Mock<IAccountDataStore>();
            _sut = new AccountService(_accountDataStore.Object);
        }

        [Fact]
        public void GetAccount_WhenCalledReturnsFromDataStore()
        {
            //ARRANGE
            var accountId = _fixture.Create<string>();
            
            //ACT
            var account = _sut.GetAccount(accountId);

            //ASSERT
            _accountDataStore.Verify(x => x.GetAccount(accountId), Times.Once);
        }
        
        
        [Fact]
        public void UpdateAccount_WhenCalledCallsDataStore()
        {
            //ARRANGE
            var account = _fixture.Create<Account>();
            
            //ACT
            _sut.UpdateAccount(account);

            //ASSERT
            _accountDataStore.Verify(x => x.UpdateAccount(account), Times.Once);
        }
    }
}