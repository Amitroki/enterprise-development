using AutoMapper;
using CarRental.Application.Contracts.CarModelGeneration;
using CarRental.Application.Interfaces;
using CarRental.Domain.Interfaces;
using CarRental.Domain.InternalData.ComponentClasses;

namespace CarRental.Application.Services;

public class CarModelGenerationService(
    IBaseRepository<CarModelGeneration, Guid> repository,
    IBaseRepository<CarModel, Guid> modelRepository,
    IMapper mapper)
    : IApplicationService<CarModelGenerationDto, CarModelGenerationCreateUpdateDto, Guid>
{
    public async Task<CarModelGenerationDto> Create(CarModelGenerationCreateUpdateDto dto)
    {
        var entity = mapper.Map<CarModelGeneration>(dto);
        var model = await modelRepository.Read(dto.ModelId);
        if (model is null)
            throw new KeyNotFoundException($"CarModel with Id {dto.ModelId} not found.");
        entity.Model = model;
        entity.ModelId = model.Id;
        var id = await repository.Create(entity);
        entity.Id = id;
        return mapper.Map<CarModelGenerationDto>(entity);
    }

    public async Task<CarModelGenerationDto> Read(Guid id)
    {
        var entity = await repository.Read(id)
            ?? throw new KeyNotFoundException($"CarModelGeneration with Id {id} not found.");
        return mapper.Map<CarModelGenerationDto>(entity);
    }

    public async Task<List<CarModelGenerationDto>> ReadAll()
    {
        var entities = await repository.ReadAll();
        foreach (var generation in entities)
        {
            if (generation.ModelId != Guid.Empty)
            {
                generation.Model = await modelRepository.Read(generation.ModelId);
            }
        }
        return mapper.Map<List<CarModelGenerationDto>>(entities);
    }

    public async Task<bool> Update(CarModelGenerationCreateUpdateDto dto, Guid id)
    {
        var existing = await repository.Read(id);
        if (existing is null)
            return false;
        mapper.Map(dto, existing);
        var model = await modelRepository.Read(dto.ModelId);
        if (model is null)
            throw new KeyNotFoundException($"CarModel with Id {dto.ModelId} not found.");
        existing.Model = model;
        existing.ModelId = model.Id;
        return await repository.Update(existing, id);
    }

    public async Task<bool> Delete(Guid id)
        => await repository.Delete(id);
}
