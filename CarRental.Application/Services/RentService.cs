using CarRental.Application.Contracts.Rent;
using CarRental.Application.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;
using Mapster;

namespace CarRental.Application.Services;

/// <summary>
/// Managing associations between cars, clients, and rental periods.
/// </summary>
public class RentService(
    IBaseRepository<Rent> repository,
    IBaseRepository<Car> carRepository,
    IBaseRepository<Client> clientRepository)
    : IApplicationService<RentDto, RentCreateUpdateDto>
{
    /// <summary>
    /// Retrieves all rental records, performing safety checks for deleted clients to ensure data integrity during mapping.
    /// </summary>
    public async Task<List<RentDto>> ReadAll()
    {
        var rents = await repository.ReadAll();
        foreach (var rent in rents)
        {
            var rep = await clientRepository.Read(rent.Client!.Id);
            if (rep == null)
            {
                rent.Client = null!;
            }
        }
        return rents.Select(r => r.Adapt<RentDto>()).ToList();
    }

    /// <summary>
    /// Retrieves a specific rental agreement by its identifier.
    /// </summary>
    public async Task<RentDto?> Read(int id)
    {
        var rep = await repository.Read(id);
        return rep.Adapt<RentDto>();
    }

    /// <summary>
    /// Creates a new rental agreement after validating that both the requested car and client exist.
    /// </summary>
    /// <returns>The created rental DTO, or null if validation fails.</returns>
    public async Task<RentDto> Create(RentCreateUpdateDto dto)
    {
        var car = await carRepository.Read(dto.CarId);
        var client = await clientRepository.Read(dto.ClientId);
        if (car == null || client == null)
        {
            throw new Exception("Car or client is not found");
        }
        var entity = dto.Adapt<Rent>();
        entity.Car = car;
        entity.Client = client;

        var id = await repository.Create(entity);
        var savedEntity = await repository.Read(id);

        return savedEntity!.Adapt<RentDto>();
    }

    /// <summary>
    /// Updates an existing rental agreement's details.
    /// </summary>
    public async Task<bool> Update(RentCreateUpdateDto dto, int id)
    {
        var existing = await repository.Read(id);
        if (existing is null) return false;
        dto.Adapt(existing);
        var res = await repository.Update(existing, id);
        return res;
    }

    /// <summary>
    /// Permanently removes a rental record from the system.
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        var res = await repository.Delete(id);
        return res;
    }
}