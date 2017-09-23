using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestFixture]
    public class AccountServiceTests
    {
        private Mock<IDataStoreService> _dataStoreServiceMock;
        private Mock<IConfigurationService> _configurationServiceMock;
        private Mock<ICalculationService> _calculationServiceMock;
        private AccountService _accountService;

        [SetUp]
        public void Setup()
        {
            _dataStoreServiceMock = new Mock<IDataStoreService>();
            _configurationServiceMock = new Mock<IConfigurationService>();
            _calculationServiceMock = new Mock<ICalculationService>();

            _accountService = new AccountService(_dataStoreServiceMock.Object, _configurationServiceMock.Object, _calculationServiceMock.Object);
        }

        [Test]
        public void GetAccount_GetsAccountFromStore()
        {
            //Arrange
            var dataStoreMock = new Mock<IAccountDataStore>();

            _dataStoreServiceMock.Setup(x => x.GetAccountDataStore(It.IsAny<string>())).Returns(() => dataStoreMock.Object);
            
            //Act
            _accountService.GetAccount(string.Empty);

            //Assert
            dataStoreMock.Verify(x => x.GetAccount(string.Empty), Times.Once);
        }

        [Test]
        public void UpdateAccount_GetsNewBalance()
        {
            //Arrange
            var dataStoreMock = new Mock<IAccountDataStore>();

            _dataStoreServiceMock.Setup(x => x.GetAccountDataStore(It.IsAny<string>())).Returns(() => dataStoreMock.Object);

            var account = new Account { Balance = 2};
            var makePaymentRequest = new MakePaymentRequest { Amount = 1};

            //Act
            _accountService.UpdateAccount(account, makePaymentRequest);

            //Assert
            _calculationServiceMock.Verify(x => x.GetDeductedBalance(2, 1), Times.Once);
        }

        [Test]
        public void UpdateAccount_UpdatesAccountWithNewBalance()
        {
            //Arrange
            var dataStoreMock = new Mock<IAccountDataStore>();

            _dataStoreServiceMock.Setup(x => x.GetAccountDataStore(It.IsAny<string>())).Returns(() => dataStoreMock.Object);
            _calculationServiceMock.Setup(x => x.GetDeductedBalance(It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(1);

            //Act
            _accountService.UpdateAccount(new Account(), new MakePaymentRequest());

            //Assert
            dataStoreMock.Verify(x => x.UpdateAccount(It.Is<Account>(a => a.Balance == 1)));
        }
    }
}