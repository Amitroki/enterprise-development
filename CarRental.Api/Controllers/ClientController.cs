using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Interfaces;
using CarRental.Application.Contracts.Client;

namespace CarRental.Api.Controllers;

/// <summary>
/// API controller for managing client records and personal data (CRUD operations)
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientController(IApplicationService<ClientDto, ClientCreateUpdateDto> clientService) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of all registered clients
    /// </summary>
    [HttpGet]
    public ActionResult<List<ClientDto>> GetAll() => Ok(clientService.ReadAll());

    /// <summary>
    /// Retrieves a specific client by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the client</param>
    [HttpGet("{id}")]
    public ActionResult<ClientDto> Get(uint id)
    {
        var client = clientService.Read(id);
        return client != null ? Ok(client) : NotFound();
    }

    /// <summary>
    /// Registers a new client and returns the created record
    /// </summary>
    /// <param name="dto">The client information to create</param>
    [HttpPost]
    public ActionResult<ClientDto> Create(ClientCreateUpdateDto dto)
    {
        var result = clientService.Create(dto);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>
    /// Updates an existing client's information
    /// </summary>
    /// <param name="id">The ID of the client to update</param>
    /// <param name="dto">The updated client data</param>
    [HttpPut("{id}")]
    public ActionResult Update(uint id, ClientCreateUpdateDto dto)
    {
        return clientService.Update(dto, id) ? NoContent() : NotFound();
    }

    /// <summary>
    /// Removes a client from the system by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the client to delete</param>
    [HttpDelete("{id}")]
    public ActionResult Delete(uint id)
    {
        return clientService.Delete(id) ? NoContent() : NotFound();
    }
}