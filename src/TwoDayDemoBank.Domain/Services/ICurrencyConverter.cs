namespace TwoDayDemoBank.Domain.Services
{
    public interface ICurrencyConverter
    {
        Money Convert(Money amount, Currency currency);
    }
}