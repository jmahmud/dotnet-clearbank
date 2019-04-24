using ClearBank.DeveloperTest.Services;

namespace ClearBank.DeveloperTest.Tests.DotNetCore
{
    public class AccountServiceTests
    {
        private IAccountService _sut;
        
        public AccountServiceTests()
        {
            _sut = new AccountService();
        }
    }
}