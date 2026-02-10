using Microsoft.EntityFrameworkCore;
using CarRental.Domain.InternalData.ComponentClasses;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.Repository;

/// <summary>
/// Repository for managing car model generations in the database
/// </summary>
/// <param name="context">The database context for car rental data</param>
public class DbCarModelGenerationRepository(CarRentalDbContext context) : IBaseRepository<CarModelGeneration, Guid>
{
    /// <summary>
    /// Retrieves all model generations with their associated car models
    /// </summary>
    /// <returns>A list of all model generation entities</returns>
    public async Task<List<CarModelGeneration>> ReadAll()
    {
        var generations = await context.ModelGenerations.ToListAsync();
        var modelIds = generations.Select(g => g.ModelId).Distinct().ToList();
        var models = await context.CarModels
                                  .Where(m => modelIds.Contains(m.Id))
                                  .ToListAsync();
        foreach (var generation in generations)
        {
            generation.Model = models.FirstOrDefault(m => m.Id == generation.ModelId);
        }
        return generations;
    }

    /// <summary>
    /// Finds a specific model generation by id and loads its associated car model
    /// </summary>
    /// <param name="id">The unique identifier of the generation</param>
    /// <returns>The generation entity if found; otherwise, null</returns>
    public async Task<CarModelGeneration?> Read(Guid id)
    {
        var entity = await context.ModelGenerations.FirstOrDefaultAsync(x => x.Id == id);
        if (entity != null)
        {
            entity.Model = await context.CarModels.FirstOrDefaultAsync(m => m.Id == entity.ModelId);
        }
        return entity;
    }

    /// <summary>
    /// Adds a new model generation to the database
    /// </summary>
    /// <param name="entity">The model generation data to persist</param>
    /// <returns>The unique identifier of the created generation</returns>
    public async Task<Guid> Create(CarModelGeneration entity)
    {
        await context.ModelGenerations.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Updates an existing model generation record.
    /// </summary>
    /// <param name="entity">The updated generation entity</param>
    /// <param name="id">The identifier of the generation to update</param>
    /// <returns>True if the changes were saved successfully; otherwise, false</returns>
    public async Task<bool> Update(CarModelGeneration entity, Guid id)
    {
        context.ModelGenerations.Update(entity);
        return await context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Removes a model generation from the database by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the generation to delete</param>
    /// <returns>True if the deletion was successful; otherwise, false</returns>
    public async Task<bool> Delete(Guid id)
    {
        var entity = await Read(id);
        if (entity == null) return false;
        context.ModelGenerations.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }
}