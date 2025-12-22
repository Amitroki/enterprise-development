using CarRental.Application.Contracts.Rent;
using CarRental.Application.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.InMemoryRepository;
using Mapster;

namespace CarRental.Application.Services;
public class RentService(
    IBaseRepository<Rent> repository,
    IBaseRepository<Car> carRepository,
    IBaseRepository<Client> clientRepository)
    : IApplicationService<RentDto, RentCreateUpdateDto>
{
    public List<RentDto> ReadAll()
    {
        var rents = repository.ReadAll();
        foreach (var rent in rents)
        {
            if (clientRepository.Read(rent.Client.Id) == null)
            {
                rent.Client = null;
            }
        }
        return rents.Select(r => r.Adapt<RentDto>()).ToList();
    }

    public RentDto? Read(uint id) =>
        repository.Read(id)?.Adapt<RentDto>();

    public RentDto? Create(RentCreateUpdateDto dto)
    {
        var car = carRepository.Read(dto.CarId);
        var client = clientRepository.Read(dto.ClientId);
        if (car == null || client == null)
        {
            return null;
        }
        var entity = dto.Adapt<Rent>();
        entity.Car = car;
        entity.Client = client;

        var id = repository.Create(entity);
        var savedEntity = repository.Read(id);

        return savedEntity!.Adapt<RentDto>();
    }

    public bool Update(RentCreateUpdateDto dto, uint id)
    {
        var existing = repository.Read(id);
        if (existing is null) return false;
        dto.Adapt(existing);
        return repository.Update(existing, id);
    }

    public bool Delete(uint id) => repository.Delete(id);
}