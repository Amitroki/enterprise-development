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
    public List<RentDto> ReadAll()
    {
        var rents = repository.ReadAll();
        foreach (var rent in rents)
        {
            if (clientRepository.Read(rent.Client!.Id) == null)
            {
                rent.Client = null!;
            }
        }
        return rents.Select(r => r.Adapt<RentDto>()).ToList();
    }

    /// <summary>
    /// Retrieves a specific rental agreement by its identifier.
    /// </summary>
    public RentDto? Read(uint id) =>
        repository.Read(id)?.Adapt<RentDto>();

    /// <summary>
    /// Creates a new rental agreement after validating that both the requested car and client exist.
    /// </summary>
    /// <returns>The created rental DTO, or null if validation fails.</returns>
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

    /// <summary>
    /// Updates an existing rental agreement's details.
    /// </summary>
    public bool Update(RentCreateUpdateDto dto, uint id)
    {
        var existing = repository.Read(id);
        if (existing is null) return false;
        dto.Adapt(existing);
        return repository.Update(existing, id);
    }

    /// <summary>
    /// Permanently removes a rental record from the system.
    /// </summary>
    public bool Delete(uint id) => repository.Delete(id);
}