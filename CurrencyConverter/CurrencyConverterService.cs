namespace CurrencyConverter;

public sealed class CurrencyConverterService : ICurrencyConverterService
{
    private static CurrencyConverterService _instance;
    private static readonly object LockObject = new object();

    private readonly CurrencyGraph _currencyGraph;

    private CurrencyConverterService()
    {
        _currencyGraph = new CurrencyGraph();
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

    public void ClearConfiguration()
    {
        _currencyGraph.Clear();
    }
    public void UpdateConfiguration(IEnumerable<Tuple<string, string, decimal>> conversionRates)
    {
        ClearConfiguration(); // Clear existing configuration

        foreach (var rate in conversionRates)
        {
            _currencyGraph.AddRate(rate.Item1.ToUpper(), rate.Item2.ToUpper(), rate.Item3);
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