using Microsoft.EntityFrameworkCore;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure;

namespace CarRental.Infrastructure.Repository;

public class DbRentRepository(CarRentalDbContext context) : IBaseRepository<Rent>
{
    public async Task<List<Rent>> ReadAll() =>
        await context.Rents.Include(r => r.Car).Include(r => r.Client).ToListAsync();

    public async Task<Rent?> Read(int id) =>
        (await context.Rents.Include(r => r.Car).Include(r => r.Client).ToListAsync())
        .FirstOrDefault(x => x.Id == id);

    public async Task<int> Create(Rent entity)
    {
        await context.Rents.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> Update(Rent entity, int id)
    {
        context.Rents.Update(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await Read(id);
        if (entity is null) return false;
        context.Rents.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }
}