using Mapster;
using CarRental.Application.Contracts.Car;
using CarRental.Application.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;
using CarRental.Domain.InternalData.ComponentClasses;

namespace CarRental.Application.Services;
public class CarService(
    IBaseRepository<Car> repository,
    IBaseRepository<CarModelGeneration> generationRepository)
    : IApplicationService<CarDto, CarCreateUpdateDto>
{
    public List<CarDto> ReadAll() =>
        repository.ReadAll().Select(e => e.Adapt<CarDto>()).ToList();

    public CarDto? Read(uint id) =>
        repository.Read(id)?.Adapt<CarDto>();

    public CarDto Create(CarCreateUpdateDto dto)
    {
        var entity = dto.Adapt<Car>();
        var fullGeneration = generationRepository.Read(dto.ModelGenerationId);

        if (fullGeneration == null)
            throw new Exception("Generation not found");
        entity.ModelGeneration = fullGeneration;

        var id = repository.Create(entity);
        var savedEntity = repository.Read(id);

        return savedEntity!.Adapt<CarDto>();
    }

    public bool Update(CarCreateUpdateDto dto, uint id)
    {
        var existing = repository.Read(id);
        if (existing is null) return false;

        dto.Adapt(existing);
        var fullGeneration = generationRepository.Read(dto.ModelGenerationId);
        if (fullGeneration != null)
        {
            existing.ModelGeneration = fullGeneration;
        }

        return repository.Update(existing, id);
    }

    public bool Delete(uint id) => repository.Delete(id);
}