namespace CarRental.Domain.InternalData.ComponentEnums;

/// <summary>
/// The type of vehicle transmission
/// </summary>
public enum TransmissionType 
{
    /// <summary>
    /// Manual gearbox with driver-operated gear shifting
    /// </summary>
    Manual,

    /// <summary>
    /// Automatic gearbox requiring no driver input for shifting
    /// </summary>
    Automatic,

    /// <summary>
    /// Robotic gearbox with automated clutch control
    /// </summary>
    Robotic,

    /// <summary>
    /// Continuously variable transmission (CVT) with stepless gear ratio adjustment
    /// </summary>
    Variable
}