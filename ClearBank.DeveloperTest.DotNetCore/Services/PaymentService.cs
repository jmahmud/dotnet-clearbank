using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using System.Configuration;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountService _accountService;
        private readonly IPaymentValidationService _paymentValidationService;

        public PaymentService(IAccountService accountService, IPaymentValidationService paymentValidationService)
        {
            _accountService = accountService;
            _paymentValidationService = paymentValidationService;
        }
        
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = _accountService.GetAccount(request.DebtorAccountNumber);
            
            var result = new MakePaymentResult();
            
            if (account != null && _paymentValidationService.Validate(request, account))
            {
                account.Balance -= request.Amount;
                _accountService.UpdateAccount(account);
                result.Success = true;
            }

            return result;
        }
    }
}
