namespace CarRental.Application.Contracts.CarModelGeneration;

/// <summary>
/// Data transfer object for creating or updating a specific car model generation.
/// </summary>
/// <param name="Year">The manufacturing year of the generation.</param>
/// <param name="TransmissionType">The type of transmission (e.g., Manual, Automatic).</param>
/// <param name="HourCost">The rental cost per hour for this generation.</param>
/// <param name="ModelId">The unique identifier of the parent car model.</param>
public record CarModelGenerationCreateUpdateDto(int Year, string? TransmissionType, decimal HourCost, int ModelId);