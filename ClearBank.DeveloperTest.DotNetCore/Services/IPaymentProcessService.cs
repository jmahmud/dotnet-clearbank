using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IPaymentProcessService
    {
        decimal CalculateBalance(MakePaymentRequest request, Account account);
    }
}