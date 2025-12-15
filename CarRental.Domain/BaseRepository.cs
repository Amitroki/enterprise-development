namespace CarRental.Domain;

public abstract class BaseRepository<TEntity, TKey>
	where TEntity : class
	where TKey: struct
{
    private uint _nextId;

    protected abstract TKey GetEntityId(TEntity entity);

    protected abstract void SetEntityId(TEntity entity, TKey id);

    private readonly List<TEntity> _entities;

    protected Repository(List<TEntity>? entities = null)
    {
        if (entities != null)
        {
            _entities = entities;
            _nextId = _entities.Count + 1;
        }
        else
        {
            _entities = new List<TEntity>();
            _nextId = 1;
        }
    }

    public virtual uint Create(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        uint currentId = _nextId;
        SetEntityId(entity, currentId);
        _entities.Add(entity);
        _nextId++;
        return currentId;
    }

    public virtual TEntity? Read(uint id)
    {
        return _entities.FirstOrDefault(c => c.Id == id);
    }

    public virtual List<TEntity> ReadAll()
    {
        List<TEntity> copy = _entities;
        return copy;
    }

    public virtual void Update(TEntity entity)
    {
        Delete(entity.Id);
        _entities.Add(entity);
    }

    public virtual bool Delete(uint id)
    {
        if (_entities[id] != null)
        {
            _entities.RemoveAt(id);
            return true;
        }
        return false;
    }


}