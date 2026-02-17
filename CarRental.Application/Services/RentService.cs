using Mapster;
using CarRental.Application.Contracts.Rent;
using CarRental.Application.Contracts.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Service for managing rent business logic and mapping between entities and DTOs
/// </summary>
/// <param name="repository">The rent data repository</param>
/// <param name="carRepository">The car data repository</param>
/// <param name="clientRepository">The client data repository</param>
public class RentService(
    IBaseRepository<Rent, Guid> repository,
    IBaseRepository<Car, Guid> carRepository,
    IBaseRepository<Client, Guid> clientRepository)
    : IApplicationService<RentDto, RentCreateUpdateDto, Guid>
{
    /// <summary>
    /// Retrieves all rent records as DTOs
    /// </summary>
    /// <returns>A list of rent data transfer objects</returns>
    public async Task<List<RentDto>> ReadAll()
    {
        var rents = await repository.ReadAll();
        return rents.Adapt<List<RentDto>>();
    }

    /// <summary>
    /// Retrieves a specific rent by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the rent</param>
    /// <returns>The found rent DTO</returns>
    /// <exception cref="KeyNotFoundException">Thrown if rent is not found</exception>
    public async Task<RentDto?> Read(Guid id)
    {
        var entity = await repository.Read(id)
            ?? throw new KeyNotFoundException($"Rent with Id {id} not found.");
        return entity.Adapt<RentDto>();
    }

    /// <summary>
    /// Creates a new rent record after validating car and client existence
    /// </summary>
    /// <param name="dto">The rent data transfer object for creation</param>
    /// <returns>The created rent as a DTO</returns>
    public async Task<RentDto> Create(RentCreateUpdateDto dto)
    {
        var car = await carRepository.Read(dto.CarId)
            ?? throw new KeyNotFoundException($"Car with Id {dto.CarId} not found.");
        var client = await clientRepository.Read(dto.ClientId)
            ?? throw new KeyNotFoundException($"Client with Id {dto.ClientId} not found.");
        var entity = dto.Adapt<Rent>();
        entity.Car = car;
        entity.Client = client;
        var id = await repository.Create(entity);
        var savedEntity = await repository.Read(id)
            ?? throw new InvalidOperationException("Created rent was not found.");
        return savedEntity.Adapt<RentDto>();
    }

    /// <summary>
    /// Updates an existing rent record and refreshes car/client links if IDs have changed
    /// </summary>
    /// <param name="dto">The updated rent data</param>
    /// <param name="id">The identifier of the rent to update</param>
    /// <returns>True if the update succeeded; otherwise, false</returns>
    public async Task<bool> Update(RentCreateUpdateDto dto, Guid id)
    {
        var existing = await repository.Read(id);
        if (existing is null) return false;
        dto.Adapt(existing);
        if (dto.CarId != existing.Car?.Id)
        {
            var car = await carRepository.Read(dto.CarId)
                ?? throw new KeyNotFoundException($"Car with Id {dto.CarId} not found.");
            existing.Car = car;
        }
        if (dto.ClientId != existing.Client?.Id)
        {
            var client = await clientRepository.Read(dto.ClientId)
                ?? throw new KeyNotFoundException($"Client with Id {dto.ClientId} not found.");
            existing.Client = client;
        }
        return await repository.Update(existing, id);
    }

    /// <summary>
    /// Deletes a rent record by its identifier
    /// </summary>
    /// <param name="id">The identifier of the rent to delete</param>
    /// <returns>True if the deletion succeeded</returns>
    public async Task<bool> Delete(Guid id)
        => await repository.Delete(id);
}