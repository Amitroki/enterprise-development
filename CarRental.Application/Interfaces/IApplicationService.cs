namespace CarRental.Application.Interfaces;

/// <summary>
/// Defines a generic contract for application services handling mapping between entities and DTOs.
/// </summary>
/// <typeparam name="TDto">The data transfer object used for output.</typeparam>
/// <typeparam name="TCreateUpdateDto">The data transfer object used for input operations.</typeparam>
public interface IApplicationService<TDto, TCreateUpdateDto>
    where TDto : class
    where TCreateUpdateDto : class
{
    /// <summary>
    /// Creates a new record from the provided input DTO and returns the resulting output DTO.
    /// </summary>
    public TDto Create(TCreateUpdateDto dto);

    /// <summary>
    /// Retrieves a single record by its unique identifier, mapped to an output DTO.
    /// </summary>
    public TDto? Read(uint id);

    /// <summary>
    /// Retrieves all records mapped to a list of output DTOs.
    /// </summary>
    public List<TDto> ReadAll();

    /// <summary>
    /// Updates an existing record identified by the given ID using the input DTO data.
    /// </summary>
    public bool Update(TCreateUpdateDto dto, uint id);

    /// <summary>
    /// Removes a record from the system by its unique identifier.
    /// </summary>
    public bool Delete(uint id);
}