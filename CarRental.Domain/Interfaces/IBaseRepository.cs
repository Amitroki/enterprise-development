namespace CarRental.Domain.Interfaces;

public interface IBaseRepository<TEntity, TKey>
    where TEntity : class
{
    uint Create(TEntity entity);
    TEntity? Read(uint id);
    List<TEntity> ReadAll();
    void Update(TEntity entity, uint id);
    bool Delete(uint id);
}