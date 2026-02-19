using CarRental.Generator;
using CarRental.Generator.Generation;
using CarRental.ServiceDefaults;
using Confluent.Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<GeneratorOptions>(builder.Configuration.GetSection("Generator"));

builder.AddServiceDefaults();

builder.AddKafkaProducer<Null, string>("car-rental-kafka");

builder.Services.AddSingleton<RentGeneratorService>();
builder.Services.AddSingleton<KafkaProducer>();

builder.Services.AddControllers();
builder.Services.AddAuthorization();

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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();