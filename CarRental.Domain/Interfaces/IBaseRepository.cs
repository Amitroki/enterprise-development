namespace CarRental.Domain.Interfaces;

/// <summary>
/// Defines the standard contract for a generic repository supporting CRUD operations.
/// </summary>
/// <typeparam name="TEntity">The type of the entity object.</typeparam>
/// <typeparam name="TKey">The type of the key.</typeparam>
public interface IBaseRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Adds a new entity to the collection and returns a unique ID.
    /// </summary>
    public Task<TKey> Create(TEntity entity);

    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    public Task<TEntity?> Read(TKey id);

    /// <summary>
    /// Returns all entities in the collection.
    /// </summary>
    public Task<List<TEntity>> ReadAll();

    /// <summary>
    /// Replaces an existing entity at the specified ID.
    /// </summary>
    public Task<bool> Update(TEntity entity, TKey id);

    /// <summary>
    /// Removes an entity from the collection by its ID.
    /// </summary>
    public Task<bool> Delete(TKey id);
}