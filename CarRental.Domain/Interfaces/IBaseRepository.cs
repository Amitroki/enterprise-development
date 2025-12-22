namespace CarRental.Domain.Interfaces;

public interface IBaseRepository<TEntity>
    where TEntity : class
{
    public uint Create(TEntity entity);
    public TEntity? Read(uint id);
    public List<TEntity> ReadAll();
    public bool Update(TEntity entity, uint id);
    public bool Delete(uint id);
}