namespace CarRental.Domain;

public interface IRepository<TEntity, TKey>
	where TEntity : class
{
	public TKey Create(TEntity entity);

	public TEntity? Read(TKey id);

	public List<TEntity> ReadAll();

	public void Update(TEntity entity);

	public bool Delete(TKey id);

}