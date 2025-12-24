using CarRental.Application.Contracts.Client;
using CarRental.Application.Contracts.Analytics;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Defines methods for business intelligence and data analysis across cars, clients, and rentals.
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Retrieves all clients who have rented a specific car model.
    /// </summary>
    public Task<List<ClientDto>> ReadClientsByModelName(string modelName);

    /// <summary>
    /// Lists all cars that are currently occupied at a specific point in time.
    /// </summary>
    public Task<List<CarInRentDto>> ReadCarsInRent(DateTime atTime);

    /// <summary>
    /// Returns the top 5 cars with the highest number of rental agreements.
    /// </summary>
    public Task<List<CarWithRentalCountDto>> ReadTop5MostRentedCars();

    /// <summary>
    /// Returns a list of all cars along with their total rental frequency.
    /// </summary>
    public Task<List<CarWithRentalCountDto>> ReadAllCarsWithRentalCount();

    /// <summary>
    /// Returns the top 5 clients who have spent the most money on rentals.
    /// </summary>
    public Task<List<ClientWithTotalAmountDto>> ReadTop5ClientsByTotalAmount();
}