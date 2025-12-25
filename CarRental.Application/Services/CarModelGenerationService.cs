using AutoMapper;
using CarRental.Application.Contracts.CarModelGeneration;
using CarRental.Application.Interfaces;
using CarRental.Domain.Interfaces;
using CarRental.Domain.InternalData.ComponentClasses;

namespace CarRental.Application.Services;

public class CarModelGenerationService(
    IBaseRepository<CarModelGeneration> repository,
    IBaseRepository<CarModel> modelRepository,
    IMapper mapper)
    : IApplicationService<CarModelGenerationDto, CarModelGenerationCreateUpdateDto>
{
    public async Task<CarModelGenerationDto> Create(CarModelGenerationCreateUpdateDto dto)
    {
        var entity = mapper.Map<CarModelGeneration>(dto);
        var model = await modelRepository.Read(dto.ModelId);
        entity.Model = model;

        var id = await repository.Create(entity);
        entity.Id = id;

        return mapper.Map<CarModelGenerationDto>(entity);
    }

    public async Task<CarModelGenerationDto?> Read(int id)
    {
        var entity = await repository.Read(id);
        return entity == null ? null : mapper.Map<CarModelGenerationDto>(entity);
    }

    public async Task<List<CarModelGenerationDto>> ReadAll()
    {
        var entities = await repository.ReadAll();
        return mapper.Map<List<CarModelGenerationDto>>(entities);
    }

    public async Task<bool> Update(CarModelGenerationCreateUpdateDto dto, int id)
    {
        var existing = await repository.Read(id);
        if (existing == null) return false;

        mapper.Map(dto, existing);
        var model = await modelRepository.Read(dto.ModelId);
        existing.Model = model;

        return await repository.Update(existing, id);
    }

    public async Task<bool> Delete(int id) => await repository.Delete(id);
}