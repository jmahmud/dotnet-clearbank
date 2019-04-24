using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.Validation
{
    public interface IPaymentRequestValidator
    {
        bool Validate(MakePaymentRequest request, Account debtorAccount);
    }
}