namespace CarRental.Application.Contracts.CarModelGeneration;

/// <summary>
/// Data transfer object representing a specific car model generation with pricing and details.
/// </summary>
/// <param name="Id">The unique identifier of the car model generation.</param>
/// <param name="Year">The manufacturing year of the generation.</param>
/// <param name="TransmissionType">The type of transmission used in this generation.</param>
/// <param name="HourCost">The rental cost per hour.</param>
/// <param name="ModelId">The identifier of the parent car model.</param>
public record CarModelGenerationDto(Guid Id, int Year, string? TransmissionType, decimal HourCost, Guid ModelId);