using AutoMapper;
using CarRental.Application.Contracts.Rent;
using CarRental.Application.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

public class RentService(
    IBaseRepository<Rent, Guid> repository,
    IBaseRepository<Car, Guid> carRepository,
    IBaseRepository<Client, Guid> clientRepository,
    IMapper mapper)
    : IApplicationService<RentDto, RentCreateUpdateDto, Guid>
{
    public async Task<List<RentDto>> ReadAll()
    {
        var rents = await repository.ReadAll();
        return mapper.Map<List<RentDto>>(rents);
    }

    public async Task<RentDto> Read(Guid id)
    {
        var entity = await repository.Read(id)
            ?? throw new KeyNotFoundException($"Rent with Id {id} not found.");

        return mapper.Map<RentDto>(entity);
    }

    public async Task<RentDto> Create(RentCreateUpdateDto dto)
    {
        var car = await carRepository.Read(dto.CarId)
            ?? throw new KeyNotFoundException($"Car with Id {dto.CarId} not found.");
        var client = await clientRepository.Read(dto.ClientId)
            ?? throw new KeyNotFoundException($"Client with Id {dto.ClientId} not found.");
        var entity = mapper.Map<Rent>(dto);
        entity.Car = car;
        entity.Client = client;
        var id = await repository.Create(entity);
        var savedEntity = await repository.Read(id)
            ?? throw new InvalidOperationException("Created rent was not found.");
        return mapper.Map<RentDto>(savedEntity);
    }

    public async Task<bool> Update(RentCreateUpdateDto dto, Guid id)
    {
        var existing = await repository.Read(id);
        if (existing is null)
            return false;
        mapper.Map(dto, existing);
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

    public async Task<bool> Delete(Guid id)
        => await repository.Delete(id);
}
