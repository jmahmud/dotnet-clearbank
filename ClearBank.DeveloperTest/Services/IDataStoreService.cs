using ClearBank.DeveloperTest.Data;

namespace ClearBank.DeveloperTest.Services
{
    public interface IDataStoreService
    {
        IAccountDataStore GetAccountDataStore(string dataStoreType);
    }
}