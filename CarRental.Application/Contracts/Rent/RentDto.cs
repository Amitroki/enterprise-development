namespace CarRental.Application.Contracts.Rent;

/// <summary>
/// Data transfer object representing a rental agreement with calculated details and linked entity info.
/// </summary>
/// <param name="Id">The unique identifier of the rental record.</param>
/// <param name="StartDateTime">The date and time when the rental period starts.</param>
/// <param name="Duration">The length of the rental in hours.</param>
/// <param name="CarId">The unique identifier of the rented car.</param>
/// <param name="CarLicensePlate">The license plate of the rented car.</param>
/// <param name="ClientId">The unique identifier of the client.</param>
/// <param name="ClientLastName">The last name of the client.</param>
/// <param name="TotalCost">The total calculated cost for the rental duration.</param>
public record RentDto(uint Id, DateTime StartDateTime, double Duration, uint CarId, string CarLicensePlate, uint ClientId, string ClientLastName, decimal TotalCost);