using AutoMapper;
using CarRental.Application.Contracts.Client;
using CarRental.Application.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

public class ClientService(
    IBaseRepository<Client, Guid> repository,
    IMapper mapper)
    : IApplicationService<ClientDto, ClientCreateUpdateDto, Guid>
{
    public async Task<List<ClientDto>> ReadAll()
    {
        var entities = await repository.ReadAll();
        return mapper.Map<List<ClientDto>>(entities);
    }

    public async Task<ClientDto> Read(Guid id)
    {
        var entity = await repository.Read(id)
            ?? throw new KeyNotFoundException($"Client with Id {id} not found.");

        return mapper.Map<ClientDto>(entity);
    }

    public async Task<ClientDto> Create(ClientCreateUpdateDto dto)
    {
        var entity = mapper.Map<Client>(dto);

        var id = await repository.Create(entity);

        // перечитываем для консистентности
        var savedEntity = await repository.Read(id)
            ?? throw new InvalidOperationException("Created client was not found.");

        return mapper.Map<ClientDto>(savedEntity);
    }

    public async Task<bool> Update(ClientCreateUpdateDto dto, Guid id)
    {
        var existing = await repository.Read(id);
        if (existing is null)
            return false;

        mapper.Map(dto, existing);

        return await repository.Update(existing, id);
    }

    public async Task<bool> Delete(Guid id)
        => await repository.Delete(id);
}
