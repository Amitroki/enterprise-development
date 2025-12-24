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
    public async Task<List<CarDto>> ReadAll()
    {
        var rep = await repository.ReadAll();
        return rep.Select(e => e.Adapt<CarDto>()).ToList();
    } 

    /// <summary>
    /// Retrieves a specific car by its identifier.
    /// </summary>
    public async Task<CarDto?> Read(int id)
    {
        var rep = await repository.Read(id);
        return rep.Adapt<CarDto>();
    }

    /// <summary>
    /// Creates a new car record after validating the associated model generation.
    /// </summary>
    /// <exception cref="Exception">Thrown when the specified ModelGenerationId does not exist.</exception>
    public async Task<CarDto> Create(CarCreateUpdateDto dto)
    {
        var entity = dto.Adapt<Car>();
        var fullGeneration = await generationRepository.Read(dto.ModelGenerationId);

        if (fullGeneration == null)
            throw new Exception("Generation not found");
        entity.ModelGeneration = fullGeneration;
        var id = await repository.Create(entity);
        var savedEntity = await repository.Read(id);
        return savedEntity!.Adapt<CarDto>();
    }

    /// <summary>
    /// Updates an existing car's information and its relationship with a model generation.
    /// </summary>
    public async Task<bool> Update(CarCreateUpdateDto dto, int id)
    {
        var existing = await repository.Read(id);
        if (existing is null) return false;
        dto.Adapt(existing);
        var fullGeneration = await generationRepository.Read(dto.ModelGenerationId);
        if (fullGeneration != null)
        {
            existing.ModelGeneration = fullGeneration;
        }
        var res = await repository.Update(existing, id);
        return res;
    }

    /// <summary>
    /// Deletes a car record by its identifier.
    /// </summary>
    public async Task<bool> Delete(int id) {
        var rep = await repository.Delete(id);
        return rep;
    }
}