using CarRental.Generator;
using CarRental.Generator.Generation;
using CarRental.ServiceDefaults;
using Confluent.Kafka;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<GeneratorOptions>(builder.Configuration.GetSection("Generator"));

builder.AddServiceDefaults();

builder.Services.AddSingleton<RentGeneratorService>();
builder.Services.AddSingleton(sp =>
{
    var cfg = sp.GetRequiredService<IConfiguration>();
    var bootstrapServers = cfg.GetConnectionString("car-rental-kafka");
    if (string.IsNullOrWhiteSpace(bootstrapServers))
        throw new InvalidOperationException("Kafka connection string 'car-rental-kafka' is missing.");
    var producerConfig = new ProducerConfig
    {
        BootstrapServers = bootstrapServers,
        Acks = Acks.All
    };

    return new ProducerBuilder<Null, string>(producerConfig).Build();
});

builder.Services.AddSingleton<KafkaProducer>();
builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();