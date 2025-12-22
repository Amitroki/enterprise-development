using CarRental.Application.Contracts.Car;
using CarRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

/// <summary>
/// API controller for managing the car fleet (CRUD operations)
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CarsController(IApplicationService<CarDto, CarCreateUpdateDto> carService) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of all cars available in the system
    /// </summary>
    [HttpGet]
    public ActionResult<List<CarDto>> GetAll()
    {
        var cars = carService.ReadAll();
        return Ok(cars);
    }

    /// <summary>
    /// Retrieves details of a specific car by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the car</param>
    [HttpGet("{id}")]
    public ActionResult<CarDto> GetById(uint id)
    {
        var car = carService.Read(id);
        if (car == null)
        {
            return NotFound($"Машина с ID {id} не найдена.");
        }
        return Ok(car);
    }

    /// <summary>
    /// Registers a new car in the fleet
    /// </summary>
    /// <param name="dto">The data for the new car record</param>
    [HttpPost]
    public ActionResult<CarDto> Create([FromBody] CarCreateUpdateDto dto)
    {
        try
        {
            var createdCar = carService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdCar.Id }, createdCar);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Updates an existing car's information
    /// </summary>
    /// <param name="id">The unique identifier of the car to update</param>
    /// <param name="dto">The updated data</param>
    [HttpPut("{id}")]
    public ActionResult<CarDto> Update(uint id, [FromBody] CarCreateUpdateDto dto)
    {
        try
        {
            var updatedCar = carService.Update(dto, id);
            return Ok(updatedCar);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Removes a car from the system
    /// </summary>
    /// <param name="id">The unique identifier of the car to delete</param>
    [HttpDelete("{id}")]
    public ActionResult Delete(uint id)
    {
        var result = carService.Delete(id);
        if (!result)
        {
            return NotFound($"Не удалось удалить машину с ID {id}.");
        }
        return NoContent();
    }
}