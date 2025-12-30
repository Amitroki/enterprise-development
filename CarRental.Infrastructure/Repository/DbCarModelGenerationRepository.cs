using Microsoft.EntityFrameworkCore;
using CarRental.Domain.InternalData.ComponentClasses;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure;

namespace CarRental.Infrastructure.Repository;

public class DbCarModelGenerationRepository(CarRentalDbContext context) : IBaseRepository<CarModelGeneration, Guid>
{
    public async Task<List<CarModelGeneration>> ReadAll() =>
        (await context.ModelGenerations.ToListAsync())
        .Select(g =>
        {
            g.Model = context.CarModels.FirstOrDefault(m => m.Id == g.ModelId);
            return g;
        }).ToList();

    public async Task<CarModelGeneration?> Read(Guid id)
    {
        var list = await context.ModelGenerations.ToListAsync();
        var entity = list.FirstOrDefault(x => x.Id == id);
        if (entity != null)
        {
            entity.Model = context.CarModels.FirstOrDefault(m => m.Id == entity.ModelId);
        }
        return entity;
    }

    public async Task<Guid> Create(CarModelGeneration entity)
    {
        await context.ModelGenerations.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> Update(CarModelGeneration entity, Guid id)
    {
        context.ModelGenerations.Update(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await Read(id);
        if (entity == null) return false;
        context.ModelGenerations.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }
}
