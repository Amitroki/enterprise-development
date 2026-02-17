namespace CarRental.Application.Contracts.Client;

/// <summary>
/// Data transfer object for creating or updating client information.
/// </summary>
/// <param name="FirstName">The client's given name.</param>
/// <param name="LastName">The client's family name.</param>
/// <param name="Patronymic">The client's patronymic.</param>
/// <param name="PhoneNumber">The client's contact phone number.</param>
/// <param name="DriverLicenseId">The unique identifier of the client's driving license.</param>
/// <param name="BirthDate">The client's date of birth.</param>
public record ClientCreateUpdateDto(string FirstName, string LastName, string? Patronymic, string PhoneNumber, string DriverLicenseId, DateOnly? BirthDate);