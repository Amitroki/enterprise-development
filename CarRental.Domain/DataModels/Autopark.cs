using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Carrental.Domain.InternalData.ComponentClasses;

public class Autopark
{
    [Key]
    [Required(ErrorMessage = "Car's ID is required")]
    [Column("id")]
    public required int Id { get; set; }

    [Required]
    [Column("model_generation")]
    public required CarModelGeneration ModelGeneration { get; set; }

    [Required(ErrorMessage = "Car's number plate is required")]
    [Column("number_plate")]
    public required string NumberPlate { get; set; }

    [Required(ErrorMessage = "Car's colour is required")]
    [Column("colour")]
    public required string Colour { get; set; }

}