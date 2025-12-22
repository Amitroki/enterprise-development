using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Car;
using CarRental.Application.Contracts.Client;
using CarRental.Application.Contracts.Rent;
using CarRental.Application.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;
using Mapster;

namespace CarRental.Application.Services;

/// <summary>
/// Implements business logic for data aggregation and rental statistics.
/// </summary>
public class AnalyticsService(
    IBaseRepository<Rent> rentRepository,
    IBaseRepository<Car> carRepository) : IAnalyticsService
{
    /// <summary>
    /// Finds unique clients who rented cars of a specific model name.
    /// </summary>
    public List<ClientDto> ReadClientsByModelName(string modelName)
    {
        return rentRepository.ReadAll()
            .Where(r => r.Car.ModelGeneration!.Model!.Name.Contains(modelName, StringComparison.OrdinalIgnoreCase))
            .Select(r => r.Client.Adapt<ClientDto>())
            .DistinctBy(c => c.Id)
            .ToList();
    }

    /// <summary>
    /// Identifies the top 5 most frequently rented cars.
    /// </summary>
    public List<CarWithRentalCountDto> ReadTop5MostRentedCars()
    {
        return rentRepository.ReadAll()
            .GroupBy(r => r.Car.Id)
            .Select(g => new CarWithRentalCountDto(
                g.First().Car.Id,
                g.First().Car.ModelGeneration?.Model!.Name ?? "Unknown",
                g.First().Car.NumberPlate,
                g.Count()
            ))
            .OrderByDescending(x => x.RentalCount)
            .Take(5)
            .ToList();
    }

    /// <summary>
    /// Retrieves cars that were actively rented at a specific point in time.
    /// </summary>
    public List<CarInRentDto> ReadCarsInRent(DateTime atTime)
    {
        return rentRepository.ReadAll()
            .Where(r => r.StartDateTime <= atTime && r.StartDateTime.AddHours(r.Duration) >= atTime)
            .Select(r => new CarInRentDto(
                r.Car.Id,
                r.Car.ModelGeneration?.Model!.Name ?? "Unknown",
                r.Car.NumberPlate,
                r.StartDateTime,
                (int)r.Duration
            ))
            .ToList();
    }

    /// <summary>
    /// Lists all cars and their total rental frequency.
    /// </summary>
    public List<CarWithRentalCountDto> ReadAllCarsWithRentalCount()
    {
        var allRents = rentRepository.ReadAll();

        return carRepository.ReadAll()
            .Select(car => new CarWithRentalCountDto(
                car.Id,
                car.ModelGeneration?.Model!.Name ?? "Unknown",
                car.NumberPlate,
                allRents.Count(r => r.Car.Id == car.Id)
            ))
            .ToList();
    }

    /// <summary>
    /// Identifies the top 5 clients by total revenue generated.
    /// </summary>
    public List<ClientWithTotalAmountDto> ReadTop5ClientsByTotalAmount()
    {
        return rentRepository.ReadAll()
            .GroupBy(r => r.Client.Id)
            .Select(g => {
                var client = g.First().Client;
                var totalAmount = g.Sum(r => (decimal)r.Duration * (r.Car.ModelGeneration?.HourCost ?? 0));
                return new ClientWithTotalAmountDto(
                    client.Id,
                    $"{client.LastName} {client.FirstName}",
                    totalAmount,
                    g.Count()
                );
            })
            .OrderByDescending(x => x.TotalSpentAmount)
            .Take(5)
            .ToList();
    }
}