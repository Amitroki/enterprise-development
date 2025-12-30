using AutoMapper;
using CarRental.Application.Contracts.Car;
using CarRental.Application.Contracts.CarModel;
using CarRental.Application.Contracts.CarModelGeneration;
using CarRental.Application.Contracts.Client;
using CarRental.Application.Contracts.Rent;
using CarRental.Domain.DataModels;
using CarRental.Domain.InternalData.ComponentClasses;
using DriveTypeEnum = CarRental.Domain.InternalData.ComponentEnums.DriveType;
using ClassTypeEnum = CarRental.Domain.InternalData.ComponentEnums.ClassType;
using BodyTypeEnum = CarRental.Domain.InternalData.ComponentEnums.BodyType;
using TransmissionTypeEnum = CarRental.Domain.InternalData.ComponentEnums.TransmissionType;

namespace CarRental.Application;

public class CarRentalProfile : Profile
{
    public CarRentalProfile()
    {
        // =====================
        // Client
        // =====================
        CreateMap<Client, ClientDto>();
        CreateMap<ClientCreateUpdateDto, Client>();

        // =====================
        // CarModel
        // =====================
        CreateMap<CarModel, CarModelDto>();
        CreateMap<CarModelCreateUpdateDto, CarModel>();

        // =====================
        // CarModelGeneration
        // =====================
        CreateMap<CarModelGeneration, CarModelGenerationDto>();
        CreateMap<CarModelGenerationCreateUpdateDto, CarModelGeneration>();

        // =====================
        // Car
        // =====================
        CreateMap<Car, CarDto>();
        CreateMap<CarCreateUpdateDto, Car>();

        // =====================
        // Rent
        // =====================
        CreateMap<Rent, RentDto>();
        CreateMap<RentCreateUpdateDto, Rent>();
    }
}
