using CarRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

/// <summary>
/// Provides specialized API endpoints for data analytics and business reporting
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of clients who have rented cars associated with a specific model name
    /// </summary>
    /// <param name="modelName">The name of the car model to filter by</param>
    [HttpGet("clients-by-model")]
    public IActionResult GetClientsByModel([FromQuery] string modelName)
    {
        var result = analyticsService.ReadClientsByModelName(modelName);
        return Ok(result);
    }

    /// <summary>
    /// Returns details of cars that are currently on lease at the specified date and time
    /// </summary>
    /// <param name="atTime">The point in time to check for active rentals</param>
    [HttpGet("cars-in-rent")]
    public IActionResult GetCarsInRent([FromQuery] DateTime atTime)
    {
        var result = analyticsService.ReadCarsInRent(atTime);
        return Ok(result);
    }

    /// <summary>
    /// Returns the top 5 most popular cars based on total rental frequency
    /// </summary>
    [HttpGet("top-5-rented-cars")]
    public IActionResult GetTop5Cars()
    {
        var result = analyticsService.ReadTop5MostRentedCars();
        return Ok(result);
    }

    /// <summary>
    /// Provides a comprehensive list of all cars and how many times each has been rented
    /// </summary>
    [HttpGet("all-cars-with-rental-count")]
    public IActionResult GetAllCarsWithCount()
    {
        var result = analyticsService.ReadAllCarsWithRentalCount();
        return Ok(result);
    }

    /// <summary>
    /// Returns the top 5 clients who have contributed the most to total revenue
    /// </summary>
    [HttpGet("top-5-clients-by-money")]
    public IActionResult GetTop5Clients()
    {
        var result = analyticsService.ReadTop5ClientsByTotalAmount();
        return Ok(result);
    }
}