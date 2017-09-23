using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IValidator
    {
        bool IsValid(Account account, MakePaymentRequest request);
    }
}