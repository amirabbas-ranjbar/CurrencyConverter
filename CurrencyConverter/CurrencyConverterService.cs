namespace CurrencyConverter;

public sealed class CurrencyConverterService : ICurrencyConverterService
{
    private static CurrencyConverterService _instance;
    private static readonly object LockObject = new object();

    private readonly CurrencyGraph _currencyGraph;

    private CurrencyConverterService()
    {
        _currencyGraph = new CurrencyGraph();

        // Add your initial conversion rates
        _currencyGraph.AddRate("USD", "CAD", 1.34m);
        _currencyGraph.AddRate("CAD", "GBP", 0.58m);
        _currencyGraph.AddRate("USD", "EUR", 0.86m);
        // Add more conversion rates as needed
    }

    public static CurrencyConverterService Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (LockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new CurrencyConverterService();
                    }
                }
            }

            return _instance;
        }
    }

    public decimal Convert(string fromCurrency, string toCurrency, decimal amount)
    {
        if (!_currencyGraph.ContainsCurrency(fromCurrency) || !_currencyGraph.ContainsCurrency(toCurrency))
        {
            throw new ArgumentException("Invalid currency specified");
        }

        var shortestPath = new List<string> { fromCurrency, toCurrency };
        decimal convertedAmount = amount;
        for (int i = 0; i < shortestPath.Count - 1; i++)
        {
            string currentCurrency = shortestPath[i];
            string nextCurrency = shortestPath[i + 1];
            decimal conversionRate = _currencyGraph.GetRate(currentCurrency, nextCurrency);
            convertedAmount *= conversionRate;
        }

        return convertedAmount;
    }

    public decimal Rate(string from, string to)
    {
        return _currencyGraph.GetRate(from, to);
    }
}