namespace CarRental.Application.Contracts.CarModel;

/// <summary>
/// Data transfer object for creating or updating a car model definition.
/// </summary>
/// <param name="Name">The brand or specific model name.</param>
/// <param name="DriveType">The type of drivetrain (e.g., AWD).</param>
/// <param name="SeatsNumber">The total passenger capacity.</param>
/// <param name="BodyType">The style of the vehicle body (e.g., Sedan, SUV).</param>
/// <param name="ClassType">The market segment or luxury class of the vehicle.</param>
public record CarModelCreateUpdateDto(string Name, string? DriveType, uint SeatsNumber, string BodyType, string? ClassType);