using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IValidationService
    {
        bool IsRequestValid(Account account, MakePaymentRequest request);
    }
}