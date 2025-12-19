using CarRental.Domain.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.DataSeed;

namespace CarRental.Infrastructure.InMemoryRepository;

public class ClientRepository(DataSeed data) : BaseRepository<Client>(data.Clients)
{
    protected override uint GetEntityId(Client client) => client.Id;

    protected override void SetEntityId(Client client, uint id) => client.Id = id;
}