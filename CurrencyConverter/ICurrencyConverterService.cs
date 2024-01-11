namespace CurrencyConverter;

public interface ICurrencyConverterService
{
    decimal Convert(string fromCurrency, string toCurrency, decimal amount);
}