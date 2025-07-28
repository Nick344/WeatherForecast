using Microsoft.AspNetCore.Mvc;
using WeatherProvider.data;

namespace WeatherProvider.controller;
[ApiController]
[Route("api/[controller]")]
public class WeatherProviderController : ControllerBase
{
    private readonly Random random = new Random();

    [HttpGet]
    public IActionResult Get()
    {
        var result = Enumerable.Range(0, 5).Select(index => new WeatherProviderData
        {
            City = "Kyiv",
            Date = DateTime.Today.AddDays(index).ToString("yyyyMMdd"),
            Celsium = random.Next(-5, 35)
        });

        return Ok(result);
    }
}