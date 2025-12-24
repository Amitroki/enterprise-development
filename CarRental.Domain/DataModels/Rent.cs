namespace CarRental.Domain.DataModels;

/// <summary>
/// Represents a car rental agreement between a client and the rental company
/// </summary>
public class Rent 
{
    /// <summary>
    /// Unique identifier of the rental record
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Date and time when the rental period starts
    /// </summary>
    public required DateTime StartDateTime { get; set; }

    /// <summary>
    /// Duration of the rental in hours
    /// </summary>
    public required double Duration { get; set; }

    /// <summary>
    /// The car that is being rented
    /// </summary>
    public required Car Car {  get; set; }

    /// <summary>
    /// The client who is renting the car
    /// </summary>
    public required Client Client { get; set; }
}
