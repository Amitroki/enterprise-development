namespace CarRental.Generator.Generation;

/// <summary>
/// Configuration options for the rental data generator
/// </summary>
public class GeneratorOptions
{
    private List<string>? _carIdStrings;
    private List<string>? _clientIdStrings;
    private List<Guid>? _carIds;
    private List<Guid>? _clientIds;

    /// <summary>
    /// List of available car IDs as strings
    /// </summary>
    public List<string> CarIds
    {
        get => _carIdStrings ?? new();
        set
        {
            _carIdStrings = value;
            _carIds = value?.Select(Guid.Parse).ToList();
        }
    }

    /// <summary>
    /// List of available client IDs as strings
    /// </summary>
    public List<string> ClientIds
    {
        get => _clientIdStrings ?? new();
        set
        {
            _clientIdStrings = value;
            _clientIds = value?.Select(Guid.Parse).ToList();
        }
    }

    /// <summary>
    /// List of available car IDs as GUIDs
    /// </summary>
    public List<Guid> CarIdGuids => _carIds ?? new();

    /// <summary>
    /// List of available client IDs as GUIDs
    /// </summary>
    public List<Guid> ClientIdGuids => _clientIds ?? new();
}