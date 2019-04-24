using ClearBank.DeveloperTest.Services.Validation;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.DotNetCore.Services.Validation
{
    public class ChapsPaymentRequestValidator : IPaymentRequestValidator
    {
        public bool Validate(MakePaymentRequest request, Account debtorAccount)
        {
            return request.PaymentScheme == PaymentScheme.Chaps && 
                   debtorAccount.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) &&
                   debtorAccount.Status == AccountStatus.Live;
        }
    }
}