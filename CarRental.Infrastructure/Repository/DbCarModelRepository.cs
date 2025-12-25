using Microsoft.EntityFrameworkCore;
using CarRental.Domain.InternalData.ComponentClasses;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure;

namespace CarRental.Infrastructure.Repository;

public class DbCarModelRepository(CarRentalDbContext context) : IBaseRepository<CarModel>
{
    public async Task<List<CarModel>> ReadAll() => await context.CarModels.ToListAsync();

    public async Task<CarModel?> Read(int id) =>
        (await context.CarModels.ToListAsync()).FirstOrDefault(x => x.Id == id);

    public async Task<int> Create(CarModel entity)
    {
        await context.CarModels.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> Update(CarModel entity, int id)
    {
        context.CarModels.Update(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await Read(id);
        if (entity is null) return false;
        context.CarModels.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }
}