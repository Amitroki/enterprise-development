using AutoMapper;
using CarRental.Application.Contracts.Analytics;
using CarRental.Application.Contracts.Client;
using CarRental.Application.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

public class AnalyticsService(
    IBaseRepository<Rent, Guid> rentRepository,
    IBaseRepository<Car, Guid> carRepository,
    IMapper mapper)
    : IAnalyticsService
{
    public async Task<List<ClientDto>> ReadClientsByModelName(string modelName)
    {
        var rents = await rentRepository.ReadAll();

        return rents
            .Where(r => r.Client != null &&
                        r.Car?.ModelGeneration?.Model?.Name != null &&
                        r.Car.ModelGeneration.Model.Name.Contains(
                            modelName,
                            StringComparison.OrdinalIgnoreCase))
            .Select(r => mapper.Map<ClientDto>(r.Client))
            .DistinctBy(c => c.Id)
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ToList();
    }

    public async Task<List<CarWithRentalCountDto>> ReadTop5MostRentedCars()
    {
        var rents = await rentRepository.ReadAll();

        return rents
            .Where(r => r.Car != null)
            .GroupBy(r => r.Car.Id)
            .Select(g =>
            {
                var car = g.First().Car!;
                return new CarWithRentalCountDto(
                    car.Id,
                    car.ModelGeneration?.Model?.Name ?? "Unknown",
                    car.NumberPlate,
                    g.Count()
                );
            })
            .OrderByDescending(x => x.RentalCount)
            .ThenBy(x => x.NumberPlate) // детерминированно вместо Guid
            .Take(5)
            .ToList();
    }

    public async Task<List<CarInRentDto>> ReadCarsInRent(DateTime atTime)
    {
        var rents = await rentRepository.ReadAll();

        return rents
            .Where(r => r.Car != null &&
                        r.StartDateTime <= atTime &&
                        atTime < r.StartDateTime.AddHours(r.Duration))
            .Select(r =>
            {
                var car = r.Car!;
                return new CarInRentDto(
                    car.Id,
                    car.ModelGeneration?.Model?.Name ?? "Unknown",
                    car.NumberPlate,
                    r.StartDateTime,
                    (int)r.Duration
                );
            })
            .OrderBy(x => x.NumberPlate)
            .ToList();
    }

    public async Task<List<CarWithRentalCountDto>> ReadAllCarsWithRentalCount()
    {
        var allRents = await rentRepository.ReadAll();
        var allCars = await carRepository.ReadAll();

        return allCars
            .Select(car => new CarWithRentalCountDto(
                car.Id,
                car.ModelGeneration?.Model?.Name ?? "Unknown",
                car.NumberPlate,
                allRents.Count(r => r.Car?.Id == car.Id)
            ))
            .OrderBy(x => x.NumberPlate)
            .ToList();
    }

    public async Task<List<ClientWithTotalAmountDto>> ReadTop5ClientsByTotalAmount()
    {
        var rents = await rentRepository.ReadAll();

        return rents
            .Where(r => r.Client != null && r.Car?.ModelGeneration != null)
            .GroupBy(r => r.Client!.Id)
            .Select(g =>
            {
                var client = g.First().Client!;
                var totalAmount = g.Sum(r =>
                    (decimal)r.Duration * r.Car!.ModelGeneration!.HourCost);

                return new ClientWithTotalAmountDto(
                    client.Id,
                    $"{client.LastName} {client.FirstName}",
                    totalAmount,
                    g.Count()
                );
            })
            .OrderByDescending(x => x.TotalSpentAmount)
            .ThenBy(x => x.FullName)
            .Take(5)
            .ToList();
    }
}
