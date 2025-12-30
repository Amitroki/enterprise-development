using AutoMapper;
using CarRental.Application.Contracts.Car;
using CarRental.Application.Interfaces;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;
using CarRental.Domain.InternalData.ComponentClasses;

namespace CarRental.Application.Services;

public class CarService(
    IBaseRepository<Car, Guid> repository,
    IBaseRepository<CarModelGeneration, Guid> generationRepository,
    IMapper mapper)
    : IApplicationService<CarDto, CarCreateUpdateDto, Guid>
{
    public async Task<List<CarDto>> ReadAll()
    {
        var entities = await repository.ReadAll();
        return mapper.Map<List<CarDto>>(entities);
    }

    public async Task<CarDto> Read(Guid id)
    {
        var entity = await repository.Read(id)
            ?? throw new KeyNotFoundException($"Car with Id {id} not found.");

        return mapper.Map<CarDto>(entity);
    }

    public async Task<CarDto> Create(CarCreateUpdateDto dto)
    {
        // Проверяем существование поколения
        var generation = await generationRepository.Read(dto.ModelGenerationId);
        if (generation is null)
            throw new KeyNotFoundException($"ModelGeneration with Id {dto.ModelGenerationId} not found.");

        var entity = mapper.Map<Car>(dto);

        var id = await repository.Create(entity);

        // перечитываем для консистентности (in-memory — не обязательно, но безопасно)
        var savedEntity = await repository.Read(id)
            ?? throw new InvalidOperationException("Created car was not found.");

        return mapper.Map<CarDto>(savedEntity);
    }

    public async Task<bool> Update(CarCreateUpdateDto dto, Guid id)
    {
        var existing = await repository.Read(id);
        if (existing is null)
            return false;

        // Если меняется поколение — валидируем
        if (dto.ModelGenerationId != existing.ModelGenerationId)
        {
            var generation = await generationRepository.Read(dto.ModelGenerationId);
            if (generation is null)
                throw new KeyNotFoundException($"ModelGeneration with Id {dto.ModelGenerationId} not found.");
        }

        mapper.Map(dto, existing);

        return await repository.Update(existing, id);
    }

    public async Task<bool> Delete(Guid id)
        => await repository.Delete(id);
}
