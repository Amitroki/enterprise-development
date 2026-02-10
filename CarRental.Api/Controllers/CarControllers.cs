using CarRental.Application.Contracts.Car;
using CarRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

/// <summary>
/// API controller for managing the car fleet (CRUD operations)
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CarController(IApplicationService<CarDto, CarCreateUpdateDto, Guid> carService, ILogger<CarController> logger) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of all cars available in the system
    /// </summary>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<CarDto>>> GetAll()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetAll), GetType().Name);
        try
        {
            var cars = await carService.ReadAll();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAll), GetType().Name);
            return Ok(cars);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetAll), GetType().Name);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Retrieves details of a specific car by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the car</param>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<CarDto>> Get(Guid id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Get), GetType().Name, id);
        try
        {
            var car = await carService.Read(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Get), GetType().Name);
            return Ok(car);
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
    /// Registers a new car in the fleet
    /// </summary>
    /// <param name="dto">The data for the new car record</param>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<CarDto>> Create([FromBody] CarCreateUpdateDto dto)
    {
        logger.LogInformation("{method} method of {controller} is called with {dto} parameter", nameof(Create), GetType().Name, dto);
        try
        {
            var createdCar = await carService.Create(dto);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Create), GetType().Name);
            return CreatedAtAction(nameof(this.Create), createdCar);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Create), GetType().Name);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Updates an existing car's information
    /// </summary>
    /// <param name="id">The unique identifier of the car to update</param>
    /// <param name="dto">The updated data</param>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<CarDto>> Update(Guid id, [FromBody] CarCreateUpdateDto dto)
    {
        logger.LogInformation("{method} method of {controller} is called with {key},{dto} parameters", nameof(Update), GetType().Name, id, dto);
        try
        {
            var updatedCar = await carService.Update(dto, id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Update), GetType().Name);
            return Ok(updatedCar);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Update), GetType().Name);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Removes a car from the system
    /// </summary>
    /// <param name="id">The unique identifier of the car to delete</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Delete(Guid id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Delete), GetType().Name, id);
        try
        {
            var result = await carService.Delete(id);
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