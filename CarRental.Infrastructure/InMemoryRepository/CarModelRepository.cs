using CarRental.Domain;
using CarRental.Domain.DataModels;
using CarRental.Domain.DataSeed;

namespace CarRental.Infrastructure.InMemoryRepository;

public class RentRepository(DataSeed data) : BaseRepository<CarModel, uint>(data.Models)
{
    protected override uint GetEntityId(CarModel model) => model.Id;

    protected override void SetEntityId(CarModel model, uint id) => model.Id = id;
}