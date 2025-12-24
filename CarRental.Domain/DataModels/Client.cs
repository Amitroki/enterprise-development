namespace CarRental.Domain.DataModels;

/// <summary>
/// Represents a client (rental customer) with personal and identification information
/// </summary>
public class Client 
{
    /// <summary>
    /// Unique identifier of the client
    /// </summary>
    public required uint Id { get; set; }

    /// <summary>
    /// Unique identifier of the client's driver's license
    /// </summary>
    public required string DriverLicenseId { get; set; }

    /// <summary>
    /// Client's last name (surname)
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Client's first name (given name)
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Client's patronymic (middle name), if applicable
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Client's date of birth
    /// </summary>
    public DateOnly? BirthDate { get; set; }
}