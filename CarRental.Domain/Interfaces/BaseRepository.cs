namespace CarRental.Domain.Interfaces;

/// <summary>
/// Provides a base implementation for in-memory CRUD operations.
/// </summary>
/// <typeparam name="TEntity">The type of the entity managed by the repository.</typeparam>
public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
	where TEntity : class
{
    /// <summary>
    /// Private field for obtaining a unique identifier 
    /// to assign it to the next entity in the repository
    /// </summary>

    private int _nextId;

    private readonly List<TEntity> _entities;
    /// <summary>
    /// Gets the unique identifier from the entity.
    /// </summary>
    protected abstract int GetEntityId(TEntity entity);

    /// <summary>
    /// Sets the unique identifier for the entity
    /// </summary>
    protected abstract void SetEntityId(TEntity entity, int id);

    /// <summary>
    /// Initializes the repository and determines the starting ID based on existing data.
    /// </summary>
    protected BaseRepository(List<TEntity>? entities = null)
    {
        _entities = entities ?? new List<TEntity>();
        if (_entities.Count > 0)
        {
            _nextId = _entities.Max(e => GetEntityId(e)) + 1;
        }
        else
        {
            _nextId = 1;
        }
    }

    /// <summary>
    /// Adds a new entity to the collection and assigns a unique ID.
    /// </summary>
    public virtual Task<int> Create(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        var currentId = _nextId;
        SetEntityId(entity, currentId);
        _entities.Add(entity);
        _nextId++;
        return Task.FromResult(currentId);
    }

    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    public virtual Task<TEntity?> Read(int id)
    {
        return Task.FromResult(_entities.FirstOrDefault(e => GetEntityId(e) == id));
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
    public virtual async Task<bool> Update(TEntity entity, int id)
    {
        var existing = await Read(id);
        if (existing != null)
        {
            var index = _entities.IndexOf(existing);
            SetEntityId(entity, id);
            _entities[index] = entity;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Removes an entity from the collection by its ID.
    /// </summary>
    public virtual async Task<bool> Delete(int id)
    {
        var entity = await Read(id);
        if (entity != null)
        {
            return _entities.Remove(entity);
        }
        return false;
    }
}