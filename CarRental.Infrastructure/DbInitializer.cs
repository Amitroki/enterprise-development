using CarRental.Domain.DataSeed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
    /// <param name="logger">The logger instance for capturing diagnostic information</param>
    /// <returns>A task representing the asynchronous seeding operation</returns>
    public static async Task SeedData(CarRentalDbContext context, ILogger logger)
    {
        try
        {
            if (await context.CarModels.AnyAsync())
            {
                logger.LogInformation("The database if already filled");
                return;
            }
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Connection with database is not establised");
        }

        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var data = new DataSeed();
            logger.LogInformation("The process of database's filling is starting...");

            await context.CarModels.AddRangeAsync(data.Models);
            await context.SaveChangesAsync();
            logger.LogInformation("Car models were successfully uploaded");

            await context.ModelGenerations.AddRangeAsync(data.Generations);
            await context.SaveChangesAsync();
            logger.LogInformation("Model's generations were successfully uploaded");

            await context.Cars.AddRangeAsync(data.Cars);
            await context.SaveChangesAsync();
            logger.LogInformation("Cars were successfully uploaded");

            await context.Clients.AddRangeAsync(data.Clients);
            await context.SaveChangesAsync();
            logger.LogInformation("Clients were successfully uploaded");

            await context.Rents.AddRangeAsync(data.Rents);
            await context.SaveChangesAsync();
            logger.LogInformation("Rents were successfully uploaded");

            await transaction.CommitAsync();
            logger.LogInformation("Database was successfully initialized!");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "The problem with filling database. Check logs for more information");
            throw;
        }
    }
}