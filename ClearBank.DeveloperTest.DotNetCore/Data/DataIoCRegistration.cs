using System;
using ClearBank.DeveloperTest.DotNetCore.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClearBank.DeveloperTest.Data
{
    public static class DataIoCRegistration
    {
        public static void AddDataRegistrations(this ServiceCollection collection)
        {
            //We shall add a si§    §
            //ngleton but use the configuration service to resolve the datastore type
            collection.AddSingleton<IAccountDataStore>(delegate(IServiceProvider provider)
            {
                var configuration = provider.GetService<ClearBankConfiguration>();
                
                if (configuration.DataStoreType == "Backup")
                {
                    return new BackupAccountDataStore();
                }
                return new AccountDataStore();

            });
        }
    }
}