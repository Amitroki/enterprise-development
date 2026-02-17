namespace CarRental.Application.Contracts.Car;

/// <summary>
/// Data transfer object representing a car with its basic details for display.
/// </summary>
/// <param name="Id">The unique identifier of the car.</param>
/// <param name="NumberPlate">The vehicle's license plate number.</param>
/// <param name="Colour">The color of the car.</param>
/// <param name="ModelGenerationId">ID of the model generation.</param>
public record CarDto(Guid Id, string NumberPlate, string Colour, Guid ModelGenerationId);