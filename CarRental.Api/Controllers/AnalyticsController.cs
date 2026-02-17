using CarRental.Application.Contracts.Client;
using CarRental.Application.Contracts.Analytics;
using CarRental.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

/// <summary>
/// Provides specialized API endpoints for data analytics and business reporting
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of clients who have rented cars associated with a specific model name
    /// </summary>
    /// <param name="modelName">The name of the car model to filter by</param>
    [HttpGet("clients-by-model")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<ClientDto>>> GetClientsByModel([FromQuery] string modelName)
    {
        logger.LogInformation("{method} method of {controller} is called with {string} parameter", nameof(GetClientsByModel), GetType().Name, modelName);
        try
        {
            var result = await analyticsService.ReadClientsByModelName(modelName);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetClientsByModel), GetType().Name);
            return Ok(result);
        }
        catch (Exception ex) 
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetClientsByModel), GetType().Name);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Returns details of cars that are currently on lease at the specified date and time
    /// </summary>
    /// <param name="atTime">The point in time to check for active rentals</param>
    [HttpGet("cars-in-rent")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<CarInRentDto>>> GetCarsInRent([FromQuery] DateTime atTime)
    {
        logger.LogInformation("{method} method of {controller} is called with {parameterName} = {parameterValue}", nameof(GetCarsInRent), GetType().Name, nameof(atTime), atTime);
        try
        {
            var result = await analyticsService.ReadCarsInRent(atTime);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetCarsInRent), GetType().Name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetCarsInRent), GetType().Name);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Returns the top 5 most popular cars based on total rental frequency
    /// </summary>
    [HttpGet("top-5-rented-cars")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<CarWithRentalCountDto>>> GetTop5Cars()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetTop5Cars), GetType().Name);
        try
        {
            var result = await analyticsService.ReadTop5MostRentedCars();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetTop5Cars), GetType().Name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetTop5Cars), GetType().Name);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Provides a comprehensive list of all cars and how many times each has been rented
    /// </summary>
    [HttpGet("all-cars-with-rental-count")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<CarWithRentalCountDto>>> GetAllCarsWithCount()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetAllCarsWithCount), GetType().Name);
        try
        {
            var result = await analyticsService.ReadAllCarsWithRentalCount();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAllCarsWithCount), GetType().Name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetAllCarsWithCount), GetType().Name);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Returns the top 5 clients who have contributed the most to total revenue
    /// </summary>
    [HttpGet("top-5-clients-by-money")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<ClientWithTotalAmountDto>>> GetTop5Clients()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetTop5Clients), GetType().Name);
        try
        {
            var result = await analyticsService.ReadTop5ClientsByTotalAmount();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetTop5Clients), GetType().Name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetTop5Clients), GetType().Name);
            return StatusCode(500);
        }
    }
}