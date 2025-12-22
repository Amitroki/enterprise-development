namespace CarRental.Application.Contracts.Client;

public record ClientDto(uint Id, string DriverLicenseId, string LastName, string FirstName, string? Patronymic, DateOnly? BirthDate);