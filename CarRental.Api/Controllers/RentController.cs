using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Interfaces;
using CarRental.Application.Contracts.Rent;

namespace CarRental.Api.Controllers;

/// <summary>
/// API controller for managing car rental agreements and lease transactions
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentController(IApplicationService<RentDto, RentCreateUpdateDto> rentService) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of all rental records, including calculated costs and linked entity names
    /// </summary>
    [HttpGet]
    public ActionResult<List<RentDto>> GetAll() => Ok(rentService.ReadAll());

    /// <summary>
    /// Retrieves a specific rental agreement by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the rental record.</param>
    [HttpGet("{id}")]
    public ActionResult<RentDto> Get(uint id)
    {
        var rent = rentService.Read(id);
        return rent != null ? Ok(rent) : NotFound();
    }

    /// <summary>
    /// Creates a new rental agreement after verifying the existence of the client and car.
    /// </summary>
    /// <param name="dto">The rental details, including CarId and ClientId.</param>
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

    /// <summary>
    /// Updates the details of an existing rental agreement.
    /// </summary>
    /// <param name="id">The ID of the rental to update.</param>
    /// <param name="dto">The updated rental data.</param>
    [HttpPut("{id}")]
    public ActionResult Update(uint id, RentCreateUpdateDto dto)
    {
        return rentService.Update(dto, id) ? NoContent() : NotFound();
    }

    /// <summary>
    /// Deletes a rental record from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the rental to remove.</param>
    [HttpDelete("{id}")]
    public ActionResult Delete(uint id)
    {
        return rentService.Delete(id) ? NoContent() : NotFound();
    }
}