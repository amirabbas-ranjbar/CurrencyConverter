namespace CurrencyConverter;

public interface ICurrencyConverterService
{
    void ClearConfiguration();
    void UpdateConfiguration(IEnumerable<Tuple<string,string,decimal>> conversionRate);
    decimal Convert(string fromCurrency, string toCurrency, decimal amount);
}