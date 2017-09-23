using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountService _accountService;
        private readonly IValidationService _validationService;

        public PaymentService(IAccountService accountService, IValidationService validationService)
        {
            _accountService = accountService;
            _validationService = validationService;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = _accountService.GetAccount(request.DebtorAccountNumber);

            var result = new MakePaymentResult();

            if (_validationService.IsRequestValid(account, request))
            {
                _accountService.UpdateAccount(account, request);
                result.Success = true;
            }

            return result;
        }
    }
}
