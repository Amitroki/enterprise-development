using CarRental.Domain.DataSeed;
using Xunit.Abstractions;

namespace CarRental.Tests;

/// <summary>
/// A class that contains unit tests for checking various scenarios of using the main classes
/// </summary>
public class DomainTests : IClassFixture<DataSeed>
{
    /// <summary>
    /// Shared test data fixture providing pre-initialized domain entities 
    /// (clients, cars, models, rentals, etc.) for all test methods in this class
    /// </summary>
    private readonly DataSeed _fixture;

    /// <summary>
    /// Helper for writing diagnostic output during test execution; 
    /// messages are visible in test logs (e.g., in Test Explorer or CI reports)
    /// </summary>
    private readonly ITestOutputHelper _output;

    /// <summary>
    /// Initializes a new instance of the test class with shared test data and output helper
    /// </summary>
    public DomainTests(DataSeed fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;
    }

    /// <summary>
    /// 1. Output of clients who rented vehicles of a specified model, 
    /// ordered by last name, first name, and patronymic
    /// </summary>
    [Fact]
    public void Should_Return_Clients_Sorted_By_FullName_For_Given_Car_Model()
    {
        var target = _fixture.Models[9]; // Volkswagen Transporter

        var targetClients = _fixture.Rents
            .Where(r => r.Car.ModelGeneration.Model.Name == target.Name)
            .Select(r => r.Client)
            .Distinct()
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ThenBy(c => c.Patronymic)
            .ToList();
        foreach (var client in targetClients)
        {
            _output.WriteLine($"{client.Id} {client.LastName} {client.FirstName} {client.Patronymic ?? ""} {client.BirthDate?.ToString() ?? ""}");
        }

        var correctId = new uint[] { 15, 5 };
        Assert.Equal(correctId, targetClients.Select(c => c.Id).ToArray());

    }

    /// <summary>
    /// 2. Output of vehicles currently in rental as of January 1, 2025, 10:00
    /// </summary>
    [Fact]
    public void CarsInRentAtBaseTime_AreListedCorrectly()
    {
        var now = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc);

        var carsInRent = _fixture.Rents
            .Where(r => r.StartDateTime <= now && now < r.StartDateTime.AddHours(r.Duration))
            .Select(r => r.Car)
            .Distinct()
            .OrderBy(c => c.Id)
            .ToList();

        foreach (var car in carsInRent)
        {
            _output.WriteLine($"{car.Id} {car.ModelGeneration.Model?.Name ?? ""} {car.NumberPlate} {car.Colour}");
        }

        var correctCount = 1;
        Assert.Equal(carsInRent.Count, correctCount);
    }

    /// <summary>
    /// 3. Output of the top 5 most frequently rented vehicles, 
    /// sorted in descending order by rental count
    /// </summary>
    [Fact]
    public void Top5MostRentedCars_AreReturnedInDescendingOrder()
    {
        var topCars = _fixture.Rents
            .GroupBy(r => r.Car)
            .Select(g => new { Car = g.Key, RentCount = g.Count() })
            .OrderByDescending(x => x.RentCount)
            .ThenBy(x => x.Car.Id)
            .Take(5)
            .ToList();

        foreach (var item in topCars)
        {
            _output.WriteLine($"{item.Car.Id} {item.Car.ModelGeneration.Model?.Name ?? ""} {item.RentCount}");
        }

        Assert.Equal(5, topCars.Count);

    }

    /// <summary>
    /// 4. Output of rental counts for every vehicle in the fleet, 
    /// including vehicles with zero rentals
    /// </summary>
    [Fact]
    public void AllCars_IncludeRentalCount_EvenIfZero()
    {
        foreach (var car in _fixture.Cars.OrderBy(c => c.Id))
        {
            _output.WriteLine(
                $"{car.Id} {car.ModelGeneration.Model?.Name ?? "Unknown"} {car.NumberPlate} " +
                $"{car.Colour} {_fixture.Rents.Count(r => r.Car.Id == car.Id)}"
            );
        }

        Assert.Equal(20, _fixture.Cars.Count);
    }

    /// <summary>
    /// 5. Output of the top 5 clients with the highest total rental amount, 
    /// calculated as the sum of (duration × hourly cost) for all their rentals
    /// </summary>
    [Fact]
    public void Top5ClientsByTotalRentalAmount_AreReturnedCorrectly()
    {
        var clientTotals = _fixture.Rents
            .GroupBy(r => r.Client)
            .Select(g => new
            {
                Client = g.Key,
                TotalAmount = g.Sum(r => r.Duration * r.Car.ModelGeneration.HourCost)
            })
            .OrderByDescending(x => x.TotalAmount)
            .ThenBy(x => x.Client.Id)
            .Take(5)
            .ToList();

        foreach (var item in clientTotals)
        {
            _output.WriteLine(
                $"{item.Client.LastName} {item.Client.FirstName} " +
                $"{item.Client.Id} {item.TotalAmount:F2}"
            );
        }

        Assert.True(clientTotals.Count == 5);
    }
}
