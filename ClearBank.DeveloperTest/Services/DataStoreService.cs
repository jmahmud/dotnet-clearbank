using ClearBank.DeveloperTest.Data;

namespace ClearBank.DeveloperTest.Services
{
    public class DataStoreService : IDataStoreService
    {
        public IAccountDataStore GetAccountDataStore(string dataStoreType)
        {
            if (dataStoreType == "Backup")
            {
                return new BackupAccountDataStore();
            }

            return new AccountDataStore();
        }
    }
}
