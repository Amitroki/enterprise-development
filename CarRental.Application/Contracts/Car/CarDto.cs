namespace CarRental.Application.Contracts.Car;

/// <summary>
/// Data transfer object representing a car with its basic details for display.
/// </summary>
/// <param name="Id">The unique identifier of the car.</param>
/// <param name="NumberPlate">The vehicle's license plate number.</param>
/// <param name="Colour">The color of the car.</param>
/// <param name="ModelName">The descriptive name of the car model.</param>
public record CarDto(int Id, string NumberPlate, string Colour, string ModelName);