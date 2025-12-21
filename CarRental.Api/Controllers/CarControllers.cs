using CarRental.Application.Contracts.Car;
using CarRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly IApplicationService<CarDto, CarCreateUpdateDto> _carService;

    public CarsController(IApplicationService<CarDto, CarCreateUpdateDto> carService)
    {
        _carService = carService;
    }

    [HttpGet]
    public ActionResult<List<CarDto>> GetAll()
    {
        var cars = _carService.ReadAll();
        return Ok(cars);
    }

    [HttpGet("{id}")]
    public ActionResult<CarDto> GetById(uint id)
    {
        var car = _carService.Read(id);
        if (car == null)
        {
            return NotFound($"Машина с ID {id} не найдена.");
        }
        return Ok(car);
    }

    [HttpPost]
    public ActionResult<CarDto> Create([FromBody] CarCreateUpdateDto dto)
    {
        try
        {
            var createdCar = _carService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdCar.Id }, createdCar);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<CarDto> Update(uint id, [FromBody] CarCreateUpdateDto dto)
    {
        try
        {
            var updatedCar = _carService.Update(dto, id);
            return Ok(updatedCar);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(uint id)
    {
        var result = _carService.Delete(id);
        if (!result)
        {
            return NotFound($"Не удалось удалить машину с ID {id}.");
        }
        return NoContent();
    }
}