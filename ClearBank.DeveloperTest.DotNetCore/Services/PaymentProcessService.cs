using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentProcessService : IPaymentProcessService
    {
        public decimal CalculateBalance(MakePaymentRequest request, Account account)
        {
            return account.Balance - request.Amount;
        }
    }
}