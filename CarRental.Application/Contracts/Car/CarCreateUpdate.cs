namespace CarRental.Application.Contracts;

public record CarCreateUpdateDto(string NumberPlate, string Colour, uint ModelGenerationId);