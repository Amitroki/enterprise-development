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

builder.AddServiceDefaults();
builder.AddMongoDBClient("CarRentalDb");

builder.Services.AddDbContext<CarRentalDbContext>((serviceProvider, options) =>
{
    var db = serviceProvider.GetRequiredService<IMongoDatabase>();
    options.UseMongoDB(db.Client, db.DatabaseNamespace.DatabaseName);
});

builder.Services.AddSingleton(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("car-rental");
});

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new CarRentalProfile());
});

builder.Services.AddSingleton<DataSeed>();

builder.Services.AddScoped<IBaseRepository<CarModel, Guid>, DbCarModelRepository>();
builder.Services.AddScoped<IBaseRepository<CarModelGeneration, Guid>, DbCarModelGenerationRepository>();
builder.Services.AddScoped<IBaseRepository<Car, Guid>, DbCarRepository>();
builder.Services.AddScoped<IBaseRepository<Client, Guid>, DbClientRepository>();
builder.Services.AddScoped<IBaseRepository<Rent, Guid>, DbRentRepository>();

builder.Services.AddScoped<DbCarRepository>();
builder.Services.AddScoped<DbClientRepository>();
builder.Services.AddScoped<DbRentRepository>();

builder.Services.AddScoped<IApplicationService<CarDto, CarCreateUpdateDto, Guid>, CarService>();
builder.Services.AddScoped<IApplicationService<ClientDto, ClientCreateUpdateDto, Guid>, ClientService>();
builder.Services.AddScoped<IApplicationService<RentDto, RentCreateUpdateDto, Guid>, RentService>();
builder.Services.AddScoped<IApplicationService<CarModelDto, CarModelCreateUpdateDto, Guid>, CarModelService>();
builder.Services.AddScoped<IApplicationService<CarModelGenerationDto, CarModelGenerationCreateUpdateDto, Guid>, CarModelGenerationService>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CarRentalDbContext>();
        var dataseed = services.GetRequiredService<DataSeed>();

        if (await context.CarModels.AnyAsync()) return;

        await context.CarModels.AddRangeAsync(dataseed.Models);
        await context.SaveChangesAsync();

        await context.ModelGenerations.AddRangeAsync(dataseed.Generations);
        await context.SaveChangesAsync();

        await context.Cars.AddRangeAsync(dataseed.Cars);
        await context.SaveChangesAsync();

        await context.Clients.AddRangeAsync(dataseed.Clients);
        await context.SaveChangesAsync();

        await context.Rents.AddRangeAsync(dataseed.Rents);
        await context.SaveChangesAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();