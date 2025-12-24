namespace CarRental.Application.Contracts.CarModel;

/// <summary>
/// Data transfer object representing a car model with its technical specifications.
/// </summary>
/// <param name="Id">The unique identifier of the car model.</param>
/// <param name="Name">The brand or specific model name.</param>
/// <param name="DriveType">The type of drivetrain (e.g., AWD, FWD, RWD).</param>
/// <param name="SeatsNumber">The total passenger capacity.</param>
/// <param name="BodyType">The style of the vehicle body (e.g., Sedan, SUV).</param>
/// <param name="ClassType">The market segment or luxury class of the vehicle.</param>
public record CarModelDto(int Id, string Name, string? DriveType, int SeatsNumber, string BodyType, string? ClassType);