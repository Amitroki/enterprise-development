namespace CarRental.Application.Contracts.Client;

public record ClientCreateUpdateDto(string FirstName, string LastName, string PhoneNumber, string DriverLicense, DateOnly BirthDate);