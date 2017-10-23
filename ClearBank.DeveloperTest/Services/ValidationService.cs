using System.Collections.Generic;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class ValidationService : IValidationService
    {
        public ValidationService()
        {
            Validators = new Dictionary<PaymentScheme, IValidator>
            {
                { PaymentScheme.Bacs, new BacsValidator() },
                { PaymentScheme.FasterPayments, new FasterPaymentsValidator() },
                { PaymentScheme.Chaps, new ChapsValidator()}
            };
        }

        public Dictionary<PaymentScheme, IValidator> Validators { get; set; }

        public bool IsRequestValid(Account account, MakePaymentRequest request)
        {
            IValidator validator;
            if (!Validators.TryGetValue(request.PaymentScheme, out validator))
            {
                return false;
            }

            return validator.IsValid(account, request);
        }
    }
}