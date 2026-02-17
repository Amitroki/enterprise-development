using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Contracts.Interfaces;
using CarRental.Application.Contracts.Client;

namespace CarRental.Api.Controllers;

/// <summary>
/// API controller for managing client records and personal data (CRUD operations)
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientController(IApplicationService<ClientDto, ClientCreateUpdateDto, Guid> clientService, ILogger<ClientController> logger) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of all registered clients
    /// </summary>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<ClientDto>>> GetAll()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetAll), GetType().Name);
        try
        {
            var result = await clientService.ReadAll();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAll), GetType().Name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetAll), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific client by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the client</param>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ClientDto>> Get(Guid id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Get), GetType().Name, id);
        try
        {
            var client = await clientService.Read(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Get), GetType().Name);
            return Ok(client);
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
    /// Registers a new client and returns the created record
    /// </summary>
    /// <param name="dto">The client information to create</param>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ClientDto>> Create(ClientCreateUpdateDto dto)
    {
        logger.LogInformation("{method} method of {controller} is called with {dto} parameter", nameof(Create), GetType().Name, dto);
        try
        {
            var createdClient = await clientService.Create(dto);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Create), GetType().Name);
            return CreatedAtAction(nameof(this.Create), createdClient);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Create), GetType().Name);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Updates an existing client's information
    /// </summary>
    /// <param name="id">The ID of the client to update</param>
    /// <param name="dto">The updated client data</param>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Update(Guid id, ClientCreateUpdateDto dto)
    {
        logger.LogInformation("{method} method of {controller} is called with {key},{dto} parameters", nameof(Update), GetType().Name, id, dto);
        try
        {
            var updatedClient = await clientService.Update(dto, id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Update), GetType().Name);
            return Ok(updatedClient);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Update), GetType().Name);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Removes a client from the system by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the client to delete</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Delete(Guid id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Delete), GetType().Name, id);
        try
        {
            var result = await clientService.Delete(id);
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