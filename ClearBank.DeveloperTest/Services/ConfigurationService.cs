using System.Configuration;

namespace ClearBank.DeveloperTest.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public string DataStoreType()
        {
            var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

            return string.IsNullOrEmpty(dataStoreType) ? string.Empty : dataStoreType;
        }
    }
}