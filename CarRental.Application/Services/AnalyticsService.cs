using AutoMapper;
using CarRental.Application.Contracts.Analytics;
using CarRental.Application.Contracts.Client;
using CarRental.Application.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;
using CarRental.Domain.InternalData.ComponentClasses;

namespace CarRental.Application.Services;

/// <summary>
/// Service for performing various analytical queries and reporting on car rental data
/// </summary>
/// <param name="rentRepository">Repository for rental records</param>
/// <param name="carRepository">Repository for car data</param>
/// <param name="carModelRepository">Repository for car model definitions</param>
/// <param name="carModelGenerationRepository">Repository for car generation data</param>
/// <param name="clientRepository">Repository for client information</param>
/// <param name="mapper">AutoMapper instance for DTO conversion</param>
public class AnalyticsService(
    IBaseRepository<Rent, Guid> rentRepository,
    IBaseRepository<Car, Guid> carRepository,
    IBaseRepository<CarModel, Guid> carModelRepository,
    IBaseRepository<CarModelGeneration, Guid> carModelGenerationRepository,
    IBaseRepository<Client, Guid> clientRepository,
    IMapper mapper)
    : IAnalyticsService
{
    /// <summary>
    /// Finds all clients who have rented a specific car model identified by its name
    /// </summary>
    /// <param name="modelName">The name (or part of the name) of the car model</param>
    /// <returns>A list of unique clients who rented the specified model, ordered by name</returns>
    public async Task<List<ClientDto>> ReadClientsByModelName(string modelName)
    {
        var rents = await rentRepository.ReadAll();
        var cars = await carRepository.ReadAll();
        var generations = await carModelGenerationRepository.ReadAll();
        var models = await carModelRepository.ReadAll();
        var clients = await clientRepository.ReadAll();
        var filteredModelIds = models
            .Where(m => m.Name.Contains(modelName, StringComparison.OrdinalIgnoreCase))
            .Select(m => m.Id)
            .ToHashSet();
        var validGenIds = generations
            .Where(g => filteredModelIds.Contains(g.ModelId))
            .Select(g => g.Id)
            .ToHashSet();
        var validCarIds = cars
            .Where(c => validGenIds.Contains(c.ModelGenerationId))
            .Select(c => c.Id)
            .ToHashSet();
        var clientIdsWithTargetCar = rents
            .Where(r => validCarIds.Contains(r.CarId))
            .Select(r => r.ClientId)
            .Distinct()
            .ToHashSet();

        return clients
            .Where(c => clientIdsWithTargetCar.Contains(c.Id))
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .Select(c => mapper.Map<ClientDto>(c))
            .ToList();
    }

    /// <summary>
    /// Identifies all cars that are currently or were rented at a specific point in time
    /// </summary>
    /// <param name="atTime">The date and time to check for active rentals</param>
    /// <returns>A list of cars that were in rent at the specified time</returns>
    public async Task<List<CarInRentDto>> ReadCarsInRent(DateTime atTime)
    {
        var allRents = await rentRepository.ReadAll();
        var activeRents = allRents
            .Where(r => r.StartDateTime <= atTime &&
                        atTime < r.StartDateTime.AddHours(r.Duration))
            .ToList();

        if (activeRents.Count == 0) return [];

        var allCars = await carRepository.ReadAll();
        var allGens = await carModelGenerationRepository.ReadAll();
        var allModels = await carModelRepository.ReadAll();

        var carsDict = allCars.ToDictionary(c => c.Id);
        var gensDict = allGens.ToDictionary(g => g.Id);
        var modelsDict = allModels.ToDictionary(m => m.Id);

        return activeRents
            .Select(r =>
            {
                var car = carsDict.GetValueOrDefault(r.CarId);
                if (car is null) return null;

                var gen = gensDict.GetValueOrDefault(car.ModelGenerationId);
                var model = gen != null ? modelsDict.GetValueOrDefault(gen.ModelId) : null;

                return new CarInRentDto(
                    car.Id,
                    model?.Name ?? "Unknown Model",
                    car.NumberPlate,
                    r.StartDateTime,
                    (int)r.Duration
                );
            })
            .Where(x => x is not null)
            .OrderBy(x => x!.NumberPlate)
            .ToList()!;
    }

    /// <summary>
    /// Retrieves the top 5 cars with the highest total number of rental transactions
    /// </summary>
    /// <returns>A list of the 5 most frequently rented cars with their rental counts</returns>
    public async Task<List<CarWithRentalCountDto>> ReadTop5MostRentedCars()
    {
        var allRents = await rentRepository.ReadAll();
        var allCars = await carRepository.ReadAll();
        var allGens = await carModelGenerationRepository.ReadAll();
        var allModels = await carModelRepository.ReadAll();

        var carStats = allRents
            .GroupBy(r => r.CarId)
            .Select(g => new { Id = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToList();

        var carsDict = allCars.ToDictionary(c => c.Id);
        var gensDict = allGens.ToDictionary(g => g.Id);
        var modelsDict = allModels.ToDictionary(m => m.Id);

        return carStats
            .Select(stat =>
            {
                var car = carsDict.GetValueOrDefault(stat.Id);
                if (car is null) return null;
                var gen = gensDict.GetValueOrDefault(car.ModelGenerationId);
                var model = gen != null ? modelsDict.GetValueOrDefault(gen.ModelId) : null;

                return new CarWithRentalCountDto(
                    car.Id,
                    model?.Name ?? "Unknown Model",
                    car.NumberPlate,
                    stat.Count
                );
            })
            .Where(x => x is not null)
            .OrderByDescending(x => x!.RentalCount)
            .ToList()!;
    }

    /// <summary>
    /// Calculates the total number of rentals for every car in the system
    /// </summary>
    /// <returns>A complete list of cars and how many times each has been rented</returns>
    public async Task<List<CarWithRentalCountDto>> ReadAllCarsWithRentalCount()
    {
        var allRents = await rentRepository.ReadAll();
        var allCars = await carRepository.ReadAll();
        var allGens = await carModelGenerationRepository.ReadAll();
        var allModels = await carModelRepository.ReadAll();

        var rentCounts = allRents.GroupBy(r => r.CarId).ToDictionary(g => g.Key, g => g.Count());
        var gensDict = allGens.ToDictionary(g => g.Id);
        var modelsDict = allModels.ToDictionary(m => m.Id);

        return allCars
            .Select(car =>
            {
                var gen = gensDict.GetValueOrDefault(car.ModelGenerationId);
                var model = gen != null ? modelsDict.GetValueOrDefault(gen.ModelId) : null;

                return new CarWithRentalCountDto(
                    car.Id,
                    model?.Name ?? "Unknown Model",
                    car.NumberPlate,
                    rentCounts.GetValueOrDefault(car.Id, 0)
                );
            })
            .OrderBy(x => x.NumberPlate)
            .ToList();
    }

    /// <summary>
    /// Identifies the top 5 clients who have spent the most money on rentals based on duration and hourly cost
    /// </summary>
    /// <returns>A list of the 5 highest-paying clients with their total spent amounts</returns>
    public async Task<List<ClientWithTotalAmountDto>> ReadTop5ClientsByTotalAmount()
    {
        var allRents = await rentRepository.ReadAll();
        var allCars = await carRepository.ReadAll();
        var allGens = await carModelGenerationRepository.ReadAll();
        var allClients = await clientRepository.ReadAll();

        var carsDict = allCars.ToDictionary(c => c.Id);
        var gensDict = allGens.ToDictionary(g => g.Id);
        var clientsDict = allClients.ToDictionary(c => c.Id);

        var topStats = allRents
            .GroupBy(r => r.ClientId)
            .Select(g =>
            {
                var total = g.Sum(r =>
                {
                    var car = carsDict.GetValueOrDefault(r.CarId);
                    var gen = car != null ? gensDict.GetValueOrDefault(car.ModelGenerationId) : null;
                    return (decimal)r.Duration * (gen?.HourCost ?? 0m);
                });
                return new { ClientId = g.Key, Amount = total, Count = g.Count() };
            })
            .OrderByDescending(x => x.Amount)
            .Take(5)
            .ToList();

        return topStats
            .Select(s =>
            {
                var client = clientsDict.GetValueOrDefault(s.ClientId);
                if (client is null) return null;

                return new ClientWithTotalAmountDto(
                    client.Id,
                    client.FirstName,
                    client.LastName,
                    client.Patronymic,
                    s.Amount,
                    s.Count
                );
            })
            .Where(x => x is not null)
            .ToList()!;
    }
}
