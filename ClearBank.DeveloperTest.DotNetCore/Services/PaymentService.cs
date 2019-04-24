using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using System.Configuration;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountService _accountService;
        private readonly IPaymentValidationService _paymentValidationService;
        private readonly IPaymentProcessService _paymentProcessService;

        public PaymentService(IAccountService accountService, IPaymentValidationService paymentValidationService, IPaymentProcessService paymentProcessService)
        {
            _accountService = accountService;
            _paymentValidationService = paymentValidationService;
            _paymentProcessService = paymentProcessService;
        }
        
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = _accountService.GetAccount(request.DebtorAccountNumber);
            
            var result = new MakePaymentResult();
            
            if (account != null && _paymentValidationService.Validate(request, account))
            {
                account.Balance = _paymentProcessService.CalculateBalance(request, account);
                
                _accountService.UpdateAccount(account);
                
                result.Success = true;
            }

            return result;
        }
    }
}
