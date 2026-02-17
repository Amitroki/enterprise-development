using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Contracts.Interfaces;
using CarRental.Application.Contracts.Rent;

namespace CarRental.Api.Controllers;

/// <summary>
/// API controller for managing car rental agreements and lease transactions
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentController(IApplicationService<RentDto, RentCreateUpdateDto, Guid> rentService, ILogger<RentController> logger) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of all rental records, including calculated costs and linked entity names
    /// </summary>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<RentDto>>> GetAll()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetAll), GetType().Name);
        try
        {
            var result = await rentService.ReadAll();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAll), GetType().Name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetAll), GetType().Name);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Retrieves a specific rental agreement by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the rental record.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<RentDto>> Get(Guid id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Get), GetType().Name, id);
        try
        {
            var rent = await rentService.Read(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Get), GetType().Name);
            return Ok(rent);
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
    /// Creates a new rental agreement after verifying the existence of the client and car.
    /// </summary>
    /// <param name="dto">The rental details, including CarId and ClientId.</param>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<RentDto>> Create(RentCreateUpdateDto dto)
    {
        logger.LogInformation("{method} method of {controller} is called with {dto} parameter", nameof(Create), GetType().Name, dto);
        try
        {
            var createdRent = await rentService.Create(dto);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Create), GetType().Name);
            return CreatedAtAction(nameof(this.Create), createdRent);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Create), GetType().Name);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Updates the details of an existing rental agreement.
    /// </summary>
    /// <param name="id">The ID of the rental to update.</param>
    /// <param name="dto">The updated rental data.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Update(Guid id, RentCreateUpdateDto dto)
    {
        logger.LogInformation("{method} method of {controller} is called with {key},{dto} parameters", nameof(Update), GetType().Name, id, dto);
        try
        {
            var updatedRent = await rentService.Update(dto, id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Update), GetType().Name);
            return Ok(updatedRent);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Update), GetType().Name);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Deletes a rental record from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the rental to remove.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Delete(Guid id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Delete), GetType().Name, id);
        try
        {
            var result = await rentService.Delete(id);
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