using System.Collections.Generic;
using System.Linq;
using ClearBank.DeveloperTest.Services.Validation;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentValidationService : IPaymentValidationService
    {
        private readonly IDictionary<PaymentScheme, IPaymentRequestValidator> _paymentRequestValidators;

        public PaymentValidationService(IDictionary<PaymentScheme, IPaymentRequestValidator> paymentRequestValidators)
        {
            _paymentRequestValidators = paymentRequestValidators;
        }
        public bool Validate(MakePaymentRequest request, Account debtorAccount)
        {
            if (_paymentRequestValidators.TryGetValue(request.PaymentScheme, out var validator))
            {
                return validator.Validate(request, debtorAccount);
            }

            return false;
            
        }
    }
}