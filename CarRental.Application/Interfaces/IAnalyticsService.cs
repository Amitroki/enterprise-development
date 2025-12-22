using CarRental.Application.Contracts.Car;
using CarRental.Application.Contracts.Client;
using CarRental.Application.Contracts;

namespace CarRental.Application.Interfaces;

public interface IAnalyticsService
{
    public List<ClientDto> ReadClientsByModelName(string modelName);

    public List<CarInRentDto> ReadCarsInRent(DateTime atTime);

    public List<CarWithRentalCountDto> ReadTop5MostRentedCars();

    public List<CarWithRentalCountDto> ReadAllCarsWithRentalCount();

    public List<ClientWithTotalAmountDto> ReadTop5ClientsByTotalAmount();
}