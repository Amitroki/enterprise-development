namespace CarRental.Application.Contracts;

/// <summary>
/// Data transfer object for car statistics, including the total number of times it was rented.
/// </summary>
/// <param name="Id">The unique identifier of the car.</param>
/// <param name="ModelName">The descriptive name of the car model.</param>
/// <param name="NumberPlate">The vehicle's license plate number.</param>
/// <param name="RentalCount">Total number of rental agreements associated with this car.</param>
public record CarWithRentalCountDto(
    uint Id,
    string ModelName,
    string NumberPlate,
    int RentalCount
);

/// <summary>
/// Data transfer object for client financial statistics.
/// </summary>
/// <param name="Id">The unique identifier of the client.</param>
/// <param name="FullName">The concatenated full name of the client.</param>
/// <param name="TotalSpentAmount">The sum of all rental costs paid by the client.</param>
/// <param name="TotalRentsCount">Total number of times the client has rented vehicles.</param>
public record ClientWithTotalAmountDto(
    uint Id,
    string FullName,
    decimal TotalSpentAmount,
    int TotalRentsCount
);

/// <summary>
/// Data transfer object representing a car that is currently or was previously in an active rental state.
/// </summary>
/// <param name="CarId">The unique identifier of the car.</param>
/// <param name="ModelName">The descriptive name of the car model.</param>
/// <param name="NumberPlate">The vehicle's license plate number.</param>
/// <param name="RentStartDate">The exact start time of the rental period.</param>
/// <param name="DurationHours">The length of the rental in hours.</param>
public record CarInRentDto(
    uint CarId,
    string ModelName,
    string NumberPlate,
    DateTime RentStartDate,
    int DurationHours
);