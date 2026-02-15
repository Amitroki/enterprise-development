namespace CarRental.Generator.Generation;

public class GeneratorOptions
{
    public List<Guid> CarIds { get; set; } = new();
    public List<Guid> ClientIds { get; set; } = new();
}
