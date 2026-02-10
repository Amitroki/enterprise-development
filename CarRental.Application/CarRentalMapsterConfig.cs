using Mapster;
using CarRental.Application.Contracts.Car;
using CarRental.Application.Contracts.CarModel;
using CarRental.Application.Contracts.CarModelGeneration;
using CarRental.Application.Contracts.Client;
using CarRental.Application.Contracts.Rent;
using CarRental.Domain.DataModels;
using CarRental.Domain.InternalData.ComponentClasses;

namespace CarRental.Application;

/// <summary>
/// AutoMapper configuration profile for mapping between Domain entities and Application DTOs
/// </summary>
public class CarRentalMapsterConfig : IRegister
{
    /// <summary>
    /// Registers mapping rules for converting between domain entities and DTOs 
    /// to enable seamless data projection and transfer using Mapster
    /// </summary>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Car, CarDto>();
        config.NewConfig<CarCreateUpdateDto, Car>();

        config.NewConfig<CarModel, CarModelDto>();
        config.NewConfig<CarModelCreateUpdateDto, CarModel>();

        config.NewConfig<CarModelGeneration, CarModelGenerationDto>();
        config.NewConfig<CarModelGenerationCreateUpdateDto, CarModelGeneration>();

        config.NewConfig<Client, ClientDto>();
        config.NewConfig<ClientCreateUpdateDto, Client>();

        config.NewConfig<Rent, RentDto>();
        config.NewConfig<RentCreateUpdateDto, Rent>();
    }
}