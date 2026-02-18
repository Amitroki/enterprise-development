using Confluent.Kafka;
using Microsoft.Extensions.Options;
using CarRental.Application.Contracts.Rent;
using System.Text.Json;

namespace CarRental.Generator;

/// <summary>
/// Kafka producer that serializes <see cref="RentCreateUpdateDto"/> into JSON
/// and publishes messages to a configured topic
/// </summary>
/// <param name="logger">Logger instance</param>
/// <param name="producer">Kafka producer</param>
/// <param name="options">Producer settings</param>
public class KafkaProducer(
    ILogger<KafkaProducer> logger,
    IProducer<Null, string> producer,
    IOptions<KafkaProducerSettings> options)
{
    private readonly KafkaProducerSettings _settings = options.Value
        ?? throw new InvalidOperationException("KafkaProducerSettings must be configured.");

    /// <summary>
    /// Sends a rent DTO as a JSON message to Kafka
    /// </summary>
    /// <param name="dto">Rent DTO to send</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task Produce(RentCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_settings.TopicName))
            throw new InvalidOperationException("KafkaProducerSettings.TopicName must be configured.");

        var payload = JsonSerializer.Serialize(dto);

        for (var attempt = 1; attempt <= _settings.MaxProduceAttempts; attempt++)
        {
            try
            {
                var result = await producer.ProduceAsync(_settings.TopicName, new Message<Null, string> { Value = payload }, cancellationToken);

                logger.LogInformation(
                    "Kafka message produced successfully. Topic={Topic}, Partition={Partition}, Offset={Offset}, " +
                    "CarId={CarId}, ClientId={ClientId}, StartDateTime={StartDateTime}, Duration={Duration}",
                    result.Topic, result.Partition.Value, result.Offset.Value,
                    dto.CarId, dto.ClientId, dto.StartDateTime, dto.Duration);

                return;
            }
            catch (ProduceException<Null, string> ex) when (attempt < _settings.MaxProduceAttempts)
            {
                logger.LogWarning(ex,
                    "Kafka produce attempt {Attempt}/{MaxAttempts} failed. Reason={Reason}. Retrying in {Delay}ms...",
                    attempt, _settings.MaxProduceAttempts, ex.Error.Reason, _settings.RetryDelayMs);

                await Task.Delay(_settings.RetryDelayMs, cancellationToken);
            }
            catch (ProduceException<Null, string> ex)
            {
                logger.LogError(ex,
                    "Kafka produce failed after {MaxAttempts} attempts. Reason={Reason}. CarId={CarId}, ClientId={ClientId}",
                    _settings.MaxProduceAttempts, ex.Error.Reason, dto.CarId, dto.ClientId);

                throw;
            }
        }
    }

    /// <summary>
    /// Sends a batch of rent DTOs as JSON messages to Kafka with controlled parallelism
    /// </summary>
    /// <param name="dtos">Rent DTOs to send</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task ProduceMany(IList<RentCreateUpdateDto> dtos, CancellationToken cancellationToken = default)
    {
        if (!dtos.Any())
        {
            logger.LogWarning("No rent DTOs to produce to Kafka.");
            return;
        }

        logger.LogInformation("Starting to produce {Count} rent messages to Kafka topic: {Topic} with parallelism {MaxParallelism}",
            dtos.Count, _settings.TopicName, _settings.MaxParallelism);

        using var semaphore = new SemaphoreSlim(_settings.MaxParallelism);
        var tasks = new List<Task>();

        foreach (var dto in dtos)
        {
            await semaphore.WaitAsync(cancellationToken);

            tasks.Add(Task.Run(async () =>
            {
                try
                {
                    await Produce(dto, cancellationToken);
                }
                finally
                {
                    semaphore.Release();
                }
            }, cancellationToken));
        }

        await Task.WhenAll(tasks);

        logger.LogInformation("Successfully produced all {Count} rent messages to Kafka", dtos.Count);
    }
}