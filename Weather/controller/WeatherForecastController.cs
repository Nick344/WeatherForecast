using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Weather.data;

namespace Weather.controller;

[ApiController]
[Route("weatherforecast")]
public class WeatherForecastController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public WeatherForecastController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetWeather()
    {
        var client = _httpClientFactory.CreateClient("WeatherProvider");
        var response = await client.GetAsync("api/WeatherProvider");

        if (!response.IsSuccessStatusCode)
        {
            return NotFound("Error from WeatherProvider");
        }

        var jsonString = await response.Content.ReadAsStringAsync();
        var result =  JsonSerializer.Deserialize<List<WeatherProviderInfo>>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }).Select(data => new WeatherData
        {
            City = data.City,
            Celsium = data.Celsium,
            Date = data.Date,
            Fahrenheit = data.Celsium * 9/5 + 32
        }).ToList();

        return Ok(result);
    }
}