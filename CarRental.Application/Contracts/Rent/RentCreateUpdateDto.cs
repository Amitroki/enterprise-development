namespace CarRental.Application.Contracts.Rent;

/// <summary>
/// Data transfer object for creating or updating a car rental agreement.
/// </summary>
/// <param name="StartDateTime">The scheduled date and time for the rental to begin.</param>
/// <param name="Duration">The length of the rental period in hours.</param>
/// <param name="CarId">The unique identifier of the car to be rented.</param>
/// <param name="ClientId">The unique identifier of the client renting the car.</param>
public record RentCreateUpdateDto(DateTime StartDateTime, double Duration, uint CarId, uint ClientId);