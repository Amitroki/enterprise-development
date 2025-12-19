using CarRental.Domain.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.DataSeed;

namespace CarRental.Infrastructure.InMemoryRepository;

public class CarModelGenerationRepository(DataSeed data) : BaseRepository<CarModelGeneration>(data.Generations)
{
    protected override uint GetEntityId(CarModelGeneration generation) => generation.Id;

    protected override void SetEntityId(CarModelGeneration generation, uint id) => generation.Id = id;
}