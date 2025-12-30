namespace CarRental.Domain.DataModels;

/// <summary>
/// Represents a car rental agreement between a client and the rental company
/// </summary>
public class Rent 
{
    /// <summary>
    /// Unique identifier of the rental record
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Date and time when the rental period starts
    /// </summary>
    public required DateTime StartDateTime { get; set; }

    /// <summary>
    /// Duration of the rental in hours
    /// </summary>
    public required double Duration { get; set; }

    /// <summary>
    /// The car ID that is being rented
    /// </summary>
    public required Guid CarId { get; set; }

    /// <summary>
    /// The car that is being rented
    /// </summary>
    public required Car Car {  get; set; }

    /// <summary>
    /// The client ID who is renting the car
    /// </summary>
    public required Guid ClientId { get; set; }

    /// <summary>
    /// The client who is renting the car
    /// </summary>
    public required Client Client { get; set; }
}
