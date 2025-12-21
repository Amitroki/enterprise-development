using CarRental.Domain.Interfaces;
using CarRental.Domain.InternalData.ComponentClasses;
using CarRental.Domain.DataSeed;

namespace CarRental.Infrastructure.InMemoryRepository;

public class CarModelRepository(DataSeed data) : BaseRepository<CarModel>(data.Models)
{
    protected override uint GetEntityId(CarModel model) => model.Id;

    protected override void SetEntityId(CarModel model, uint id) => model.Id = id;
}