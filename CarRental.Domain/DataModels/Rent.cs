using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Domain.DataModels;

public class Rent {
    public int Id { get; set; }

    public required DateTime StartDateTime { get; set; }

    public required int Duration { get; set; }

    public required Autopark Car {  get; set; }

    public required Client Client { get; set; }
}
