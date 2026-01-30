using CarRental.Application.Contracts.CarModel;
using CarRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

/// <summary>
/// API Controller for managing car models
/// </summary>
/// <param name="service">The application service for car model operations</param>
/// <param name="logger">The logger instance for diagnostics and activity tracking</param>
[ApiController]
[Route("api/[controller]")]
public class CarModelController(IApplicationService<CarModelDto, CarModelCreateUpdateDto, Guid> service, ILogger<CarController> logger) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of all car models
    /// </summary>
    /// <returns>A collection of car model DTOs</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<CarModelDto>>> GetAll()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetAll), GetType().Name);
        try
        {
            var carModel = await service.ReadAll();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAll), GetType().Name);
            return Ok(carModel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetAll), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific car model by its unique identifier
    /// </summary>
    /// <param name="id">The GUID of the car model</param>
    /// <returns>The requested car model DTO</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<CarModelDto>> Get(Guid id)
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(Get), GetType().Name);
        try
        {
            var result = await service.Read(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Get), GetType().Name);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Get), GetType().Name);
            return StatusCode(404, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Get), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Creates a new car model entry
    /// </summary>
    /// <param name="dto">The data for the new car model</param>
    /// <returns>The created car model DTO</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<CarModelDto>> Create(CarModelCreateUpdateDto dto)
    {
        logger.LogInformation("{method} method of {controller} is called with {@dto} parameter", nameof(Create), GetType().Name, dto);
        try
        {
            var result = await service.Create(dto);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Create), GetType().Name);
            return CreatedAtAction(nameof(this.Create), result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Create), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Updates an existing car model
    /// </summary>
    /// <param name="id">The GUID of the car model to update</param>
    /// <param name="dto">The updated information</param>
    /// <returns>The result of the update operation</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update(Guid id, CarModelCreateUpdateDto dto)
    {
        logger.LogInformation("{method} method of {controller} is called with {key},{@dto} parameters", nameof(Update), GetType().Name, id, dto);
        try
        {
            var result = await service.Update(dto, id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Update), GetType().Name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Update), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Deletes a car model from the system
    /// </summary>
    /// <param name="id">The GUID of the car model to remove</param>
    /// <returns>An OK result if deleted, or NoContent if not found</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(Guid id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Delete), GetType().Name, id);
        try
        {
            var result = await service.Delete(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Delete), GetType().Name);
            return result ? Ok() : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Delete), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}