using CarRental.Domain;
using CarRental.Domain.DataModels;
using CarRental.Domain.DataSeed;

namespace CarRental.Infrastructure.InMemoryRepository;

public class RentRepository(DataSeed data) : BaseRepository<Rent, uint>(data.Rents)
{
    protected override uint GetEntityId(Rent rent) => rent.Id;

    protected override void SetEntityId(Rent rent, uint id) => rent.Id = id;
}