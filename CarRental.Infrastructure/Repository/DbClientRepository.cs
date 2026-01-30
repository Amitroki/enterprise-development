using Microsoft.EntityFrameworkCore;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.Repository;

/// <summary>
/// Repository for managing client entities in the database
/// </summary>
/// <param name="context">The database context for car rental data</param>
public class DbClientRepository(CarRentalDbContext context) : IBaseRepository<Client, Guid>
{
    /// <summary>
    /// Retrieves all clients from the database
    /// </summary>
    /// <returns>A list of all client entities</returns>
    public async Task<List<Client>> ReadAll() => await context.Clients.ToListAsync();

    /// <summary>
    /// Finds a specific client by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the client</param>
    /// <returns>The client entity if found; otherwise, null</returns>
    public async Task<Client?> Read(Guid id) =>
        (await context.Clients.ToListAsync()).FirstOrDefault(x => x.Id == id);

    /// <summary>
    /// Adds a new client to the database
    /// </summary>
    /// <param name="entity">The client data to persist</param>
    /// <returns>The unique identifier of the created client</returns>
    public async Task<Guid> Create(Client entity)
    {
        await context.Clients.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Updates an existing client's information
    /// </summary>
    /// <param name="entity">The updated client entity</param>
    /// <param name="id">The identifier of the client to update</param>
    /// <returns>True if the changes were saved successfully; otherwise, false</returns>
    public async Task<bool> Update(Client entity, Guid id)
    {
        context.Clients.Update(entity);
        return await context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Removes a client from the database by their identifier
    /// </summary>
    /// <param name="id">The unique identifier of the client to delete</param>
    /// <returns>True if the client was deleted successfully; otherwise, false</returns>
    public async Task<bool> Delete(Guid id)
    {
        var entity = await Read(id);
        if (entity is null) return false;
        context.Clients.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }
}