using Microsoft.EntityFrameworkCore;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure;

namespace CarRental.Infrastructure.Repository;

public class DbCarRepository(CarRentalDbContext context) : IBaseRepository<Car>
{
    public async Task<List<Car>> ReadAll() => await context.Cars.ToListAsync();

    public async Task<Car?> Read(int id) =>
        (await context.Cars.ToListAsync()).FirstOrDefault(x => x.Id == id);

    public async Task<int> Create(Car entity)
    {
        await context.Cars.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> Update(Car entity, int id)
    {
        context.Cars.Update(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await Read(id);
        if (entity is null) return false;
        context.Cars.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }
}