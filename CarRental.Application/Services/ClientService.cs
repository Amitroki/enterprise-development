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
    public async Task<List<ClientDto>> ReadAll() {
        var rep = await repository.ReadAll();
        return rep.Select(e => e.Adapt<ClientDto>()).ToList();
    }

    /// <summary>
    /// Finds a specific client by their unique identifier.
    /// </summary>
    public async Task<ClientDto?> Read(int id) {
        var rep = await repository.Read(id);
        return rep.Adapt<ClientDto>();
    }

    /// <summary>
    /// Registers a new client in the system.
    /// </summary>
    public async Task<ClientDto> Create(ClientCreateUpdateDto dto)
    {
        var entity = dto.Adapt<Client>();
        var id = await repository.Create(entity);
        var savedEntity = await repository.Read(id);
        return savedEntity!.Adapt<ClientDto>();
    }

    /// <summary>
    /// Updates an existing client's personal and contact information.
    /// </summary>
    public async Task<bool> Update(ClientCreateUpdateDto dto, int id)
    {
        var existing = await repository.Read(id);
        if (existing is null) return false;
        dto.Adapt(existing);
        var res = await repository.Update(existing, id);
        return res;
    }

    /// <summary>
    /// Removes a client record from the database.
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        var res = await repository.Delete(id);
        return res;
    }
}