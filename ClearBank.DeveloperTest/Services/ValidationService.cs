using System.Collections.Generic;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class ValidationService : IValidationService
    {
        public ValidationService()
        {
            Validators = new Dictionary<string, IValidator>
            {
                { PaymentScheme.Bacs.ToString(), new BacsValidator() },
                { PaymentScheme.FasterPayments.ToString(), new FasterPaymentsValidator() },
                { PaymentScheme.Chaps.ToString(), new ChapsValidator()}
            };
        }

        public Dictionary<string, IValidator> Validators { get; set; }

        public bool IsRequestValid(Account account, MakePaymentRequest request)
        {
            IValidator validator;
            if (!Validators.TryGetValue(request.PaymentScheme.ToString(), out validator))
            {
                return false;
            }

            return validator.IsValid(account, request);
        }
    }
}