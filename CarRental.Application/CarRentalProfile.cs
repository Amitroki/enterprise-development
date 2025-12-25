using CarRental.Domain;
using CarRental.Application.Contracts.Car;
using CarRental.Application.Contracts.Client;
using CarRental.Application.Contracts.Rent;
using CarRental.Application.Contracts.CarModel;
using CarRental.Application.Contracts.CarModelGeneration;
using CarRental.Domain.DataModels;
using CarRental.Domain.InternalData.ComponentClasses;
using AutoMapper;

namespace CarRental.Application;

public class CarRentalProfile : Profile
{
    public CarRentalProfile()
    {
        // --- 1. Client Mapping ---
        CreateMap<Client, ClientDto>().ReverseMap();
        CreateMap<ClientCreateUpdateDto, Client>();

        // --- 2. CarModel Mapping (Enum to String) ---
        CreateMap<CarModel, CarModelDto>()
            .ForMember(dest => dest.BodyType, opt => opt.MapFrom(src => src.BodyType.ToString()))
            .ForMember(dest => dest.DriveType, opt => opt.MapFrom(src => src.DriveType.HasValue ? src.DriveType.Value.ToString() : null))
            .ForMember(dest => dest.ClassType, opt => opt.MapFrom(src => src.ClassType.HasValue ? src.ClassType.Value.ToString() : null));

        CreateMap<CarModelCreateUpdateDto, CarModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        // --- 3. CarModelGeneration Mapping ---
        CreateMap<CarModelGeneration, CarModelGenerationDto>()
            .ForMember(dest => dest.ModelId, opt => opt.MapFrom(src => src.Model != null ? src.Model.Id : 0))
            .ForMember(dest => dest.TransmissionType, opt => opt.MapFrom(src => src.TransmissionType.HasValue ? src.TransmissionType.Value.ToString() : null));

        CreateMap<CarModelGenerationCreateUpdateDto, CarModelGeneration>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Model, opt => opt.Ignore());

        // --- 4. Car Mapping (Flattening) ---
        CreateMap<Car, CarDto>()
            .ForMember(dest => dest.GenerationId, opt => opt.MapFrom(src => src.ModelGeneration != null ? src.ModelGeneration.Id : 0))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.ModelGeneration != null ? src.ModelGeneration.Year : 0))
            .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src =>
                (src.ModelGeneration != null && src.ModelGeneration.Model != null)
                ? src.ModelGeneration.Model.Name
                : "Unknown Model"));

        CreateMap<CarCreateUpdateDto, Car>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ModelGeneration, opt => opt.Ignore());

        // --- 5. Rent Mapping (Complex Logic) ---
        CreateMap<Rent, RentDto>()
            .ForMember(dest => dest.CarId, opt => opt.MapFrom(src => src.Car != null ? src.Car.Id : 0))
            .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.Client != null ? src.Client.Id : 0))
            .ForMember(dest => dest.ClientLastName, opt => opt.MapFrom(src =>
                src.Client != null ? src.Client.LastName : "Deleted"))
            .ForMember(dest => dest.CarLicensePlate, opt => opt.MapFrom(src =>
                src.Car != null ? src.Car.NumberPlate : "Deleted"))
            .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src =>
                (src.Car != null && src.Car.ModelGeneration != null)
                ? (decimal)src.Duration * src.Car.ModelGeneration.HourCost
                : 0));

        CreateMap<RentCreateUpdateDto, Rent>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Car, opt => opt.Ignore())
            .ForMember(dest => dest.Client, opt => opt.Ignore());
    }
}