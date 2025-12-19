using CarRental.Domain.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.DataSeed;

namespace CarRental.Infrastructure.InMemoryRepository;

public class RentRepository(DataSeed data) : BaseRepository<Rent>(data.Rents)
{
    protected override uint GetEntityId(Rent rent) => rent.Id;

    protected override void SetEntityId(Rent rent, uint id) => rent.Id = id;
}