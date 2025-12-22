using Mapster;
using CarRental.Domain.DataModels;
using CarRental.Domain.InternalData.ComponentClasses;
using CarRental.Application.Contracts.Car;
using CarRental.Application.Contracts.Client;
using CarRental.Application.Contracts.Rent;
using CarRental.Application.Contracts.CarModel;
using CarRental.Application.Contracts.CarModelGeneration;

namespace CarRental.Application.Mapping;

public static class MappingConfig
{
    public static void Configure()
    {
        TypeAdapterConfig.GlobalSettings.Default.PreserveReference(false);

        TypeAdapterConfig<Client, ClientDto>.NewConfig()
            .MapToConstructor(true);

        TypeAdapterConfig<Car, CarDto>.NewConfig()

            .MapToConstructor(true)
            .Map(dest => dest.ModelName, src => src.ModelGeneration!.Model!.Name);

        TypeAdapterConfig<Rent, RentDto>.NewConfig()
            .MapToConstructor(true)
            .Map(dest => dest.CarId, src => src.Car.Id)
            .Map(dest => dest.ClientId, src => src.Client.Id)
            .Map(dest => dest.ClientLastName, src => src.Client != null? src.Client.LastName: "Client is deleted")
            .Map(dest => dest.CarLicensePlate, src => src.Car != null? src.Car.NumberPlate: "Car is deleted")
            .Map(dest => dest.TotalCost, src => src.Car != null && src.Car.ModelGeneration != null? (decimal)src.Duration * src.Car.ModelGeneration.HourCost: 0);
    }
}