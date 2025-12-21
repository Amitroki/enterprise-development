using Mapster;
using CarRental.Application.Contracts;
using CarRental.Domain.DataModels;

namespace CarRental.Application.Mapping;

public static class MappingConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<Car, CarDto>.NewConfig()
            .Map(dest => dest.ModelName, src => src.ModelGeneration.Model.Name);

        TypeAdapterConfig<CarCreateUpdateDto, Car>.NewConfig()
            .Ignore(dest => dest.ModelGeneration);
    }
}