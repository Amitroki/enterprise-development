namespace CarRental.Application.Contracts.Rent;

public record RentDto(uint Id, DateTime StartDateTime, double Duration, uint CarId, string CarLicensePlate, uint ClientId, string ClientLastName, decimal TotalCost);