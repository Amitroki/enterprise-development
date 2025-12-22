using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Interfaces;
using CarRental.Application.Contracts.Rent;

namespace CarRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentController(IApplicationService<RentDto, RentCreateUpdateDto> rentService) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<RentDto>> GetAll() => Ok(rentService.ReadAll());

    [HttpGet("{id}")]
    public ActionResult<RentDto> Get(uint id)
    {
        var rent = rentService.Read(id);
        return rent != null ? Ok(rent) : NotFound();
    }

    [HttpPost]
    public ActionResult<RentDto> Create(RentCreateUpdateDto dto)
    {
        var result = rentService.Create(dto);
        if (result == null)
        {
            return BadRequest("Client or car is not exist");
        }
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public ActionResult Update(uint id, RentCreateUpdateDto dto)
    {
        return rentService.Update(dto, id) ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(uint id)
    {
        return rentService.Delete(id) ? NoContent() : NotFound();
    }
}