using CarRental.Domain.InternalData.ComponentClasses;
using CarRental.Domain.InternalData.ComponentEnums;

namespace CarRental.Domain.InternalData.ComponentClasses;

/// <summary>
/// Represents a specific generation of a car model, 
/// including its production year, transmission type, 
/// and rental cost per hour
/// </summary>
public class CarModelGeneration
{
    /// <summary>
    /// Unique identifier of the car model generation
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Calendar year when this generation of the car model was produced
    /// </summary>
    public required int Year { get; set; }

    /// <summary>
    /// Type of transmission used in this car model generation (manual, automatic, robotic or variable)
    /// </summary>
    public TransmissionType? TransmissionType { get; set; }

    /// <summary>
    /// The car model to which this generation belongs (a class that describes 
    /// the main technical characteristics, such as the model name, 
    /// drive type, transmission type, body type, and vehicle class)
    /// </summary>
    public CarModel? Model { get; set; }

    /// <summary>
    /// Rental cost per hour for vehicles of this model generation
    /// </summary>
    public required decimal HourCost { get; set; }
}
