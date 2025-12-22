using CarRental.Application.Contracts.Car;
using CarRental.Application.Contracts.Client;
using CarRental.Application.Contracts.Rent;
using CarRental.Application.Interfaces;
using CarRental.Application.Mapping;
using CarRental.Application.Services;
using CarRental.Domain.DataModels;
using CarRental.Domain.Interfaces;
using CarRental.Domain.InternalData.ComponentClasses;
using CarRental.Infrastructure.InMemoryRepository;
using Mapster;
using MapsterMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(TypeAdapterConfig.GlobalSettings);
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddSingleton<CarRental.Domain.DataSeed.DataSeed>();
builder.Services.AddSingleton<IBaseRepository<CarModel>, CarModelRepository>();
builder.Services.AddSingleton<IBaseRepository<CarModelGeneration>, CarModelGenerationRepository>();
builder.Services.AddSingleton<IBaseRepository<Car>, CarRepository>();
builder.Services.AddSingleton<IBaseRepository<Client>, ClientRepository>();
builder.Services.AddSingleton<IBaseRepository<Rent>, RentRepository>();

builder.Services.AddScoped<IApplicationService<CarDto, CarCreateUpdateDto>, CarService>();
builder.Services.AddScoped<IApplicationService<ClientDto, ClientCreateUpdateDto>, ClientService>();
builder.Services.AddScoped<IApplicationService<RentDto, RentCreateUpdateDto>, RentService>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

MappingConfig.Configure();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection(); // Можно включить, если настроены сертификаты

app.UseAuthorization();
app.MapControllers();

app.Run();