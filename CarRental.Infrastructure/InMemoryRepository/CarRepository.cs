using CarRental.Domain;
using CarRental.Domain.DataModels;
using CarRental.Domain.DataSeed;

namespace CarRental.Infrastructure.InMemoryRepository;

public class CarRepository(DataSeed data) : BaseRepository<Car, uint>(data.Cars)
{
    protected override uint GetEntityId(Car car) => car.Id;

    protected override void SetEntityId(Car car, uint id) => car.Id = id;
}