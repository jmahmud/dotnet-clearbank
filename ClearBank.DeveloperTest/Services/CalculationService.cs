namespace ClearBank.DeveloperTest.Services
{
    public class CalculationService : ICalculationService
    {
        public decimal GetDeductedBalance(decimal existingBalance, decimal amount)
        {
            return existingBalance - amount;
        }
    }
}