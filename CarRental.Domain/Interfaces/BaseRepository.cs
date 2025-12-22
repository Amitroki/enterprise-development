namespace CarRental.Domain.Interfaces;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
	where TEntity : class
{
    private uint _nextId;

    protected abstract uint GetEntityId(TEntity entity);

    protected abstract void SetEntityId(TEntity entity, uint id);

    private readonly List<TEntity> _entities;

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

    public virtual uint Create(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        var currentId = _nextId;
        SetEntityId(entity, currentId);
        _entities.Add(entity);
        _nextId++;
        return currentId;
    }

    public virtual TEntity? Read(uint id)
    {
        return _entities.FirstOrDefault(e => GetEntityId(e) == id);
    }

    public virtual List<TEntity> ReadAll()
    {
       return _entities.ToList();
    }

    public virtual bool Update(TEntity entity, uint id)
    {
        var existing = Read(id);
        if (existing != null)
        {
            var index = _entities.IndexOf(existing);
            SetEntityId(entity, id);
            _entities[index] = entity;
            return true;
        }
        return false;
    }

    public virtual bool Delete(uint id)
    {
        var entity = Read(id);
        if (entity != null)
        {
            return _entities.Remove(entity);
        }
        return false;
    }


}