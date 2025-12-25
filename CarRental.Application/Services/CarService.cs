using AutoMapper;
using CarRental.Application.Contracts.Car;
using CarRental.Application.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;
using CarRental.Domain.InternalData.ComponentClasses;

namespace CarRental.Application.Services;

public class CarService(
    IBaseRepository<Car> repository,
    IBaseRepository<CarModelGeneration> generationRepository,
    IMapper mapper)
    : IApplicationService<CarDto, CarCreateUpdateDto>
{
    public async Task<List<CarDto>> ReadAll()
    {
        var entities = await repository.ReadAll();
        return mapper.Map<List<CarDto>>(entities);
    }

    public async Task<CarDto?> Read(int id)
    {
        var entity = await repository.Read(id);
        return entity == null ? null : mapper.Map<CarDto>(entity);
    }

    public async Task<CarDto> Create(CarCreateUpdateDto dto)
    {
        var entity = mapper.Map<Car>(dto);
        var fullGeneration = await generationRepository.Read(dto.ModelGenerationId);

        if (fullGeneration == null)
            throw new Exception("Generation not found");

        entity.ModelGeneration = fullGeneration;
        var id = await repository.Create(entity);
        var savedEntity = await repository.Read(id);
        return mapper.Map<CarDto>(savedEntity!);
    }

    public async Task<bool> Update(CarCreateUpdateDto dto, int id)
    {
        var existing = await repository.Read(id);
        if (existing is null) return false;

        mapper.Map(dto, existing);

        var fullGeneration = await generationRepository.Read(dto.ModelGenerationId);
        if (fullGeneration != null)
        {
            existing.ModelGeneration = fullGeneration;
        }

        return await repository.Update(existing, id);
    }

    public async Task<bool> Delete(int id) => await repository.Delete(id);
}