namespace CurrencyConverter;

public class CurrencyGraph
{
    private readonly Dictionary<string, Dictionary<string, decimal>> _graph;

    public CurrencyGraph()
    {
        _graph = new Dictionary<string, Dictionary<string, decimal>>();
    }

    public void AddRate(string fromCurrency, string toCurrency, decimal rate)
    {
        if (!_graph.ContainsKey(fromCurrency))
        {
            _graph[fromCurrency] = new Dictionary<string, decimal>();
        }

        _graph[fromCurrency][toCurrency] = rate;

        // Add the reciprocal rate
        if (!_graph.ContainsKey(toCurrency))
        {
            _graph[toCurrency] = new Dictionary<string, decimal>();
        }

        _graph[toCurrency][fromCurrency] = 1 / rate;
    }

    public decimal GetRate(string fromCurrency, string toCurrency)
    {
        return _graph[fromCurrency][toCurrency];
    }

    public bool ContainsCurrency(string currency)
    {
        return _graph.ContainsKey(currency);
    }
}