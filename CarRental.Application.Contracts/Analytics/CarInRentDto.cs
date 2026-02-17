namespace CarRental.Application.Contracts.Analytics;

/// <summary>
/// Data transfer object representing a car that is currently or was previously in an active rental state.
/// </summary>
/// <param name="CarId">The unique identifier of the car.</param>
/// <param name="ModelName">The descriptive name of the car model.</param>
/// <param name="NumberPlate">The vehicle's license plate number.</param>
/// <param name="RentStartDate">The exact start time of the rental period.</param>
/// <param name="DurationHours">The length of the rental in hours.</param>
public record CarInRentDto(Guid CarId, string ModelName, string NumberPlate, DateTime RentStartDate, int DurationHours);