using CarRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
{
    [HttpGet("clients-by-model")]
    public IActionResult GetClientsByModel([FromQuery] string modelName)
    {
        var result = analyticsService.ReadClientsByModelName(modelName);
        return Ok(result);
    }

    [HttpGet("cars-in-rent")]
    public IActionResult GetCarsInRent([FromQuery] DateTime atTime)
    {
        var result = analyticsService.ReadCarsInRent(atTime);
        return Ok(result);
    }

    [HttpGet("top-5-rented-cars")]
    public IActionResult GetTop5Cars()
    {
        var result = analyticsService.ReadTop5MostRentedCars();
        return Ok(result);
    }

    [HttpGet("all-cars-with-rental-count")]
    public IActionResult GetAllCarsWithCount()
    {
        var result = analyticsService.ReadAllCarsWithRentalCount();
        return Ok(result);
    }

    [HttpGet("top-5-clients-by-money")]
    public IActionResult GetTop5Clients()
    {
        var result = analyticsService.ReadTop5ClientsByTotalAmount();
        return Ok(result);
    }
}