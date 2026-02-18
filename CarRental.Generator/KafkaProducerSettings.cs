namespace CarRental.Generator;

/// <summary>
/// Kafka settings used by the CarRental Kafka producer host
/// </summary>
public class KafkaProducerSettings
{
    /// <summary>
    /// Kafka topic name used for producing messages
    /// </summary>
    public string TopicName { get; init; } = "car-rentals";

    /// <summary>
    /// Maximum number of attempts to send a message
    /// </summary>
    public int MaxProduceAttempts { get; init; } = 5;

    /// <summary>
    /// Delay between produce retries in milliseconds
    /// </summary>
    public int RetryDelayMs { get; init; } = 1000;

    /// <summary>
    /// Maximum number of parallel produce operations
    /// </summary>
    public int MaxParallelism { get; init; } = 10;
}