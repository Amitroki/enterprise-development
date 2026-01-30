using Microsoft.EntityFrameworkCore;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.Repository;

/// <summary>
/// Repository for managing car entities in the database
/// </summary>
/// <param name="context">The database context for car rental data</param>
public class DbCarRepository(CarRentalDbContext context) : IBaseRepository<Car, Guid>
{
    /// <summary>
    /// Retrieves all cars from the database
    /// </summary>
    /// <returns>A list of all car entities</returns>
    public async Task<List<Car>> ReadAll() => await context.Cars.ToListAsync();

    /// <summary>
    /// Finds a specific car by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the car</param>
    /// <returns>The car entity if found; otherwise, null</returns>
    public async Task<Car?> Read(Guid id) =>
        (await context.Cars.ToListAsync()).FirstOrDefault(x => x.Id == id);

    /// <summary>
    /// Adds a new car to the database
    /// </summary>
    /// <param name="entity">The car data to persist</param>
    /// <returns>The unique identifier of the created car</returns>
    public async Task<Guid> Create(Car entity)
    {
        await context.Cars.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Updates an existing car record
    /// </summary>
    /// <param name="entity">The updated car entity</param>
    /// <param name="id">The identifier of the car to update</param>
    /// <returns>True if the changes were saved successfully; otherwise, false</returns>
    public async Task<bool> Update(Car entity, Guid id)
    {
        context.Cars.Update(entity);
        return await context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Removes a car from the database by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the car to delete</param>
    /// <returns>True if the car was deleted successfully; otherwise, false</returns>
    public async Task<bool> Delete(Guid id)
    {
        var entity = await Read(id);
        if (entity is null) return false;
        context.Cars.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }
}