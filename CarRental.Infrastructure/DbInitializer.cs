using CarRental.Domain.DataSeed;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure;

/// <summary>
/// Performs a conditional data seed by checking if the database is empty 
/// and, if so, populating it with a predefined set of entities (from models to rents);
/// It ensures the system has necessary initial data while maintaining referential integrity through sequential updates
/// </summary>
public static class DbInitializer
{
    /// <summary>
    /// Asynchronously seeds the database with initial car rental data if the CarModels table is empty
    /// </summary>
    /// <param name="context">The database context instance used to persist the seed data</param>
    /// <returns>A task representing the asynchronous seeding operation</returns>
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