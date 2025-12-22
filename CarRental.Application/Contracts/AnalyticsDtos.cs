namespace CarRental.Application.Contracts;

public record CarWithRentalCountDto(
    uint Id,
    string ModelName,
    string NumberPlate,
    int RentalCount
);

public record ClientWithTotalAmountDto(
    uint Id,
    string FullName,
    decimal TotalSpentAmount,
    int TotalRentsCount
);

public record CarInRentDto(
    uint CarId,
    string ModelName,
    string NumberPlate,
    DateTime RentStartDate,
    int DurationHours
);