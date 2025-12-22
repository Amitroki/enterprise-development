namespace CarRental.Application.Contracts.CarModelGeneration;

public record CarModelGenerationDto(uint Id, int Year, string? TransmissionType, decimal HourCost, uint ModelId);