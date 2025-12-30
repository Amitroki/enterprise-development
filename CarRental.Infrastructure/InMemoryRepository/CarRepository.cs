using CarRental.Domain.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.DataSeed;

namespace CarRental.Infrastructure.InMemoryRepository;

/// <summary>
/// Repository for the Car entity
/// Inherits BaseRepository for in-memory CRUD operations
/// </summary>
public class CarRepository(DataSeed data) : BaseRepository<Car>(data.Cars)
{
    /// <summary>
    /// Gets the unique identifier from the specified Car entity
    /// </summary>
    protected override Guid GetEntityId(Car car) => car.Id;

    /// <summary>
    /// Sets the unique identifier for the specified Car entity
    /// </summary>
    protected override void SetEntityId(Car car, Guid id) => car.Id = id;
}