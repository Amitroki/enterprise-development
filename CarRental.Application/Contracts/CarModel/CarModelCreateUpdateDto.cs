namespace CarRental.Application.Contracts.CarModel;

public record CarModelCreateUpdateDto(string Name, string? DriveType, uint SeatsNumber, string BodyType, string? ClassType);