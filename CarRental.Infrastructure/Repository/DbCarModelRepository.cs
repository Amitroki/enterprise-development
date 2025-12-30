using Microsoft.EntityFrameworkCore;
using CarRental.Domain.InternalData.ComponentClasses;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure;

namespace CarRental.Infrastructure.Repository;

public class DbCarModelRepository(CarRentalDbContext context) : IBaseRepository<CarModel, Guid>
{
    public async Task<List<CarModel>> ReadAll() => await context.CarModels.ToListAsync();

    public async Task<CarModel?> Read(Guid id) =>
        (await context.CarModels.ToListAsync()).FirstOrDefault(x => x.Id == id);

    public async Task<Guid> Create(CarModel entity)
    {
        await context.CarModels.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> Update(CarModel entity, Guid id)
    {
        context.CarModels.Update(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await Read(id);
        if (entity is null) return false;
        context.CarModels.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }
}