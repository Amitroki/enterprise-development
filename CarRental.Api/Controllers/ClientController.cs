using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Interfaces;
using CarRental.Application.Contracts.Client;

namespace CarRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController(IApplicationService<ClientDto, ClientCreateUpdateDto> clientService) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<ClientDto>> GetAll() => Ok(clientService.ReadAll());

    [HttpGet("{id}")]
    public ActionResult<ClientDto> Get(uint id)
    {
        var client = clientService.Read(id);
        return client != null ? Ok(client) : NotFound();
    }

    [HttpPost]
    public ActionResult<ClientDto> Create(ClientCreateUpdateDto dto)
    {
        var result = clientService.Create(dto);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public ActionResult Update(uint id, ClientCreateUpdateDto dto)
    {
        return clientService.Update(dto, id) ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(uint id)
    {
        return clientService.Delete(id) ? NoContent() : NotFound();
    }
}