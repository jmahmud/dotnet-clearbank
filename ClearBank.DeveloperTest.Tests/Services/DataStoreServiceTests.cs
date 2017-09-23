using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestFixture]
    public class DataStoreServiceTests
    {
        private DataStoreService _dataStoreService;

        [SetUp]
        public void Setup()
        {
            _dataStoreService = new DataStoreService();
        }

        [Test]
        public void GetAccountDataStore_BackupStore_ReturnsBackupStoreType()
        {
            //Arrange
            
            //Act
            var accountDataStore = _dataStoreService.GetAccountDataStore("Backup");

            //Assert
            Assert.That(accountDataStore, Is.TypeOf<BackupAccountDataStore>());
        }

        [Test]
        public void GetAccountDataStore_NonBackupStore_ReturnsAccountStoreType()
        {
            //Arrange

            //Act
            var accountDataStore = _dataStoreService.GetAccountDataStore("");

            //Assert
            Assert.That(accountDataStore, Is.TypeOf<AccountDataStore>());
        }
    }
}