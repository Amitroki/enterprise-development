using CarRental.Application.Contracts.Car;

namespace CarRental.Application.Interfaces;

public interface IAnalyticsService
{
    //public List<ClientDto> ReadClientsByModelName(string modelName);

    public List<CarDto> ReadCarsInRent(DateTime atTime);

    public List<CarDto> ReadTop5MostRentedCars();

    //public List<CarWithRentalCountDto> ReadAllCarsWithRentalCount();

    //public List<ClientWithTotalAmountDto> ReadTop5ClientsByTotalAmount();
}