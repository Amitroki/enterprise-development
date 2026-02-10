using Mapster;
using CarRental.Application.Contracts.CarModel;
using CarRental.Application.Interfaces;
using CarRental.Domain.Interfaces;
using CarRental.Domain.InternalData.ComponentClasses;

namespace CarRental.Application.Services;

/// <summary>
/// Service for managing car model business logic and DTO mapping
/// </summary>
/// <param name="repository">The car model data repository.</param>
public class CarModelService(
    IBaseRepository<CarModel, Guid> repository)
    : IApplicationService<CarModelDto, CarModelCreateUpdateDto, Guid>
{
    /// <summary>
    /// Creates a new car model and returns the result without re-querying the database
    /// </summary>
    /// <param name="dto">The data transfer object for creating a car model</param>
    /// <returns>The newly created car model DTO.</returns>
    public async Task<CarModelDto> Create(CarModelCreateUpdateDto dto)
    {
        var entity = dto.Adapt<CarModel>();
        var id = await repository.Create(entity);
        entity.Id = id;
        return entity.Adapt<CarModelDto>();
    }

    /// <summary>
    /// Retrieves a specific car model by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the car model</param>
    /// <returns>The mapped car model DTO</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the car model is not found</exception>
    public async Task<CarModelDto?> Read(Guid id)
    {
        var entity = await repository.Read(id)
            ?? throw new KeyNotFoundException($"CarModel with Id {id} not found.");
        return entity.Adapt<CarModelDto>();
    }

    /// <summary>
    /// Retrieves all car models from the repository
    /// </summary>
    /// <returns>A list of car model DTOs</returns>
    public async Task<List<CarModelDto>> ReadAll()
    {
        var entities = await repository.ReadAll();
        return entities.Adapt<List<CarModelDto>>();
    }

    /// <summary>
    /// Updates an existing car model's information
    /// </summary>
    /// <param name="dto">The updated car model data</param>
    /// <param name="id">The identifier of the model to update</param>
    /// <returns>True if the update succeeded; otherwise, false</returns>
    public async Task<bool> Update(CarModelCreateUpdateDto dto, Guid id)
    {
        var existing = await repository.Read(id);
        if (existing is null) return false;

        dto.Adapt(existing);
        return await repository.Update(existing, id);
    }

    /// <summary>
    /// Deletes a car model record by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the car model to remove</param>
    /// <returns>True if the deletion was successful</returns>
    public async Task<bool> Delete(Guid id)
        => await repository.Delete(id);
}
