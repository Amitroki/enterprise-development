namespace CarRental.Application.Contracts.Client;

/// <summary>
/// Data transfer object representing client details for display and identification.
/// </summary>
/// <param name="Id">The unique identifier of the client record.</param>
/// <param name="DriverLicenseId">The identification number of the client's driver license.</param>
/// <param name="LastName">The client's family name.</param>
/// <param name="FirstName">The client's first name.</param>
/// <param name="Patronymic">The client's middle name (optional).</param>
/// <param name="BirthDate">The client's date of birth (optional).</param>
public record ClientDto(Guid Id, string DriverLicenseId, string LastName, string FirstName, string? Patronymic, DateOnly? BirthDate);