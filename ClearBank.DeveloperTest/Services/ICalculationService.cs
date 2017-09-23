namespace ClearBank.DeveloperTest.Services
{
    public interface ICalculationService
    {
        decimal GetDeductedBalance(decimal existingBalance, decimal amount);
    }
}