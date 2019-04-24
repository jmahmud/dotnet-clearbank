using ClearBank.DeveloperTest.Services.Validation;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.DotNetCore.Services.Validation
{
    public class BacsPaymentRequestValidator : IPaymentRequestValidator
    {
        public bool Validate(MakePaymentRequest request, Account debtorAccount)
        {
            return request.PaymentScheme == PaymentScheme.Bacs && 
                   debtorAccount.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs);
        }
    }
}