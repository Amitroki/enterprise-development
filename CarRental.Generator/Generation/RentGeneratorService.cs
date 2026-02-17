using CarRental.Application.Contracts.Rent;
using Microsoft.Extensions.Options;
using Bogus;

namespace CarRental.Generator.Generation;

/// <summary>
/// Service for generating fake rental contract data
/// </summary>
public class RentGeneratorService
{
    private readonly GeneratorOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="RentGeneratorService"/> class
    /// </summary>
    /// <param name="options">Generator configuration options</param>
    public RentGeneratorService(IOptions<GeneratorOptions> options)
    {
        _options = options.Value;
    }

    /// <summary>
    /// Generates a specified number of fake rental contracts
    /// </summary>
    /// <param name="count">Number of contracts to generate</param>
    /// <returns>List of generated rental DTOs</returns>
    /// <exception cref="InvalidOperationException">Thrown when CarIds or ClientIds lists are empty</exception>
    public IList<RentCreateUpdateDto> GenerateContract(int count)
    {
        if (_options.CarIds == null || !_options.CarIds.Any())
            throw new InvalidOperationException("CarIds list is empty. Check appsettings.json Generator section.");

        if (_options.ClientIds == null || !_options.ClientIds.Any())
            throw new InvalidOperationException("ClientIds list is empty. Check appsettings.json Generator section.");

        var generatedRents = new Faker<RentCreateUpdateDto>().CustomInstantiator(f => new RentCreateUpdateDto(
            StartDateTime: f.Date.Soon(1),
            Duration: f.Random.Double(1, 100),
            CarId: Guid.Parse(f.PickRandom(_options.CarIds)),
            ClientId: Guid.Parse(f.PickRandom(_options.ClientIds))
            )
        );

        return generatedRents.Generate(count); 
    }
}
