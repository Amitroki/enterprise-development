namespace CarRental.Domain.InternalData.ComponentEnums;

/// <summary>
/// The type of vehicle drive system
/// </summary>
public enum DriveType {
    /// <summary>
    /// Front-wheel drive, where power is delivered to the front wheels
    /// </summary>
    FrontWheel,

    /// <summary>
    /// Rear-wheel drive, where power is delivered to the rear wheels
    /// </summary>
    RearWheel,

    /// <summary>
    /// All-wheel drive, where power is distributed to all wheels
    /// </summary>
    AllWheel
}