using Mapster;
using CarRental.Domain.DataModels;
using CarRental.Application.Contracts.Car;
using CarRental.Application.Contracts.Client;
using CarRental.Application.Contracts.Rent;

namespace CarRental.Application.Mapping;

/// <summary>
/// Provides global configuration for object-to-object mapping using Mapster.
/// Defines rules for converting domain entities into data transfer objects.
/// </summary>
public static class MappingConfig
{
    /// <summary>
    /// Initializes and registers mapping configurations between domain models and DTOs.
    /// </summary>
    public static void Configure()
    {
        TypeAdapterConfig.GlobalSettings.Default.PreserveReference(false);

        // Client mapping
        TypeAdapterConfig<Client, ClientDto>.NewConfig()
            .MapToConstructor(true);

        // Car mapping with flattened ModelName
        TypeAdapterConfig<Car, CarDto>.NewConfig()
            .MapToConstructor(true)
            .Map(dest => dest.ModelName, src => src.ModelGeneration!.Model!.Name);

        // Rent mapping with complex logic for associated entities and costs
        TypeAdapterConfig<Rent, RentDto>.NewConfig()
            .MapToConstructor(true)
            .Map(dest => dest.CarId, src => src.Car.Id)
            .Map(dest => dest.ClientId, src => src.Client.Id)
            .Map(dest => dest.ClientLastName, src => src.Client != null? src.Client.LastName: "Client is deleted")
            .Map(dest => dest.CarLicensePlate, src => src.Car != null? src.Car.NumberPlate: "Car is deleted")
            .Map(dest => dest.TotalCost, src => src.Car != null && src.Car.ModelGeneration != null? (decimal)src.Duration * src.Car.ModelGeneration.HourCost: 0);
    }
}