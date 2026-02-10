using Mapster;
using CarRental.Application.Contracts.CarModelGeneration;
using CarRental.Application.Interfaces;
using CarRental.Domain.Interfaces;
using CarRental.Domain.InternalData.ComponentClasses;

namespace CarRental.Application.Services;

/// <summary>
/// Service for managing car model generations, including linking generations to parent car models
/// </summary>
/// <param name="repository">The repository for model generation entities</param>
/// <param name="modelRepository">The repository for car model entities</param>
public class CarModelGenerationService(
    IBaseRepository<CarModelGeneration, Guid> repository,
    IBaseRepository<CarModel, Guid> modelRepository)
    : IApplicationService<CarModelGenerationDto, CarModelGenerationCreateUpdateDto, Guid>
{
    /// <summary>
    /// Creates a new model generation after validating that the associated car model exists
    /// </summary>
    /// <param name="dto">The model generation data transfer object</param>
    /// <returns>The created model generation as a DTO</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the associated CarModel ID is invalid</exception>
    public async Task<CarModelGenerationDto> Create(CarModelGenerationCreateUpdateDto dto)
    {
        var entity = dto.Adapt<CarModelGeneration>();
        var model = await modelRepository.Read(dto.ModelId)
            ?? throw new KeyNotFoundException($"CarModel with Id {dto.ModelId} not found.");
        entity.Model = model;
        entity.ModelId = model.Id;
        var id = await repository.Create(entity);
        entity.Id = id;
        return entity.Adapt<CarModelGenerationDto>();
    }

    /// <summary>
    /// Retrieves a specific model generation by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the generation</param>
    /// <returns>The mapped generation DTO</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the generation record is not found</exception>
    public async Task<CarModelGenerationDto?> Read(Guid id)
    {
        var entity = await repository.Read(id)
            ?? throw new KeyNotFoundException($"CarModelGeneration with Id {id} not found.");
        return entity.Adapt<CarModelGenerationDto>();
    }

    /// <summary>
    /// Retrieves all model generations and asynchronously populates their parent models
    /// </summary>
    /// <returns>A list of model generation DTOs with linked model data</returns>
    public async Task<List<CarModelGenerationDto>> ReadAll()
    {
        var entities = await repository.ReadAll();
        foreach (var generation in entities)
        {
            if (generation.ModelId != Guid.Empty)
            {
                generation.Model = await modelRepository.Read(generation.ModelId);
            }
        }
        return entities.Adapt<List<CarModelGenerationDto>>();
    }

    /// <summary>
    /// Updates an existing model generation and refreshes its link to a car model
    /// </summary>
    /// <param name="dto">The updated data for the generation</param>
    /// <param name="id">The identifier of the generation to update</param>
    /// <returns>True if the update was successful; otherwise, false</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the new parent CarModel is not found</exception>
    public async Task<bool> Update(CarModelGenerationCreateUpdateDto dto, Guid id)
    {
        var existing = await repository.Read(id);
        if (existing is null) return false;
        dto.Adapt(existing);
        var model = await modelRepository.Read(dto.ModelId)
            ?? throw new KeyNotFoundException($"CarModel with Id {dto.ModelId} not found.");
        existing.Model = model;
        existing.ModelId = model.Id;
        return await repository.Update(existing, id);
    }

    /// <summary>
    /// Deletes a car model generation record from the system
    /// </summary>
    /// <param name="id">The identifier of the generation to delete</param>
    /// <returns>True if the deletion was successful</returns>
    public async Task<bool> Delete(Guid id)
        => await repository.Delete(id);
}