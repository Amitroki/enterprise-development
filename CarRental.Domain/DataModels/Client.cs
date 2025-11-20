using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Domain.DataModels;

[Table("client")]
public class Client {

	[Key]
	[Required(ErrorMessage = "Client's ID is required")]
	[Column("id")]
	public required uint Id { get; set; }

	[Required(ErrorMessage = "Client's driver license ID is required")]
	[StringLength(10, ErrorMessage = "The driver license ID's length should not exceed 50 characters")]
	[Column("driver_license")]
	public required string DriverLicenseId { get; set; }

	[Required(ErrorMessage = "Client's last name is required")]
	[StringLength(50, ErrorMessage = "The last name's length should not exceed 50 characters")]
	[Column("last_name")]
	public required string LastName { get; set; }

	[Required(ErrorMessage = "Client's first name is required")]
	[StringLength(50, ErrorMessage = "The first name's length should not exceed 50 characters")]
	[Column("first_name")]
	public required string FirstName { get; set; }

	[Required(ErrorMessage = "Client's patronymic is required")]
	[StringLength(50, ErrorMessage = "The patronymic's length should not exceed 50 characters")]
	[Column("patronymic")]
	public required string Patronymic { get; set; }

	[Required(ErrorMessage = "Client's date of birth is required")]
	[Column("birth_date")]
	public required DateOnly? BirthDate { get; set; }
}