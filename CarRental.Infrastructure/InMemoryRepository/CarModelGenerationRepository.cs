using CarRental.Domain.Interfaces;
using CarRental.Domain.InternalData.ComponentClasses;
using CarRental.Domain.DataSeed;

namespace CarRental.Infrastructure.InMemoryRepository;

/// <summary>
/// Repository for managing CarModelGeneration entities
/// Provides data access for vehicle model's generation (e.g., year) using the BaseRepository
/// </summary>
public class CarModelGenerationRepository(DataSeed data) : BaseRepository<CarModelGeneration>(data.Generations)
{
    /// <summary>
    /// Gets the unique identifier from the specified CarModelGeneration entity
    /// </summary>
    protected override Guid GetEntityId(CarModelGeneration generation) => generation.Id;

    /// <summary>
    /// Sets the unique identifier for the specified CarModelGeneration entity
    /// </summary>
    protected override void SetEntityId(CarModelGeneration generation, Guid id) => generation.Id = id;
}