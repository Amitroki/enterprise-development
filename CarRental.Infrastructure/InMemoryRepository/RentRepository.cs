using CarRental.Domain.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.DataSeed;

namespace CarRental.Infrastructure.InMemoryRepository;

/// <summary>
/// Repository for the Rent entity
/// Inherits BaseRepository for in-memory CRUD operations
/// </summary>
public class RentRepository(DataSeed data) : BaseRepository<Rent>(data.Rents)
{
    /// <summary>
    /// Gets the unique identifier from the specified Rent entity
    /// </summary>
    protected override int GetEntityId(Rent rent) => rent.Id;

    /// <summary>
    /// Sets the unique identifier for the specified Rent entity
    /// </summary>
    protected override void SetEntityId(Rent rent, int id) => rent.Id = id;
}