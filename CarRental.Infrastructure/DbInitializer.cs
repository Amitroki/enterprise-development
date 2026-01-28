using CarRental.Domain.DataSeed;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure;

public static class DbInitializer
{
    public static async Task SeedData(CarRentalDbContext context)
    {
        if (await context.CarModels.AnyAsync()) return;

        var data = new DataSeed();

        await context.CarModels.AddRangeAsync(data.Models);
        await context.SaveChangesAsync();

        await context.ModelGenerations.AddRangeAsync(data.Generations);
        await context.SaveChangesAsync();

        await context.Cars.AddRangeAsync(data.Cars);
        await context.SaveChangesAsync();

        await context.Clients.AddRangeAsync(data.Clients);
        await context.SaveChangesAsync();

        await context.Rents.AddRangeAsync(data.Rents);
        await context.SaveChangesAsync();
    }
}