using System;
using System.IO;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.DotNetCore.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.DotNetCore
{
    public class IoCTests
    {
        private ServiceCollection _sut;
        private IServiceProvider _serviceProvider;
        public IoCTests()
        {
            _sut = new ServiceCollection();
        }

        [Fact]
        public void EnsureConfigurationRegistrationWorks()
        {
            _sut.AddConfigurationRegistrations(Directory.GetCurrentDirectory(), "appsettings.json");
            _serviceProvider = _sut.BuildServiceProvider();
            
           var config = _serviceProvider.GetService<ClearBankConfiguration>();
           
           Assert.NotNull(config);
           Assert.True(config.DataStoreType == "Backup");
           
        }
        
        
        [Fact]
        public void EnsureBackupAccountDataStoreIsInjected()
        {
            //ARRANGE
            _sut.AddSingleton(new ClearBankConfiguration() {DataStoreType = "Backup"});
            _sut.AddDataRegistrations();
            _serviceProvider = _sut.BuildServiceProvider();
            
            //ACT
            var accountService = _serviceProvider.GetService<IAccountDataStore>();
           
            //ASSERT
            Assert.NotNull(accountService);
            Assert.IsType<BackupAccountDataStore>(accountService);

        }
        
        [Fact]
        public void EnsureAccountDataStoreIsInjected()
        {
            //ARRANGE
            _sut.AddSingleton(new ClearBankConfiguration() {DataStoreType = "SOMETHIGNELSE"});
            _sut.AddDataRegistrations();
            _serviceProvider = _sut.BuildServiceProvider();
            
            //ACT
            var accountService = _serviceProvider.GetService<IAccountDataStore>();
           
            //ASSERT
            Assert.NotNull(accountService);
            Assert.IsType<AccountDataStore>(accountService);

        }
    }
}