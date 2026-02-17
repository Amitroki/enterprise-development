namespace CarRental.Infrastructure.Kafka;

/// <summary>
/// Kafka settings used by CarRental Kafka consumer
/// </summary>
public class ConsumerSettings
{
    /// <summary>
    /// Kafka topic name used for consuming messages
    /// </summary>
    public string TopicName { get; init; } = "car-rentals";

    /// <summary>
    /// Consumer group ID for Kafka
    /// </summary>
    public string GroupId { get; init; } = "car-rental-consumer-group";

    /// <summary>
    /// Enables Kafka auto-commit for the consumer.
    /// If false, the consumer commits offsets manually after successful processing
    /// </summary>
    public bool AutoCommitEnabled { get; init; } = false;

    /// <summary>
    /// Poll timeout for consuming messages in milliseconds
    /// </summary>
    public int ConsumeTimeoutMs { get; init; } = 5000;

    /// <summary>
    /// Maximum number of attempts to deserialize a message payload
    /// </summary>
    public int MaxDeserializeAttempts { get; init; } = 3;
}
