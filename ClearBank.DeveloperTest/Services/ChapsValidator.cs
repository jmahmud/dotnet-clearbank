using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class ChapsValidator : IValidator
    {
        public bool IsValid(Account account, MakePaymentRequest request)
        {
            if (account == null) return false;
            
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
            {
                return false;
            }

            return account.Status == AccountStatus.Live;
        }
    }
}