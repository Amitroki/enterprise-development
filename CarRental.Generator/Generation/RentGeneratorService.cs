using CarRental.Application.Contracts.Rent;
using Microsoft.Extensions.Options;
using Bogus;
using CarRental.Generator.Generation;

namespace CarRental.Generator;

public static class RentGeneratorService
{
    private readonly GeneratorOptions _options;

    public RentGeneratorService(IOptions<GeneratorOptions> options)
    {
        _options = options.Value;
    }

    public static IList<RentCreateUpdateDto> GenerateContract(int count)
    {
        var generatedRents = new Faker<RentCreateUpdateDto>().CustomInstantiator(f => new RentCreateUpdateDto(
            StartDateTime: f.Date.Soon(1),
            Duration: f.Random.Double(1, 100),
            CarId: f.PickRandom(_options.CarIds),
            ClientId: f.PickRandom(_options.ClientIds)
            )
        );

        return generatedRents.Generate(count); 
    }
}
