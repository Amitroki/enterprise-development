namespace CarRental.Application.Contracts.Car;

public record CarCreateUpdateDto(string NumberPlate, string Colour, uint ModelGenerationId);