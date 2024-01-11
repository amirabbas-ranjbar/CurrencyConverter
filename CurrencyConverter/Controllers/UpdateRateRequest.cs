namespace CurrencyConverter.Controllers;

public class UpdateRateRequest
{
    public string From { get; set; }
    public string To { get; set; }
    public decimal Amount { get; set; }
}