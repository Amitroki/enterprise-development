namespace CarRental.Application.Interfaces;

public interface IAnalyticsService
{
    List<ClientDto> ReadClientsByModelName(string modelName);

    List<CarDto> ReadCarsInRent(DateTime atTime);

    List<CarDto> ReadTop5MostRentedCars();

    List<CarWithRentalCountDto> ReadAllCarsWithRentalCount();

    List<ClientWithTotalAmountDto> ReadTop5ClientsByTotalAmount();
}