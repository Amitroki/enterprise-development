using Mapster;
using CarRental.Application.Contracts.Analytics;
using CarRental.Application.Contracts.Client;
using CarRental.Application.Contracts.Interfaces;
using CarRental.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Services;

/// <summary>
/// Service for performing various analytical queries and reporting on car rental data
/// </summary>
/// <param name="context">Database context used for executing optimized LINQ queries against the car rental entities</param>
public class AnalyticsService(CarRentalDbContext context)
    : IAnalyticsService
{
    /// <summary>
    /// Finds all clients who have rented a specific car model identified by its name
    /// </summary>
    /// <param name="modelName">The name (or part of the name) of the car model</param>
    /// <returns>A list of unique clients who rented the specified model, ordered by name</returns>
    public async Task<List<ClientDto>> ReadClientsByModelName(string modelName)
    {
        var modelIds = await context.CarModels
            .AsNoTracking()
            .Where(m => m.Name.Contains(modelName))
            .Select(m => m.Id)
            .ToListAsync();
        if (!modelIds.Any()) return new List<ClientDto>();
        var generationIds = await context.ModelGenerations
            .AsNoTracking()
            .Where(mg => modelIds.Contains(mg.ModelId))
            .Select(mg => mg.Id)
            .ToListAsync();
        var carIds = await context.Cars
            .AsNoTracking()
            .Where(c => generationIds.Contains(c.ModelGenerationId))
            .Select(c => c.Id)
            .ToListAsync();
        var clientIds = await context.Rents
            .AsNoTracking()
            .Where(r => carIds.Contains(r.CarId))
            .Select(r => r.ClientId)
            .Distinct()
            .ToListAsync();
        var clients = await context.Clients
            .AsNoTracking()
            .Where(cl => clientIds.Contains(cl.Id))
            .OrderBy(cl => cl.LastName)
            .ThenBy(cl => cl.FirstName)
            .ToListAsync();

        return clients.Adapt<List<ClientDto>>();
    }

    /// <summary>
    /// Identifies all cars that are currently or were rented at a specific point in time
    /// </summary>
    /// <param name="atTime">The date and time to check for active rentals</param>
    /// <returns>A list of cars that were in rent at the specified time</returns>
    public async Task<List<CarInRentDto>> ReadCarsInRent(DateTime atTime)
    {
        var activeRents = await context.Rents
            .AsNoTracking()
            .Where(r => r.StartDateTime <= atTime)
            .ToListAsync();
        var filteredRents = activeRents
            .Where(r => r.StartDateTime.AddHours(r.Duration) > atTime)
            .ToList();
        if (!filteredRents.Any()) return new List<CarInRentDto>();
        var carIds = filteredRents.Select(r => r.CarId).Distinct().ToList();
        var cars = await context.Cars
            .AsNoTracking()
            .Where(c => carIds.Contains(c.Id))
            .ToListAsync();
        var generationIds = cars.Select(c => c.ModelGenerationId).Distinct().ToList();
        var generations = await context.ModelGenerations
            .AsNoTracking()
            .Where(mg => generationIds.Contains(mg.Id))
            .ToListAsync();
        var modelIds = generations.Select(mg => mg.ModelId).Distinct().ToList();
        var models = await context.CarModels
            .AsNoTracking()
            .Where(m => modelIds.Contains(m.Id))
            .ToListAsync();
        var result = filteredRents.Select(r =>
        {
            var car = cars.First(c => c.Id == r.CarId);
            var gen = generations.First(g => g.Id == car.ModelGenerationId);
            var model = models.First(m => m.Id == gen.ModelId);

            return new CarInRentDto(
                car.Id,
                model.Name,
                car.NumberPlate,
                r.StartDateTime,
                (int)r.Duration
            );
        })
        .OrderBy(x => x.NumberPlate)
        .ToList();

        return result;
    }

    /// <summary>
    /// Retrieves the top 5 cars with the highest total number of rental transactions
    /// </summary>
    /// <returns>A list of the 5 most frequently rented cars with their rental counts</returns>
    public async Task<List<CarWithRentalCountDto>> ReadTop5MostRentedCars()
    {
        var allRentCarIds = await context.Rents
            .AsNoTracking()
            .Select(r => r.CarId)
            .ToListAsync();
        if (!allRentCarIds.Any()) return new List<CarWithRentalCountDto>();
        var topStats = allRentCarIds
            .GroupBy(id => id)
            .Select(g => new { CarId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToList();
        var topCarIds = topStats.Select(x => x.CarId).ToList();
        var cars = await context.Cars.AsNoTracking().Where(c => topCarIds.Contains(c.Id)).ToListAsync();
        var generationIds = cars.Select(c => c.ModelGenerationId).Distinct().ToList();
        var generations = await context.ModelGenerations.AsNoTracking().Where(mg => generationIds.Contains(mg.Id)).ToListAsync();
        var modelIds = generations.Select(mg => mg.ModelId).Distinct().ToList();
        var models = await context.CarModels.AsNoTracking().Where(m => modelIds.Contains(m.Id)).ToListAsync();

        return topStats.Select(stat =>
        {
            var car = cars.First(c => c.Id == stat.CarId);
            var gen = generations.First(g => g.Id == car.ModelGenerationId);
            var model = models.First(m => m.Id == gen.ModelId);
            return new CarWithRentalCountDto(car.Id, model.Name, car.NumberPlate, stat.Count);
        }).ToList();
    }

    /// <summary>
    /// Calculates the total number of rentals for every car in the system
    /// </summary>
    /// <returns>A complete list of cars and how many times each has been rented</returns>
    public async Task<List<CarWithRentalCountDto>> ReadAllCarsWithRentalCount()
    {
        var allRentCarIds = await context.Rents
            .AsNoTracking()
            .Select(r => r.CarId)
            .ToListAsync();
        var rentDict = allRentCarIds
            .GroupBy(id => id)
            .ToDictionary(g => g.Key, g => g.Count());
        var cars = await context.Cars.AsNoTracking().ToListAsync();
        var generations = await context.ModelGenerations.AsNoTracking().ToListAsync();
        var models = await context.CarModels.AsNoTracking().ToListAsync();

        return cars.Select(car =>
        {
            var gen = generations.First(g => g.Id == car.ModelGenerationId);
            var model = models.First(m => m.Id == gen.ModelId);
            rentDict.TryGetValue(car.Id, out var count);

            return new CarWithRentalCountDto(car.Id, model.Name, car.NumberPlate, count);
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
        var rents = await context.Rents.AsNoTracking().ToListAsync();
        var cars = await context.Cars.AsNoTracking().ToListAsync();
        var generations = await context.ModelGenerations.AsNoTracking().ToListAsync();
        var clientStats = rents
            .GroupBy(r => r.ClientId)
            .Select(g =>
            {
                var totalAmount = g.Sum(r =>
                {
                    var car = cars.FirstOrDefault(c => c.Id == r.CarId);
                    var gen = generations.FirstOrDefault(gn => gn.Id == car?.ModelGenerationId);
                    return (decimal)r.Duration * (gen?.HourCost ?? 0);
                });

                return new
                {
                    ClientId = g.Key,
                    Amount = totalAmount,
                    Count = g.Count()
                };
            })
            .OrderByDescending(x => x.Amount)
            .Take(5)
            .ToList();
        if (!clientStats.Any()) return new List<ClientWithTotalAmountDto>();
        var topClientIds = clientStats.Select(x => x.ClientId).ToList();
        var clients = await context.Clients
            .AsNoTracking()
            .Where(c => topClientIds.Contains(c.Id))
            .ToListAsync();
        var result = clientStats.Select(stat =>
        {
            var client = clients.First(c => c.Id == stat.ClientId);
            return new ClientWithTotalAmountDto(
                client.Id,
                client.FirstName,
                client.LastName,
                client.Patronymic,
                stat.Amount,
                stat.Count
            );
        }).ToList();

        return result;
    }
}
