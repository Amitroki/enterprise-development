namespace CarRental.Domain.Interfaces;

/// <summary>
/// Provides a base implementation for in-memory CRUD operations.
/// </summary>
/// <typeparam name="TEntity">The type of the entity managed by the repository.</typeparam>
public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity, Guid>
	where TEntity : class
{
    private readonly List<TEntity> _entities;
    /// <summary>
    /// Gets the unique identifier from the entity.
    /// </summary>
    protected abstract Guid GetEntityId(TEntity entity);

    /// <summary>
    /// Sets the unique identifier for the entity
    /// </summary>
    protected abstract void SetEntityId(TEntity entity, Guid id);

    /// <summary>
    /// Initializes the repository and determines the starting ID based on existing data.
    /// </summary>
    protected BaseRepository(List<TEntity>? entities = null)
    {
        _entities = entities ?? new List<TEntity>();
    }

    /// <summary>
    /// Adds a new entity to the collection and assigns a unique ID.
    /// </summary>
    public virtual Task<Guid> Create(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        var id = Guid.NewGuid();
        SetEntityId(entity, id);
        _entities.Add(entity);
        return Task.FromResult(id);
    }

    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    public virtual Task<TEntity?> Read(Guid id)
    {
        return Task.FromResult(
            _entities.FirstOrDefault(e => GetEntityId(e) == id)
        );
    }

    /// <summary>
    /// Returns all entities in the collection.
    /// </summary>
    public virtual Task<List<TEntity>> ReadAll()
    {
       return Task.FromResult(_entities.ToList());
    }

    /// <summary>
    /// Replaces an existing entity at the specified ID.
    /// </summary>
    public virtual async Task<bool> Update(TEntity entity, Guid id)
    {
        var existing = await Read(id);
        if (existing == null)
            return false;

        var index = _entities.IndexOf(existing);
        SetEntityId(entity, id);
        _entities[index] = entity;

        return true;
    }

    /// <summary>
    /// Removes an entity from the collection by its ID.
    /// </summary>
    public virtual async Task<bool> Delete(Guid id)
    {
        var entity = await Read(id);
        return entity != null && _entities.Remove(entity);
    }
}