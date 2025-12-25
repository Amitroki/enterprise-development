using CarRental.Application.Contracts.CarModel;
using CarRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarModelController(IApplicationService<CarModelDto, CarModelCreateUpdateDto> service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<CarModelDto>>> GetAll() => Ok(await service.ReadAll());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CarModelDto>> Get(int id)
    {
        var result = await service.Read(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CarModelDto>> Create(CarModelCreateUpdateDto dto)
    {
        var result = await service.Create(dto);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CarModelCreateUpdateDto dto)
    {
        var result = await service.Update(dto, id);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await service.Delete(id);
        return result ? NoContent() : NotFound();
    }
}