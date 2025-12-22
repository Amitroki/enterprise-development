namespace CarRental.Application.Contracts.CarModel;

public record CarModelDto(uint Id, string Name, string? DriveType, uint SeatsNumber, string BodyType, string? ClassType);