namespace CarRental.Application.Contracts.Generator;

/// <summary>
/// Response model for rental generation operation
/// </summary>
public class GenerateRentalsResponse
{
    /// <summary>
    /// Total number of items requested
    /// </summary>
    public int TotalRequested { get; set; }

    /// <summary>
    /// Total number of items successfully sent
    /// </summary>
    public int TotalSent { get; set; }

    /// <summary>
    /// Size of each batch
    /// </summary>
    public int BatchSize { get; set; }

    /// <summary>
    /// Delay between batches in milliseconds
    /// </summary>
    public int DelayMs { get; set; }

    /// <summary>
    /// Number of batches sent
    /// </summary>
    public int Batches { get; set; }

    /// <summary>
    /// Whether the operation was canceled
    /// </summary>
    public bool Canceled { get; set; }
}