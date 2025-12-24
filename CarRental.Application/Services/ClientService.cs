using Mapster;
using CarRental.Application.Contracts.Client;
using CarRental.Application.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Manages client-related operations, including registration and profile management.
/// </summary>
public class ClientService(IBaseRepository<Client> repository) : IApplicationService<ClientDto, ClientCreateUpdateDto>
{
    /// <summary>
    /// Retrieves a complete list of registered clients.
    /// </summary>
    public List<ClientDto> ReadAll() =>
        repository.ReadAll().Select(e => e.Adapt<ClientDto>()).ToList();

    /// <summary>
    /// Finds a specific client by their unique identifier.
    /// </summary>
    public ClientDto? Read(int id) =>
        repository.Read(id)?.Adapt<ClientDto>();

    /// <summary>
    /// Registers a new client in the system.
    /// </summary>
    public ClientDto Create(ClientCreateUpdateDto dto)
    {
        var entity = dto.Adapt<Client>();
        var id = repository.Create(entity);
        var savedEntity = repository.Read(id);
        return savedEntity!.Adapt<ClientDto>();
    }

    /// <summary>
    /// Updates an existing client's personal and contact information.
    /// </summary>
    public bool Update(ClientCreateUpdateDto dto, int id)
    {
        var existing = repository.Read(id);
        if (existing is null) return false;
        dto.Adapt(existing);
        return repository.Update(existing, id);
    }

    /// <summary>
    /// Removes a client record from the database.
    /// </summary>
    public bool Delete(int id) => repository.Delete(id);
}