namespace CarRental.Application.Contracts.Rent;

public record RentCreateUpdateDto(DateTime StartDateTime, double Duration, uint CarId, uint ClientId);