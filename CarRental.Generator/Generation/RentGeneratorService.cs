using CarRental.Application.Contracts.Rent;
using Microsoft.Extensions.Options;
using Bogus;

namespace CarRental.Generator.Generation;

/// <summary>
/// Service for generating fake rental contract data
/// </summary>
/// <param name="optionsMonitor">Generator configuration options monitor</param>
public class RentGeneratorService(IOptionsMonitor<GeneratorOptions> optionsMonitor)
{
    /// <summary>
    /// Generates a specified number of fake rental contracts
    /// </summary>
    /// <param name="count">Number of contracts to generate</param>
    /// <returns>List of generated rental DTOs</returns>
    public IList<RentCreateUpdateDto> GenerateContract(int count)
    {
        var options = optionsMonitor.CurrentValue;

        if (!options.CarIdGuids.Any())
            throw new InvalidOperationException("CarIds list is empty. Check configuration.");

        if (!options.ClientIdGuids.Any())
            throw new InvalidOperationException("ClientIds list is empty. Check configuration.");

        var generatedRents = new Faker<RentCreateUpdateDto>().CustomInstantiator(f => new RentCreateUpdateDto(
            StartDateTime: f.Date.Soon(1),
            Duration: f.Random.Double(1, 100),
            CarId: f.PickRandom(options.CarIdGuids),
            ClientId: f.PickRandom(options.ClientIdGuids)
        ));

        return generatedRents.Generate(count);
    }
}