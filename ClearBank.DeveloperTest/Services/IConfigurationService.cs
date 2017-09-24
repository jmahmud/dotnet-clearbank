namespace ClearBank.DeveloperTest.Services
{
    public interface IConfigurationService
    {
        string GetAppSettingForKey(string key);
    }
}