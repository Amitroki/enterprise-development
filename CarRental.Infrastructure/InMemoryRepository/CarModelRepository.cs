using CarRental.Domain.Interfaces;
using CarRental.Domain.InternalData.ComponentClasses;
using CarRental.Domain.DataSeed;

namespace CarRental.Infrastructure.InMemoryRepository;

/// <summary>
/// Repository for managing CarModel entities
/// Provides data access for vehicle models (e.g., model name) using the BaseRepository
/// </summary>
public class CarModelRepository(DataSeed data) : BaseRepository<CarModel>(data.Models)
{
    /// <summary>
    /// Gets the unique identifier from the specified CarModel entity
    /// </summary>
    protected override uint GetEntityId(CarModel model) => model.Id;

    /// <summary>
    /// Sets the unique identifier for the specified CarModel entity
    /// </summary>
    protected override void SetEntityId(CarModel model, uint id) => model.Id = id;
}