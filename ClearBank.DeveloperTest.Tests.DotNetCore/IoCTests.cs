using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.DotNetCore.Configuration;
using ClearBank.DeveloperTest.DotNetCore.Constants;
using ClearBank.DeveloperTest.DotNetCore.Services.Validation;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Services.Validation;
using ClearBank.DeveloperTest.Types;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.DotNetCore
{
    public class IoCTests : IDisposable
    {
        private ServiceCollection _sut;
        private IServiceProvider _serviceProvider;
        private IServiceScope _scope;
        public IoCTests()
        {
            _sut = new ServiceCollection();
        }
        
        private void InitialseContainer()
        {
            var serviceProvider = _sut.BuildServiceProvider();
            var serviceScopeFactory = serviceProvider.GetService<IServiceScopeFactory>();
            _scope = serviceScopeFactory.CreateScope();
            _serviceProvider = _scope.ServiceProvider;
        }

        [Fact]
        public void EnsureConfigurationRegistrationWorks()
        {
            //ARRANGE
            _sut.AddConfigurationRegistrations(Directory.GetCurrentDirectory(), "appsettings.json");
            InitialseContainer();
            
            //ACT
           var config = _serviceProvider.GetService<ClearBankConfiguration>();
           
           //ASSERT
           Assert.NotNull(config);
           Assert.True(config.DataStoreType == DataStoreType.Backup);
           
        }
        
        
        [Fact]
        public void EnsureBackupAccountDataStoreIsInjected()
        {
            //ARRANGE
            _sut.AddSingleton(new ClearBankConfiguration() {DataStoreType = DataStoreType.Backup});
            _sut.AddDataRegistrations();
            InitialseContainer();
            
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
            _sut.AddSingleton(new ClearBankConfiguration() {DataStoreType = DataStoreType.Unknown});
            _sut.AddDataRegistrations();
            InitialseContainer();
            
            //ACT
            var accountService = _serviceProvider.GetService<IAccountDataStore>();
           
            //ASSERT
            Assert.NotNull(accountService);
            Assert.IsType<AccountDataStore>(accountService);

        }
        
        
        [Fact]
        public void EnsureAccountServiceIsInjected()
        {
            //ARRANGE
            _sut.AddServiceRegistrations();
            _sut.AddDataRegistrations();
            _sut.AddConfigurationRegistrations(Directory.GetCurrentDirectory(), "appsettings.json");
            InitialseContainer();
            
            //ACT
            var accountService = _serviceProvider.GetService<IAccountService>();
           
            //ASSERT
            Assert.NotNull(accountService);
            Assert.IsType<AccountService>(accountService);
        }
        
        
        [Fact]
        public void EnsurePaymentValidationServiceIsInjected()
        {
            //ARRANGE
            _sut.AddServiceRegistrations();
            _sut.AddDataRegistrations();
            _sut.AddConfigurationRegistrations(Directory.GetCurrentDirectory(), "appsettings.json");
            InitialseContainer();
            
            //ACT
            var service = _serviceProvider.GetService<IPaymentValidationService>();
           
            //ASSERT
            Assert.NotNull(service);
            Assert.IsType<PaymentValidationService>(service);
        }

        [Fact]
        public void EnsurePaymentRequestValidatorsAreInjected()
        {
            //ARRANGE
            _sut.AddServiceRegistrations();
            _sut.AddDataRegistrations();
            _sut.AddConfigurationRegistrations(Directory.GetCurrentDirectory(), "appsettings.json");
            InitialseContainer();
            
            //ACT
            var validators = _serviceProvider.GetService<IDictionary<PaymentScheme, IPaymentRequestValidator>>();
           
            //ASSERT
            Assert.NotNull(validators);

            var bacsValidator = validators[PaymentScheme.Bacs];
            Assert.NotNull(bacsValidator);
            Assert.IsType<BacsPaymentRequestValidator>(bacsValidator);
            
            var fpValidator = validators[PaymentScheme.FasterPayments];
            Assert.NotNull(fpValidator);
            Assert.IsType<FasterPaymentsPaymentRequestValidator>(fpValidator);
            
            var chapsValidator = validators[PaymentScheme.Chaps];
            Assert.NotNull(chapsValidator);
            Assert.IsType<ChapsPaymentRequestValidator>(chapsValidator);
        }

        public void Dispose()
        {
            _scope?.Dispose();
        }
    }
}