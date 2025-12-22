using CarRental.Domain.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.DataSeed;

namespace CarRental.Infrastructure.InMemoryRepository;

/// <summary>
/// Repository for the Client entity
/// Inherits BaseRepository for in-memory CRUD operations
/// </summary>
public class ClientRepository(DataSeed data) : BaseRepository<Client>(data.Clients)
{
    /// <summary>
    /// Gets the unique identifier from the specified Client entity
    /// </summary>
    protected override uint GetEntityId(Client client) => client.Id;

    /// <summary>
    /// Sets the unique identifier for the specified Client entity
    /// </summary>
    protected override void SetEntityId(Client client, uint id) => client.Id = id;
}