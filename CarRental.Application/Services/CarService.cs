using AutoMapper;
using CarRental.Application.Contracts.Car;
using CarRental.Application.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;
using CarRental.Domain.InternalData.ComponentClasses;

namespace CarRental.Application.Services;

/// <summary>
/// Service for managing car business logic and coordinating data between repositories and DTOs
/// </summary>
/// <param name="repository">The car data repository</param>
/// <param name="generationRepository">The car model generation data repository</param>
/// <param name="mapper">The AutoMapper instance for object mapping</param>
public class CarService(
    IBaseRepository<Car, Guid> repository,
    IBaseRepository<CarModelGeneration, Guid> generationRepository,
    IMapper mapper)
    : IApplicationService<CarDto, CarCreateUpdateDto, Guid>
{
    /// <summary>
    /// Retrieves all cars available in the system as DTOs
    /// </summary>
    /// <returns>A list of car data transfer objects</returns>
    public async Task<List<CarDto>> ReadAll()
    {
        var entities = await repository.ReadAll();
        return mapper.Map<List<CarDto>>(entities);
    }

    /// <summary>
    /// Retrieves a specific car by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the car</param>
    /// <returns>The found car DTO.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the car with the specified ID does not exist</exception>
    public async Task<CarDto?> Read(Guid id)
    {
        var entity = await repository.Read(id)
            ?? throw new KeyNotFoundException($"Car with Id {id} not found.");
        return mapper.Map<CarDto>(entity);
    }

    /// <summary>
    /// Creates a new car record after validating that the associated model generation exists
    /// </summary>
    /// <param name="dto">The data for creating the new car</param>
    /// <returns>The created car as a DTO.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the provided ModelGenerationId is invalid</exception>
    /// <exception cref="InvalidOperationException">Thrown if the car cannot be retrieved after creation</exception>
    public async Task<CarDto> Create(CarCreateUpdateDto dto)
    {
        var generation = await generationRepository.Read(dto.ModelGenerationId);
        if (generation is null)
            throw new KeyNotFoundException($"ModelGeneration with Id {dto.ModelGenerationId} not found.");
        var entity = mapper.Map<Car>(dto);
        var id = await repository.Create(entity);
        var savedEntity = await repository.Read(id)
            ?? throw new InvalidOperationException("Created car was not found.");
        return mapper.Map<CarDto>(savedEntity);
    }

    /// <summary>
    /// Updates an existing car's data and validates the model generation if it has changed
    /// </summary>
    /// <param name="dto">The updated car data.</param>
    /// <param name="id">The unique identifier of the car to update</param>
    /// <returns>True if the update was successful; otherwise, false</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the new ModelGenerationId does not exist</exception>
    public async Task<bool> Update(CarCreateUpdateDto dto, Guid id)
    {
        var existing = await repository.Read(id);
        if (existing is null)
            return false;
        if (dto.ModelGenerationId != existing.ModelGenerationId)
        {
            var generation = await generationRepository.Read(dto.ModelGenerationId);
            if (generation is null)
                throw new KeyNotFoundException($"ModelGeneration with Id {dto.ModelGenerationId} not found.");
        }
        mapper.Map(dto, existing);
        return await repository.Update(existing, id);
    }

    /// <summary>
    /// Removes a car record from the system
    /// </summary>
    /// <param name="id">The unique identifier of the car to delete</param>
    /// <returns>True if the deletion was successful</returns>
    public async Task<bool> Delete(Guid id)
        => await repository.Delete(id);
}