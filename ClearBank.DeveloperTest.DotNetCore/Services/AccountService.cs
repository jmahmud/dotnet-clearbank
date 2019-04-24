using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDataStore _accountDataStore;

        public AccountService(IAccountDataStore accountDataStore)
        {
            _accountDataStore = accountDataStore;
        }
        
        public Account GetAccount(string accountNumber)
        {
            return _accountDataStore.GetAccount(accountNumber);
        }

        public void UpdateAccount(Account account)
        {
            _accountDataStore.UpdateAccount(account);
        }
    }
}