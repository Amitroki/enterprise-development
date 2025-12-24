namespace CarRental.Application.Contracts.Car;

/// <summary>
/// Data transfer object for creating or updating a car record.
/// </summary>
/// <param name="NumberPlate">The vehicle's license plate number.</param>
/// <param name="Colour">The color of the car.</param>
/// <param name="ModelGenerationId">The unique identifier of the associated car model generation.</param>
public record CarCreateUpdateDto(string NumberPlate, string Colour, int ModelGenerationId);