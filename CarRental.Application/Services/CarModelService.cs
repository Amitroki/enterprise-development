using AutoMapper;
using CarRental.Application.Contracts.CarModel;
using CarRental.Application.Interfaces;
using CarRental.Domain.Interfaces;
using CarRental.Domain.InternalData.ComponentClasses;

namespace CarRental.Application.Services;

public class CarModelService(IBaseRepository<CarModel> repository, IMapper mapper)
    : IApplicationService<CarModelDto, CarModelCreateUpdateDto>
{
    public async Task<CarModelDto> Create(CarModelCreateUpdateDto dto)
    {
        var entity = mapper.Map<CarModel>(dto);
        var id = await repository.Create(entity);
        entity.Id = id;
        return mapper.Map<CarModelDto>(entity);
    }

    public async Task<CarModelDto?> Read(int id)
    {
        var entity = await repository.Read(id);
        return entity == null ? null : mapper.Map<CarModelDto>(entity);
    }

    public async Task<List<CarModelDto>> ReadAll()
    {
        var entities = await repository.ReadAll();
        return mapper.Map<List<CarModelDto>>(entities);
    }

    public async Task<bool> Update(CarModelCreateUpdateDto dto, int id)
    {
        var existing = await repository.Read(id);
        if (existing == null) return false;

        mapper.Map(dto, existing); // Обновляем существующий объект
        return await repository.Update(existing, id);
    }

    public async Task<bool> Delete(int id) => await repository.Delete(id);
}