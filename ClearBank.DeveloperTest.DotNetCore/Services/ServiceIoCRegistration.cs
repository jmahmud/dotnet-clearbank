using System;
using System.Collections.Generic;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.DotNetCore.Configuration;
using ClearBank.DeveloperTest.DotNetCore.Services.Validation;
using ClearBank.DeveloperTest.Services.Validation;
using ClearBank.DeveloperTest.Types;
using Microsoft.Extensions.DependencyInjection;

namespace ClearBank.DeveloperTest.Services
{
    public static class ServiceIoCRegistration
    {
        public static void AddServiceRegistrations(this ServiceCollection collection)
        {
            collection.AddSingleton<IAccountService, AccountService>();
            collection.AddSingleton<IPaymentValidationService, PaymentValidationService>();
            
            var validators = new Dictionary<PaymentScheme, IPaymentRequestValidator>()
            {
                { PaymentScheme.Bacs, new BacsPaymentRequestValidator() },
                { PaymentScheme.Chaps, new ChapsPaymentRequestValidator() },
                { PaymentScheme.FasterPayments, new FasterPaymentsPaymentRequestValidator() }
            };

            collection.AddSingleton<IDictionary<PaymentScheme, IPaymentRequestValidator>>(validators);
            

        }
    }
}