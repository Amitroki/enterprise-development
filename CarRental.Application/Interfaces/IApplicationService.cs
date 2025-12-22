namespace CarRental.Application.Interfaces;

public interface IApplicationService<TDto, TCreateUpdateDto>
    where TDto : class
    where TCreateUpdateDto : class
{
    public TDto Create(TCreateUpdateDto dto);

    public TDto? Read(uint id);

    public List<TDto> ReadAll();

    public bool Update(TCreateUpdateDto dto, uint id);

    public bool Delete(uint id);
}