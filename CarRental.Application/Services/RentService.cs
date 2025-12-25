using AutoMapper;
using CarRental.Application.Contracts.Rent;
using CarRental.Application.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

public class RentService(
    IBaseRepository<Rent> repository,
    IBaseRepository<Car> carRepository,
    IBaseRepository<Client> clientRepository,
    IMapper mapper)
    : IApplicationService<RentDto, RentCreateUpdateDto>
{
    public async Task<List<RentDto>> ReadAll()
    {
        var rents = await repository.ReadAll();
        return mapper.Map<List<RentDto>>(rents);
    }

    public async Task<RentDto?> Read(int id)
    {
        var entity = await repository.Read(id);
        return entity == null ? null : mapper.Map<RentDto>(entity);
    }

    public async Task<RentDto> Create(RentCreateUpdateDto dto)
    {
        var car = await carRepository.Read(dto.CarId);
        var client = await clientRepository.Read(dto.ClientId);

        if (car == null || client == null)
            throw new Exception("Car or client is not found");

        var entity = mapper.Map<Rent>(dto);
        entity.Car = car;
        entity.Client = client;

        var id = await repository.Create(entity);
        var savedEntity = await repository.Read(id);

        return mapper.Map<RentDto>(savedEntity!);
    }

    public async Task<bool> Update(RentCreateUpdateDto dto, int id)
    {
        var existing = await repository.Read(id);
        if (existing is null) return false;

        mapper.Map(dto, existing);

        var car = await carRepository.Read(dto.CarId);
        var client = await clientRepository.Read(dto.ClientId);

        if (car != null) existing.Car = car;
        if (client != null) existing.Client = client;

        return await repository.Update(existing, id);
    }

    public async Task<bool> Delete(int id) => await repository.Delete(id);
}