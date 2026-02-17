namespace CarRental.Generator.Generation;

/// <summary>
/// Configuration options for the rental data generator
/// </summary>
public class GeneratorOptions
{
    /// <summary>
    /// List of available car IDs to randomly select from
    /// </summary>
    public List<string> CarIds { get; set; } = new();

    /// <summary>
    /// List of available client IDs to randomly select from
    /// </summary>
    public List<string> ClientIds { get; set; } = new();
}
