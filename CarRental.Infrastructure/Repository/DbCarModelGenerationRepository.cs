using Microsoft.EntityFrameworkCore;
using CarRental.Domain.InternalData.ComponentClasses;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure;

namespace CarRental.Infrastructure.Repository;

public class DbCarModelGenerationRepository(CarRentalDbContext context) : IBaseRepository<CarModelGeneration>
{
    public async Task<List<CarModelGeneration>> ReadAll() =>
        await context.ModelGenerations.Include(g => g.Model).ToListAsync();

    public async Task<CarModelGeneration?> Read(int id) =>
        (await context.ModelGenerations.Include(g => g.Model).ToListAsync())
        .FirstOrDefault(x => x.Id == id);

    public async Task<int> Create(CarModelGeneration entity)
    {
        await context.ModelGenerations.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> Update(CarModelGeneration entity, int id)
    {
        context.ModelGenerations.Update(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await Read(id);
        if (entity is null) return false;
        context.ModelGenerations.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }
}