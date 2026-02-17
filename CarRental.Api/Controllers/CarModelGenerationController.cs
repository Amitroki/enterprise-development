using CarRental.Application.Contracts.CarModelGeneration;
using CarRental.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

/// <summary>
/// API Controller for managing car model generations
/// </summary>
/// <param name="service">The application service for car model generation logic</param>
/// <param name="logger">The logger instance for diagnostics</param>
[ApiController]
[Route("api/[controller]")]
public class CarModelGenerationsController(IApplicationService<CarModelGenerationDto, CarModelGenerationCreateUpdateDto, Guid> service, ILogger<CarController> logger) : ControllerBase
{
    /// <summary>
    /// Retrieves all car model generations
    /// </summary>
    /// <returns>A list of car model generation DTOs</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<CarModelGenerationDto>>> GetAll()
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
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Retrieves a specific car model generation by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the car model generation</param>
    /// <returns>The requested car model generation DTO</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<CarModelGenerationDto>> Get(Guid id)
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
            logger.LogWarning(ex, "An exception happened during {method} method of {controller}", nameof(Get), GetType().Name);
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Get), GetType().Name);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Creates a new car model generation
    /// </summary>
    /// <param name="dto">The data transfer object containing car model generation details</param>
    /// <returns>The created car model generation DTO</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<CarModelGenerationDto>> Create(CarModelGenerationCreateUpdateDto dto)
    {
        logger.LogInformation("{method} method of {controller} is called with {dto} parameter", nameof(Create), GetType().Name, dto);
        try
        {
            var result = await service.Create(dto);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Create), GetType().Name);
            return CreatedAtAction(nameof(this.Create), result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Create), GetType().Name);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Updates an existing car model generation
    /// </summary>
    /// <param name="id">The unique identifier of the generation to update</param>
    /// <param name="dto">The updated data for the car model generation</param>
    /// <returns>An IActionResult indicating the result of the operation</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update(Guid id, CarModelGenerationCreateUpdateDto dto)
    {
        logger.LogInformation("{method} method of {controller} is called with {key},{dto} parameters", nameof(Update), GetType().Name, id, dto);
        try
        {
            var result = await service.Update(dto, id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Update), GetType().Name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Update), GetType().Name);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Deletes a car model generation by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the generation to delete</param>
    /// <returns>An IActionResult indicating the result of the deletion</returns>
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
            return StatusCode(500);
        }
    }
}