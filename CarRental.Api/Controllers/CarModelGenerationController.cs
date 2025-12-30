using CarRental.Application.Contracts.CarModelGeneration;
using CarRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarModelGenerationsController(IApplicationService<CarModelGenerationDto, CarModelGenerationCreateUpdateDto, Guid> service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<CarModelGenerationDto>>> GetAll() => Ok(await service.ReadAll());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CarModelGenerationDto>> Get(Guid id)
    {
        var result = await service.Read(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CarModelGenerationDto>> Create(CarModelGenerationCreateUpdateDto dto)
    {
        var result = await service.Create(dto);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(Guid id, CarModelGenerationCreateUpdateDto dto)
    {
        var result = await service.Update(dto, id);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await service.Delete(id);
        return result ? NoContent() : NotFound();
    }
}