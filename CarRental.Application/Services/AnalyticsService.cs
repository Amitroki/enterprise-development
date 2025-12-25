using AutoMapper;
using CarRental.Application.Contracts.Analytics;
using CarRental.Application.Contracts.Client;
using CarRental.Application.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

public class AnalyticsService(
    IBaseRepository<Rent> rentRepository,
    IBaseRepository<Car> carRepository,
    IMapper mapper) : IAnalyticsService
{
    public async Task<List<ClientDto>> ReadClientsByModelName(string modelName)
    {
        var rents = await rentRepository.ReadAll();

        return rents
            .Where(r => r.Car?.ModelGeneration?.Model?.Name != null &&
                        r.Car.ModelGeneration.Model.Name.Contains(modelName, StringComparison.OrdinalIgnoreCase))
            .Select(r => mapper.Map<ClientDto>(r.Client))
            .DistinctBy(c => c.Id)
            .ToList();
    }

    public async Task<List<CarWithRentalCountDto>> ReadTop5MostRentedCars()
    {
        var rents = await rentRepository.ReadAll();

        return rents
            .Where(r => r.Car != null)
            .GroupBy(r => r.Car.Id)
            .Select(g => {
                var firstRent = g.First();
                return new CarWithRentalCountDto(
                    firstRent.Car.Id,
                    firstRent.Car.ModelGeneration?.Model?.Name ?? "Unknown",
                    firstRent.Car.NumberPlate,
                    g.Count()
                );
            })
            .OrderByDescending(x => x.RentalCount)
            .Take(5)
            .ToList();
    }

    public async Task<List<CarInRentDto>> ReadCarsInRent(DateTime atTime)
    {
        var rents = await rentRepository.ReadAll();

        return rents
            .Where(r => r.Car != null &&
                        r.StartDateTime <= atTime &&
                        r.StartDateTime.AddHours(r.Duration) >= atTime)
            .Select(r => new CarInRentDto(
                r.Car.Id,
                r.Car.ModelGeneration?.Model?.Name ?? "Unknown",
                r.Car.NumberPlate,
                r.StartDateTime,
                (int)r.Duration
            ))
            .ToList();
    }

    public async Task<List<CarWithRentalCountDto>> ReadAllCarsWithRentalCount()
    {
        var allRents = await rentRepository.ReadAll();
        var allCars = await carRepository.ReadAll();

        return allCars.Select(car => new CarWithRentalCountDto(
                car.Id,
                car.ModelGeneration?.Model?.Name ?? "Unknown",
                car.NumberPlate,
                allRents.Count(r => r.Car?.Id == car.Id)
            )).ToList();
    }

    public async Task<List<ClientWithTotalAmountDto>> ReadTop5ClientsByTotalAmount()
    {
        var rents = await rentRepository.ReadAll();

        return rents
            .Where(r => r.Client != null && r.Car?.ModelGeneration != null)
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