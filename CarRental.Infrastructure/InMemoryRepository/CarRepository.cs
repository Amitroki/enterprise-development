using CarRental.Domain.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.DataSeed;

namespace CarRental.Infrastructure.InMemoryRepository;

public class CarRepository(DataSeed data) : BaseRepository<Car>(data.Cars)
{
    protected override uint GetEntityId(Car car) => car.Id;

    protected override void SetEntityId(Car car, uint id) => car.Id = id;
}