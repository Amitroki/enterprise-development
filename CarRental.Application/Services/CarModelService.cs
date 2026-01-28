using AutoMapper;
using CarRental.Application.Contracts.CarModel;
using CarRental.Application.Interfaces;
using CarRental.Domain.Interfaces;
using CarRental.Domain.InternalData.ComponentClasses;

namespace CarRental.Application.Services;

public class CarModelService(
    IBaseRepository<CarModel, Guid> repository,
    IMapper mapper)
    : IApplicationService<CarModelDto, CarModelCreateUpdateDto, Guid>
{
    public async Task<CarModelDto> Create(CarModelCreateUpdateDto dto)
    {
        var entity = mapper.Map<CarModel>(dto);
        var id = await repository.Create(entity);
        entity.Id = id;
        return mapper.Map<CarModelDto>(entity);
    }

    public async Task<CarModelDto> Read(Guid id)
    {
        var entity = await repository.Read(id)
            ?? throw new KeyNotFoundException($"CarModel with Id {id} not found.");
        return mapper.Map<CarModelDto>(entity);
    }

    public async Task<List<CarModelDto>> ReadAll()
    {
        var entities = await repository.ReadAll();
        return mapper.Map<List<CarModelDto>>(entities);
    }

    public async Task<bool> Update(CarModelCreateUpdateDto dto, Guid id)
    {
        var existing = await repository.Read(id);
        if (existing is null)
            return false;
        mapper.Map(dto, existing);
        return await repository.Update(existing, id);
    }

    public async Task<bool> Delete(Guid id)
        => await repository.Delete(id);
}
