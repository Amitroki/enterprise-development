using System.ComponentModel.DataAnnotations;

namespace CarRental.Application.Contracts.Generator;

/// <summary>
/// Request model for generating rental contracts
/// </summary>
public class GenerateRentalsRequest
{
    /// <summary>
    /// Total number of rentals to generate
    /// </summary>
    [Required]
    [Range(1, 1000, ErrorMessage = "TotalCount must be between 1 and 1000.")]
    public int TotalCount { get; set; }

    /// <summary>
    /// Number of rentals per batch
    /// </summary>
    [Required]
    [Range(1, 100, ErrorMessage = "BatchSize must be between 1 and 100.")]
    public int BatchSize { get; set; }

    /// <summary>
    /// Delay between batches in milliseconds
    /// </summary>
    [Required]
    [Range(100, 30000, ErrorMessage = "DelayMs must be between 0 and 60000.")]
    public int DelayMs { get; set; }
}