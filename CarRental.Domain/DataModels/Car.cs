using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CarRental.Domain.InternalData.ComponentClasses;

namespace CarRental.Domain.DataModels;

public class Car {
    
    public required int Id { get; set; }

    public required CarModelGeneration ModelGeneration { get; set; }

    public required string NumberPlate { get; set; }

    public required string Colour { get; set; }

}