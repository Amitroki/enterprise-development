using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Domain.DataModels;

public class Client {

	public required uint Id { get; set; }

	public required string DriverLicenseId { get; set; }

	public required string LastName { get; set; }

	public required string FirstName { get; set; }

	public required string Patronymic { get; set; }

	public required DateOnly? BirthDate { get; set; }
}