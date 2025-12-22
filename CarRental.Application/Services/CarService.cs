using Mapster;
using CarRental.Application.Contracts.Car;
using CarRental.Application.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;
using CarRental.Domain.InternalData.ComponentClasses;

namespace CarRental.Application.Services;

/// <summary>
/// Provides CRUD operations for car entities, including relationship management with car model generations.
/// </summary>
public class CarService(
    IBaseRepository<Car> repository,
    IBaseRepository<CarModelGeneration> generationRepository)
    : IApplicationService<CarDto, CarCreateUpdateDto>
{
    /// <summary>
    /// Retrieves all car records and maps them to DTOs.
    /// </summary>
    public List<CarDto> ReadAll() =>
        repository.ReadAll().Select(e => e.Adapt<CarDto>()).ToList();

    /// <summary>
    /// Retrieves a specific car by its identifier.
    /// </summary>
    public CarDto? Read(uint id) =>
        repository.Read(id)?.Adapt<CarDto>();

    /// <summary>
    /// Creates a new car record after validating the associated model generation.
    /// </summary>
    /// <exception cref="Exception">Thrown when the specified ModelGenerationId does not exist.</exception>
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

    /// <summary>
    /// Updates an existing car's information and its relationship with a model generation.
    /// </summary>
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

    /// <summary>
    /// Deletes a car record by its identifier.
    /// </summary>
    public bool Delete(uint id) => repository.Delete(id);
}