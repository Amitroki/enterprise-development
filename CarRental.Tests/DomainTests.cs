using CarRental.Domain.DataSeed;
using Xunit.Abstractions;

namespace CarRental.Tests;

/// <summary>
/// Unit tests for rental domain analytics, initialized with shared test data and output helper.
/// The primary constructor accepts:
/// - <paramref name="fixture"/>: Pre-filled domain entities (clients, cars, models, rentals).
/// - <paramref name="output"/>: xUnit helper for diagnostic logging in test results.
/// </summary>
public class DomainTests(
    DataSeed fixture,
    ITestOutputHelper output) : IClassFixture<DataSeed>
{
    /// <summary>
    /// 1. Output of clients who rented vehicles of a specified model, 
    /// ordered by last name, first name, and patronymic
    /// </summary>
    [Fact]
    public void GetClientsByModelName_WhenModelHasRentals_ReturnsClientsSortedByFullName()
    {
        var target = fixture.Models[9]; // Volkswagen Transporter

        var targetClients = fixture.Rents
            .Where(r => r.Car?.ModelGeneration.Model?.Name == target.Name)
            .Select(r => r.Client)
            .Distinct()
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ThenBy(c => c.Patronymic ?? string.Empty)
            .ToList();
        foreach (var client in targetClients)
        {
            output.WriteLine($"{client.Id} {client.LastName} {client.FirstName} {client.Patronymic ?? ""} {client.BirthDate?.ToString() ?? ""}");
        }

        var sorted = targetClients
        .OrderBy(c => c.LastName)
        .ThenBy(c => c.FirstName)
        .ThenBy(c => c.Patronymic ?? string.Empty)
        .Select(c => c.Id)
        .ToArray();

        Assert.Equal(sorted, targetClients.Select(c => c.Id).ToArray());

    }

    /// <summary>
    /// 2. Output of vehicles currently in rental as of January 1, 2025, 10:00
    /// </summary>
    [Fact]
    public void GetCarsInRent_WhenCheckedAtBaseTime_ReturnsActiveRentalCars()
    {
        var now = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc);

        var carsInRent = fixture.Rents
            .Where(r => r.StartDateTime <= now &&
                        now < r.StartDateTime.AddHours(r.Duration))
            .Select(r => r.Car)
            .Distinct()
            .OrderBy(c => c.NumberPlate)
            .ToList();

        foreach (var car in carsInRent)
        {
            output.WriteLine($"{car.Id} {car.ModelGeneration.Model?.Name ?? ""} {car.NumberPlate} {car.Colour}");
        }

        Assert.Single(carsInRent);
    }

    /// <summary>
    /// 3. Output of the top 5 most frequently rented vehicles, 
    /// sorted in descending order by rental count
    /// </summary>
    [Fact]
    public void GetTopRentedCars_WhenAllRentalsExist_ReturnsTop5CarsOrderedByRentalCountDescending()
    {
        var topCars = fixture.Rents
            .GroupBy(r => r.Car)
            .Select(g => new { Car = g.Key, RentCount = g.Count() })
            .OrderByDescending(x => x.RentCount)
            .ThenBy(x => x.Car.NumberPlate)
            .Take(5)
            .ToList();

        foreach (var item in topCars)
        {
            output.WriteLine($"{item.Car.Id} {item.Car.ModelGeneration.Model?.Name ?? ""} {item.RentCount}");
        }

        Assert.Equal(5, topCars.Count);

        Assert.True(
            topCars.SequenceEqual(
                topCars.OrderByDescending(x => x.RentCount)
                       .ThenBy(x => x.Car.NumberPlate)
            )
        );
    }

    /// <summary>
    /// 4. Output of rental counts for every vehicle in the fleet, 
    /// including vehicles with zero rentals
    /// </summary>
    [Fact]
    public void GetAllCars_WhenFleetIsInitialized_ReturnsAllCarsWithRentalCountIncludingZero()
    {
        var cars = fixture.Cars
            .OrderBy(c => c.NumberPlate)
            .ToList();

        foreach (var car in cars)
        {
            var rentCount = fixture.Rents.Count(r => r.Car.Id == car.Id);

            output.WriteLine(
                $"{car.Id} {car.ModelGeneration.Model?.Name ?? "Unknown"} " +
                $"{car.NumberPlate} {car.Colour} {rentCount}"
            );
        }

        Assert.Equal(20, cars.Count);
    }

    /// <summary>
    /// 5. Output of the top 5 clients with the highest total rental amount, 
    /// calculated as the sum of (duration × hourly cost) for all their rentals
    /// </summary>
    [Fact]
    public void GetTopClientsByTotalRentalAmount_WhenRentalsHaveDurationAndCost_ReturnsTop5ClientsOrderedByAmountDescending()
    {
        var clientTotals = fixture.Rents
            .GroupBy(r => r.Client)
            .Select(g => new
            {
                Client = g.Key,
                TotalAmount = g.Sum(r =>
                    Convert.ToDecimal(r.Duration) * r.Car.ModelGeneration.HourCost)
            })
            .OrderByDescending(x => x.TotalAmount)
            .ThenBy(x => x.Client.LastName)
            .ThenBy(x => x.Client.FirstName)
            .Take(5)
            .ToList();

        foreach (var item in clientTotals)
        {
            output.WriteLine(
                $"{item.Client.LastName} {item.Client.FirstName} " +
                $"{item.Client.Id} {item.TotalAmount:F2}"
            );
        }

        Assert.Equal(5, clientTotals.Count);

        Assert.True(
            clientTotals.SequenceEqual(
                clientTotals.OrderByDescending(x => x.TotalAmount)
                            .ThenBy(x => x.Client.LastName)
                            .ThenBy(x => x.Client.FirstName)
            )
        );
    }
}
