using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class ChapsValidator : IValidator
    {
        public virtual bool IsValid(Account account, MakePaymentRequest request)
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