using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CurrencyController : ControllerBase
{
    [HttpGet]
    public IActionResult Convert(string from, string to, decimal amount)
    {
        var service = CurrencyConverterService.Instance;
        var result = service.Convert(from.ToUpper(), to.ToUpper(), amount);
        return Ok(result);
    }

    [HttpGet]
    public IActionResult Rate(string from, string to)
    {
        var service = CurrencyConverterService.Instance;
        var result = service.Rate(from.ToUpper(), to.ToUpper());
        return Ok(result);
    }

    [HttpPost]
    public IActionResult UpdateRates(List<UpdateRateRequest> requests)
    {
        var service = CurrencyConverterService.Instance;
        service.UpdateConfiguration(requests.Select(a => new Tuple<string, string, decimal>(a.From, a.To, a.Amount)));
        return Ok();
    }
}