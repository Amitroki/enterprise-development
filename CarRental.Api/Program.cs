using CarRental.Domain.DataSeed;
using CarRental.Infrastructure.InMemoryRepository;
using CarRental.Application.Services.CarService;
using CarRental.Application.Interfaces;
using CarRental.Application.Contracts.Car;
using CarRental.Application.Mapping;
using Mapster;
using MapsterMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DataSeed>();
builder.Services.AddScoped<CarRepository>();
builder.Services.AddScoped<CarModelGenerationRepository>();

var config = new TypeAdapterConfig();
MappingConfig.Configure();
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddScoped<IApplicationService<CarDto, CarCreateUpdateDto>, CarService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
