namespace CarRental.Generator.Generation;

/// <summary>
/// Configuration options for the rental data generator.
/// </summary>
public class GeneratorOptions
{
    private List<string>? _carIdStrings;
    private List<string>? _clientIdStrings;
    private List<Guid>? _carIds;
    private List<Guid>? _clientIds;

    /// <summary>
    /// List of available car IDs as strings (for configuration binding).
    /// </summary>
    public List<string> CarIds
    {
        get => _carIdStrings ?? new();
        set
        {
            _carIdStrings = value;
            _carIds = ParseGuids(value, "CarIds");
        }
    }

    /// <summary>
    /// List of available client IDs as strings (for configuration binding).
    /// </summary>
    public List<string> ClientIds
    {
        get => _clientIdStrings ?? new();
        set
        {
            _clientIdStrings = value;
            _clientIds = ParseGuids(value, "ClientIds");
        }
    }

    /// <summary>
    /// List of available car IDs as GUIDs (pre-parsed for performance).
    /// </summary>
    public List<Guid> CarIdGuids => _carIds ?? new();

    /// <summary>
    /// List of available client IDs as GUIDs (pre-parsed for performance).
    /// </summary>
    public List<Guid> ClientIdGuids => _clientIds ?? new();

    private static List<Guid> ParseGuids(List<string>? values, string fieldName)
    {
        if (values == null)
            return new List<Guid>();
        var result = new List<Guid>();
        for (var i = 0; i < values.Count; i++)
        {
            var value = values[i];
            if (string.IsNullOrWhiteSpace(value))
                throw new FormatException($"{fieldName}[{i}] is null or empty");
            if (!Guid.TryParse(value, out var guid))
                throw new FormatException($"{fieldName}[{i}] has invalid GUID format: {value}");

            result.Add(guid);
        }

        return result;
    }
}