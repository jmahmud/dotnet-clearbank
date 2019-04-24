using ClearBank.DeveloperTest.Types;
using Microsoft.Extensions.Options;

namespace ClearBank.DeveloperTest.Services
{
    public interface IPaymentValidationService
    {
        bool Validate(MakePaymentRequest request, Account debtorAccount);
    }
}