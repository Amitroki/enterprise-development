namespace CarRental.Application.Contracts.CarModelGeneration;

public record CarModelGenerationCreateUpdateDto(int Year, string? TransmissionType, decimal HourCost, uint ModelId);