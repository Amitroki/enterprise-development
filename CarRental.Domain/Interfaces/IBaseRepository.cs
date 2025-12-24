namespace CarRental.Domain.Interfaces;

/// <summary>
/// Defines the standard contract for a generic repository supporting CRUD operations.
/// </summary>
/// <typeparam name="TEntity">The type of the entity object.</typeparam>
public interface IBaseRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Adds a new entity to the collection and returns a unique ID.
    /// </summary>
    public Task<int> Create(TEntity entity);

    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    public Task<TEntity?> Read(int id);

    /// <summary>
    /// Returns all entities in the collection.
    /// </summary>
    public Task<List<TEntity>> ReadAll();

    /// <summary>
    /// Replaces an existing entity at the specified ID.
    /// </summary>
    public Task<bool> Update(TEntity entity, int id);

    /// <summary>
    /// Removes an entity from the collection by its ID.
    /// </summary>
    public Task<bool> Delete(int id);
}