using CarRental.Domain.InternalData.ComponentEnums;


namespace CarRental.Domain.InternalData.ComponentClasses;


public class CarModel
{
    public required int Id { get; set; }

    public string Name { get; set; }

    public required DriveType DriveType { get; set; }

    public required uint SeatsNumber { get; set; }
    
    public required BodyType BodyType { get; set; }

    public required ClassType ClassType { get; set; }
}

