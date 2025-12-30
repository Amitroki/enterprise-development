using Microsoft.EntityFrameworkCore;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure;

namespace CarRental.Infrastructure.Repository;

public class DbClientRepository(CarRentalDbContext context) : IBaseRepository<Client, Guid>
{ 
    public async Task<List<Client>> ReadAll() => await context.Clients.ToListAsync();

    public async Task<Client?> Read(Guid id) =>
        (await context.Clients.ToListAsync()).FirstOrDefault(x => x.Id == id);

    public async Task<Guid> Create(Client entity)
    {
        await context.Clients.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> Update(Client entity, Guid id)
    {
        context.Clients.Update(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await Read(id);
        if (entity is null) return false;
        context.Clients.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }
}