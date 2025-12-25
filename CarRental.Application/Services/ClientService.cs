using AutoMapper;
using CarRental.Application.Contracts.Client;
using CarRental.Application.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

public class ClientService(IBaseRepository<Client> repository, IMapper mapper)
    : IApplicationService<ClientDto, ClientCreateUpdateDto>
{
    public async Task<List<ClientDto>> ReadAll()
    {
        var entities = await repository.ReadAll();
        return mapper.Map<List<ClientDto>>(entities);
    }

    public async Task<ClientDto?> Read(int id)
    {
        var entity = await repository.Read(id);
        return entity == null ? null : mapper.Map<ClientDto>(entity);
    }

    public async Task<ClientDto> Create(ClientCreateUpdateDto dto)
    {
        var entity = mapper.Map<Client>(dto);
        var id = await repository.Create(entity);
        var savedEntity = await repository.Read(id);
        return mapper.Map<ClientDto>(savedEntity!);
    }

    public async Task<bool> Update(ClientCreateUpdateDto dto, int id)
    {
        var existing = await repository.Read(id);
        if (existing is null) return false;

        mapper.Map(dto, existing);
        return await repository.Update(existing, id);
    }

    public async Task<bool> Delete(int id) => await repository.Delete(id);
}