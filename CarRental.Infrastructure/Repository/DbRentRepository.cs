using Microsoft.EntityFrameworkCore;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure;

namespace CarRental.Infrastructure.Repository;

public class DbRentRepository(CarRentalDbContext context) : IBaseRepository<Rent, Guid>
{
    public async Task<List<Rent>> ReadAll() =>
        (await context.Rents.ToListAsync())
        .Select(r =>
        {
            r.Car = context.Cars.FirstOrDefault(c => c.Id == r.CarId);
            r.Client = context.Clients.FirstOrDefault(c => c.Id == r.ClientId);
            return r;
        }).ToList();

    public async Task<Rent?> Read(Guid id)
    {
        var list = await context.Rents.ToListAsync();
        var entity = list.FirstOrDefault(r => r.Id == id);
        if (entity != null)
        {
            entity.Car = context.Cars.FirstOrDefault(c => c.Id == entity.CarId);
            entity.Client = context.Clients.FirstOrDefault(c => c.Id == entity.ClientId);
        }
        return entity;
    }

    public async Task<Guid> Create(Rent entity)
    {
        await context.Rents.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> Update(Rent entity, Guid id)
    {
        context.Rents.Update(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await Read(id);
        if (entity == null) return false;
        context.Rents.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }
}
