using CarRental.Infrastructure.Kafka;
using Confluent.Kafka;

namespace CarRental.Api;

/// <summary>
/// Extension methods for registering generator service client in DI container
/// </summary>
internal static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Registers Kafka consumer client for interacting with the data generator service
    /// </summary>
    /// <param name="builder">Web application builder</param>
    /// <returns>Web application builder with registered Kafka services</returns>
    public static WebApplicationBuilder AddGeneratorService(this WebApplicationBuilder builder)
    {
        builder.Services.AddHostedService<Consumer>();

        builder.AddKafkaConsumer<Ignore, string>(
            "car-rental-kafka",
            configureSettings: settings =>
            {
                settings.Config.GroupId = "car-rental-consumer-group";
                settings.Config.AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Earliest;
                settings.Config.EnableAutoCommit = false;
            }
        );

        return builder;
    }
}