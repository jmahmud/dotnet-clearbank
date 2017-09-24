using System.Configuration;

namespace ClearBank.DeveloperTest.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public string GetAppSettingForKey(string key)
        {
            var value = ConfigurationManager.AppSettings[key];

            return string.IsNullOrEmpty(value) ? string.Empty : value;
        }
    }
}