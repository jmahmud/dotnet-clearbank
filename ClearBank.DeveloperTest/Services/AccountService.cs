using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDataStoreService _dataStoreService;
        private readonly ICalculationService _calculationService;
        private readonly string _dataStoreType;

        public AccountService(IDataStoreService dataStoreService, IConfigurationService configurationService, ICalculationService calculationService)
        {
            _dataStoreService = dataStoreService;
            _calculationService = calculationService;
            _dataStoreType = configurationService.DataStoreType();
        }

        public Account GetAccount(string accountNumber)
        {
            var accountDataStore = _dataStoreService.GetAccountDataStore(_dataStoreType);

            return accountDataStore.GetAccount(accountNumber);
        }

        public void UpdateAccount(Account account, MakePaymentRequest request)
        {
            var accountDataStore = _dataStoreService.GetAccountDataStore(_dataStoreType);

            account.Balance = _calculationService.GetDeductedBalance(account.Balance, request.Amount);

            accountDataStore.UpdateAccount(account);
        }
    }
}