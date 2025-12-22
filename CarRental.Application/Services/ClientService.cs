using Mapster;
using CarRental.Application.Contracts.Client;
using CarRental.Application.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

public class ClientService(IBaseRepository<Client> repository) : IApplicationService<ClientDto, ClientCreateUpdateDto>
{
    public List<ClientDto> ReadAll() =>
        repository.ReadAll().Select(e => e.Adapt<ClientDto>()).ToList();

    public ClientDto? Read(uint id) =>
        repository.Read(id)?.Adapt<ClientDto>();

    public ClientDto Create(ClientCreateUpdateDto dto)
    {
        var entity = dto.Adapt<Client>();
        var id = repository.Create(entity);
        var savedEntity = repository.Read(id);
        return savedEntity!.Adapt<ClientDto>();
    }

    public bool Update(ClientCreateUpdateDto dto, uint id)
    {
        var existing = repository.Read(id);
        if (existing is null) return false;
        dto.Adapt(existing);
        return repository.Update(existing, id);
    }

    public bool Delete(uint id) => repository.Delete(id);
}