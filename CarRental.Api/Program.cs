using CarRental.Application;
using CarRental.Application.Contracts.Car;
using CarRental.Application.Contracts.CarModel;
using CarRental.Application.Contracts.CarModelGeneration;
using CarRental.Application.Contracts.Client;
using CarRental.Application.Contracts.Rent;
using CarRental.Application.Interfaces;
using CarRental.Application.Services;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;
using CarRental.Domain.DataSeed;
using CarRental.Domain.InternalData.ComponentClasses;
using CarRental.Infrastructure;
using CarRental.Infrastructure.Repository;
using CarRental.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Подключение инфраструктуры Aspire и MongoDB ---
builder.AddServiceDefaults();
builder.AddMongoDBClient("CarRentalDb");

builder.Services.AddDbContext<CarRentalDbContext>((serviceProvider, options) =>
{
    var db = serviceProvider.GetRequiredService<IMongoDatabase>();
    options.UseMongoDB(db.Client, db.DatabaseNamespace.DatabaseName);
});

// --- 2. Регистрация AutoMapper (ВМЕСТО Mapster) ---
// Находит CarRentalProfile и регистрирует IMapper в DI
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new CarRentalProfile());
});

builder.Services.AddSingleton<DataSeed>();

// --- 3. Регистрация РЕПОЗИТОРИЕВ MONGODB ---
builder.Services.AddScoped<IBaseRepository<CarModel>, DbCarModelRepository>();
builder.Services.AddScoped<IBaseRepository<CarModelGeneration>, DbCarModelGenerationRepository>();
builder.Services.AddScoped<IBaseRepository<Car>, DbCarRepository>();
builder.Services.AddScoped<IBaseRepository<Client>, DbClientRepository>();
builder.Services.AddScoped<IBaseRepository<Rent>, DbRentRepository>();

// Дополнительная регистрация конкретных типов для AnalyticsService (если требуется)
builder.Services.AddScoped<DbCarRepository>();
builder.Services.AddScoped<DbClientRepository>();
builder.Services.AddScoped<DbRentRepository>();

// --- 4. Регистрация прикладных сервисов ---
builder.Services.AddScoped<IApplicationService<CarDto, CarCreateUpdateDto>, CarService>();
builder.Services.AddScoped<IApplicationService<ClientDto, ClientCreateUpdateDto>, ClientService>();
builder.Services.AddScoped<IApplicationService<RentDto, RentCreateUpdateDto>, RentService>();
builder.Services.AddScoped<IApplicationService<CarModelDto, CarModelCreateUpdateDto>, CarModelService>();
builder.Services.AddScoped<IApplicationService<CarModelGenerationDto, CarModelGenerationCreateUpdateDto>, CarModelGenerationService>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

// --- 5. Настройка контроллеров и Swagger ---
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => a.GetName().Name!.StartsWith("CarRental"))
        .Distinct();

    foreach (var assembly in assemblies)
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
            c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

app.MapDefaultEndpoints();

// --- 6. Инициализация Базы Данных (Seed) ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CarRentalDbContext>();
        var dataseed = services.GetRequiredService<DataSeed>();

        if (await context.CarModels.AnyAsync()) return;

        // 1. Сначала добавляем базовые модели
        await context.CarModels.AddRangeAsync(dataseed.Models);
        await context.SaveChangesAsync();

        // 2. Затем поколения (они ссылаются на модели)
        await context.ModelGenerations.AddRangeAsync(dataseed.Generations);
        await context.SaveChangesAsync();

        // 3. Машины (ссылаются на поколения)
        await context.Cars.AddRangeAsync(dataseed.Cars);
        await context.SaveChangesAsync();

        // 4. Клиентов
        await context.Clients.AddRangeAsync(dataseed.Clients);
        await context.SaveChangesAsync();

        // 5. И в конце аренду (ссылается на машины и клиентов)
        await context.Rents.AddRangeAsync(dataseed.Rents);
        await context.SaveChangesAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// --- 7. Конфигурация Pipeline ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();