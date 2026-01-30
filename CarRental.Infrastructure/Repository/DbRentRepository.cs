using Microsoft.EntityFrameworkCore;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.Repository;

/// <summary>
/// Repository for managing rent records in the database
/// </summary>
/// <param name="context">The database context for car rental data</param>
public class DbRentRepository(CarRentalDbContext context) : IBaseRepository<Rent, Guid>
{
    /// <summary>
    /// Retrieves all rent records with populated car and client details
    /// </summary>
    /// <returns>A list of all rent entities</returns>
    public async Task<List<Rent>> ReadAll() =>
        (await context.Rents.ToListAsync())
        .Select(r =>
        {
            r.Car = context.Cars.FirstOrDefault(c => c.Id == r.CarId)!;
            r.Client = context.Clients.FirstOrDefault(c => c.Id == r.ClientId)!;
            return r;
        }).ToList();

    /// <summary>
    /// Retrieves a specific rent record by its identifier with linked data
    /// </summary>
    /// <param name="id">The unique identifier of the rent</param>
    /// <returns>The rent entity if found; otherwise, null</returns>
    public async Task<Rent?> Read(Guid id)
    {
        var list = await context.Rents.ToListAsync();
        var entity = list.FirstOrDefault(r => r.Id == id);
        if (entity != null)
        {
            entity.Car = context.Cars.FirstOrDefault(c => c.Id == entity.CarId)!;
            entity.Client = context.Clients.FirstOrDefault(c => c.Id == entity.ClientId)!;
        }
        return entity;
    }

    /// <summary>
    /// Creates a new rent record in the database
    /// </summary>
    /// <param name="entity">The rent entity to create</param>
    /// <returns>The identifier of the created rent</returns>
    public async Task<Guid> Create(Rent entity)
    {
        await context.Rents.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Updates an existing rent record
    /// </summary>
    /// <param name="entity">The updated rent entity</param>
    /// <param name="id">The identifier of the rent to update</param>
    /// <returns>True if the update was successful</returns>
    public async Task<bool> Update(Rent entity, Guid id)
    {
        context.Rents.Update(entity);
        return await context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Deletes a rent record by its identifier
    /// </summary>
    /// <param name="id">The identifier of the rent to delete</param>
    /// <returns>True if the deletion was successful</returns>
    public async Task<bool> Delete(Guid id)
    {
        var entity = await Read(id);
        if (entity == null) return false;
        context.Rents.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }
}
