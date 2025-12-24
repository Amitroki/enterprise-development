namespace CarRental.Application.Contracts.Analytics;

/// <summary>
/// Data transfer object for car statistics, including the total number of times it was rented.
/// </summary>
/// <param name="Id">The unique identifier of the car.</param>
/// <param name="ModelName">The descriptive name of the car model.</param>
/// <param name="NumberPlate">The vehicle's license plate number.</param>
/// <param name="RentalCount">Total number of rental agreements associated with this car.</param>
public record CarWithRentalCountDto(uint Id, string ModelName, string NumberPlate, int RentalCount);