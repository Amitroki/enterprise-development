namespace CarRental.Application.Interfaces;

public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    public TDto Create(TCreateUpdateDto dto);

    public TDto? Read(TKey id);

    public List<TDto> ReadAll();

    public TDto Update(TCreateUpdateDto dto, TKey id);

    public bool Delete(TKey id);
}