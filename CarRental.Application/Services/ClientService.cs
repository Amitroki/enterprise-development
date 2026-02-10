using Mapster;
using CarRental.Application.Contracts.Client;
using CarRental.Application.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Service for managing client-related business logic and DTO mapping.
/// </summary>
/// <param name="repository">The client data repository.</param>
public class ClientService(
    IBaseRepository<Client, Guid> repository)
    : IApplicationService<ClientDto, ClientCreateUpdateDto, Guid>
{
    /// <summary>
    /// Retrieves all clients as a list of DTOs.
    /// </summary>
    /// <returns>A list of client data transfer objects.</returns>
    public async Task<List<ClientDto>> ReadAll()
    {
        var entities = await repository.ReadAll();
        return entities.Adapt<List<ClientDto>>();
    }

    /// <summary>
    /// Retrieves a specific client by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the client</param>
    /// <returns>The mapped client DTO</returns>
    /// <exception cref="KeyNotFoundException">Thrown when no client exists with the given ID</exception>
    public async Task<ClientDto?> Read(Guid id)
    {
        var entity = await repository.Read(id)
            ?? throw new KeyNotFoundException($"Client with Id {id} not found.");
        return entity.Adapt<ClientDto>();
    }

    /// <summary>
    /// Creates a new client record and returns the created entity as a DTO
    /// </summary>
    /// <param name="dto">The client data for creation</param>
    /// <returns>The created client DTO</returns>
    /// <exception cref="InvalidOperationException">Thrown if the client cannot be retrieved after creation</exception>
    public async Task<ClientDto> Create(ClientCreateUpdateDto dto)
    {
        var entity = dto.Adapt<Client>();
        var id = await repository.Create(entity);
        var savedEntity = await repository.Read(id)
            ?? throw new InvalidOperationException("Created client was not found.");
        return savedEntity.Adapt<ClientDto>();
    }

    /// <summary>
    /// Updates an existing client record using the provided data
    /// </summary>
    /// <param name="dto">The updated client data</param>
    /// <param name="id">The identifier of the client to update</param>
    /// <returns>True if the update was successful; otherwise, false</returns>
    public async Task<bool> Update(ClientCreateUpdateDto dto, Guid id)
    {
        var existing = await repository.Read(id);
        if (existing is null) return false;
        dto.Adapt(existing);
        return await repository.Update(existing, id);
    }

    /// <summary>
    /// Deletes a client record from the system
    /// </summary>
    /// <param name="id">The unique identifier of the client to remove</param>
    /// <returns>True if the deletion was successful</returns>
    public async Task<bool> Delete(Guid id)
        => await repository.Delete(id);
}