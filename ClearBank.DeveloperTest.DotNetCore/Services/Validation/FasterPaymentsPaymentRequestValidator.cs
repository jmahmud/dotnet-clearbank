using ClearBank.DeveloperTest.Services.Validation;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.DotNetCore.Services.Validation
{
    public class FasterPaymentsPaymentRequestValidator : IPaymentRequestValidator
    {
        public bool Validate(MakePaymentRequest request, Account debtorAccount)
        {
            return request.PaymentScheme == PaymentScheme.FasterPayments && 
                   debtorAccount.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) &&
                   debtorAccount.Balance >= request.Amount;
        }
    }
}