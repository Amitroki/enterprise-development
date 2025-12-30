using CarRental.Domain.InternalData.ComponentClasses;

namespace CarRental.Domain.DataModels;

/// <summary>
/// Represents a specific physical vehicle available for rental
/// </summary>
public class Car 
{
    /// <summary>
    /// Unique identifier of the car
    /// </summary>
    public required Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// The model generation id this car belongs to, defining its year, transmission type, and base rental cost
    /// </summary>
    public required Guid ModelGenerationId { get; set; }

    /// <summary>
    /// The model generation this car belongs to, defining its year, transmission type, and base rental cost
    /// </summary>
    public required CarModelGeneration ModelGeneration { get; set; }

    /// <summary>
    /// License plate number of the car
    /// </summary>
    public required string NumberPlate { get; set; }

    /// <summary>
    /// Exterior colour of the car
    /// </summary>
    public required string Colour { get; set; }
}