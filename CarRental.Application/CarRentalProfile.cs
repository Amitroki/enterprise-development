using AutoMapper;
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
public class CarRentalProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CarRentalProfile"/> class and defines mapping rules
    /// </summary>
    public CarRentalProfile()
    {
        CreateMap<Car, CarDto>();
        CreateMap<CarCreateUpdateDto, Car>();

        CreateMap<CarModel, CarModelDto>();
        CreateMap<CarModelCreateUpdateDto, CarModel>();

        CreateMap<CarModelGeneration, CarModelGenerationDto>();
        CreateMap<CarModelGenerationCreateUpdateDto, CarModelGeneration>();

        CreateMap<Client, ClientDto>();
        CreateMap<ClientCreateUpdateDto, Client>();

        CreateMap<Rent, RentDto>();
        CreateMap<RentCreateUpdateDto, Rent>();
    }
}