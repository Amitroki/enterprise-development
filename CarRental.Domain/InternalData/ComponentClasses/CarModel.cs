using CarRental.Domain.InternalData.ComponentEnums;
using DriveType = CarRental.Domain.InternalData.ComponentEnums.DriveType;

namespace CarRental.Domain.InternalData.ComponentClasses;

/// <summary>
/// Represents a specific car model with its key characteristics 
/// such as name, body type, drive type, seating capacity, and vehicle class
/// </summary>
public class CarModel
{
    /// <summary>
    /// Unique identifier of the car model
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Name of the car model (e.g., "Camry", "Golf", "Model 3")
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Type of drive system used by the car model (front-wheel, rear-wheel or all-wheel drive)
    /// </summary>
    public DriveType? DriveType { get; set; }

    /// <summary>
    /// Number of passenger seats in the vehicle
    /// </summary>
    public required uint SeatsNumber { get; set; }

    /// <summary>
    /// Body style of the car model (e.g., sedan, SUV, hatchback)
    /// </summary>
    public required BodyType BodyType { get; set; }

    /// <summary>
    /// Vehicle classification by size and market segment (A, B, C, D, E or F)
    /// </summary>
    public ClassType? ClassType { get; set; }
}

