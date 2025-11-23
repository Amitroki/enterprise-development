using CarRental.Domain.InternalData.ComponentClasses;
using CarRental.Domain.InternalData.ComponentEnums;

namespace CarRental.Domain.IntenralData.ComponentClasses;

public class CarModelGeneration
{
    public required int Id { get; set; }

    public required int Year { get; set; }

    public required TransmissionType TransmissionType { get; set; }

    public required CarModel Model { get; set; }

    public required float HourCost { get; set; }
}
