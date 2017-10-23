using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class FasterPaymentsValidator : IValidator
    {
        public virtual bool IsValid(Account account, MakePaymentRequest request)
        {
            if (account == null) return false;
            
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
            {
                return false;
            }

            return account.Balance >= request.Amount;
        }
    }
}