using System;
using System.IO;
using ClearBank.DeveloperTest.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClearBank.DeveloperTest.DotNetCore.Configuration
{
    public static class ConfigurationIoCRegistration
    {
        public static void AddConfigurationRegistrations(this ServiceCollection collection, string currentDirectory, string filename)
        {
            collection.AddTransient(delegate(IServiceProvider provider)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(currentDirectory)
                    .AddJsonFile(filename);
            
                var config = builder.Build();
            
                var clearBankConfig = new ClearBankConfiguration();
                config.GetSection("ClearBank").Bind(clearBankConfig);
                return clearBankConfig;
            });
            
            
        }
    }
}