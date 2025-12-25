using CarRental.Domain.DataSeed;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure;

public static class DbInitializer
{
    public static async Task SeedData(CarRentalDbContext context)
    {
        // Проверяем, есть ли данные в базе. Если уже есть хотя бы одна модель — ничего не делаем.
        if (await context.CarModels.AnyAsync()) return;

        var data = new DataSeed();

        // 1. Сначала добавляем базовые модели
        await context.CarModels.AddRangeAsync(data.Models);
        await context.SaveChangesAsync();

        // 2. Затем поколения (они ссылаются на модели)
        await context.ModelGenerations.AddRangeAsync(data.Generations);
        await context.SaveChangesAsync();

        // 3. Машины (ссылаются на поколения)
        await context.Cars.AddRangeAsync(data.Cars);
        await context.SaveChangesAsync();

        // 4. Клиентов
        await context.Clients.AddRangeAsync(data.Clients);
        await context.SaveChangesAsync();

        // 5. И в конце аренду (ссылается на машины и клиентов)
        await context.Rents.AddRangeAsync(data.Rents);
        await context.SaveChangesAsync();
    }
}