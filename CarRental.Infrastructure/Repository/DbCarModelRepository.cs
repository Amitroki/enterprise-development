using Microsoft.EntityFrameworkCore;
using CarRental.Domain.InternalData.ComponentClasses;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.Repository;

/// <summary>
/// Repository for managing car model entities in the database
/// </summary>
/// <param name="context">The database context for car rental data</param>
public class DbCarModelRepository(CarRentalDbContext context) : IBaseRepository<CarModel, Guid>
{
    /// <summary>
    /// Retrieves all car models from the database
    /// </summary>
    /// <returns>A list of all car model entities</returns>
    public async Task<List<CarModel>> ReadAll() => await context.CarModels.ToListAsync();

    /// <summary>
    /// Finds a specific car model by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the car model</param>
    /// <returns>The car model entity if found; otherwise, null</returns>
    public async Task<CarModel?> Read(Guid id) =>
        (await context.CarModels.ToListAsync()).FirstOrDefault(x => x.Id == id);

    /// <summary>
    /// Adds a new car model to the database
    /// </summary>
    /// <param name="entity">The car model data to persist</param>
    /// <returns>The unique identifier of the created car model</returns>
    public async Task<Guid> Create(CarModel entity)
    {
        await context.CarModels.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Updates an existing car model record
    /// </summary>
    /// <param name="entity">The updated car model entity</param>
    /// <param name="id">The identifier of the car model to update</param>
    /// <returns>True if the changes were saved successfully; otherwise, false</returns>
    public async Task<bool> Update(CarModel entity, Guid id)
    {
        context.CarModels.Update(entity);
        return await context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Removes a car model from the database by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the car model to delete</param>
    /// <returns>True if the car model was deleted successfully; otherwise, false</returns>
    public async Task<bool> Delete(Guid id)
    {
        var entity = await Read(id);
        if (entity is null) return false;
        context.CarModels.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }
}