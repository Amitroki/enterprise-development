using Confluent.Kafka;
using CarRental.Application.Contracts.Rent;
using CarRental.Application.Contracts.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CarRental.Infrastructure.Kafka;

/// <summary>
/// Background service that consumes rent messages from Kafka and saves them to database
/// </summary>
/// <param name="logger">Logger for recording operations</param>
/// <param name="consumer">Kafka consumer instance</param>
/// <param name="scopeFactory">Factory for creating service scopes</param>
/// <param name="options">Consumer configuration settings</param>
public class Consumer(
    ILogger<Consumer> logger,
    IConsumer<Ignore, string> consumer,
    IServiceScopeFactory scopeFactory,
    IOptions<ConsumerSettings> options) : BackgroundService
{
    private readonly ConsumerSettings _settings = options.Value;

    /// <summary>
    /// Main execution loop that continuously consumes and processes messages from Kafka
    /// </summary>
    /// <param name="stoppingToken">Cancellation token to stop the consumer</param>
    /// <returns>Task representing the asynchronous operation</returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            consumer.Subscribe(_settings.TopicName);
            logger.LogInformation("KafkaConsumer started on topic {TopicName}", _settings.TopicName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to subscribe to topic {TopicName}", _settings.TopicName);
            return;
        }

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                ConsumeResult<Ignore, string>? message = null;

                try
                {
                    message = consumer.Consume(TimeSpan.FromMilliseconds(_settings.ConsumeTimeoutMs));

                    if (message is null)
                        continue;

                    var payload = message.Message?.Value;

                    if (string.IsNullOrWhiteSpace(payload))
                    {
                        logger.LogWarning("Empty payload. Topic={Topic}, Offset={Offset}",
                            message.Topic, message.Offset.Value);
                        CommitIfNeeded(message);
                        continue;
                    }

                    RentCreateUpdateDto? dto = null;

                    for (var attempt = 1; attempt <= _settings.MaxDeserializeAttempts; attempt++)
                    {
                        try
                        {
                            dto = JsonSerializer.Deserialize<RentCreateUpdateDto>(payload);
                            break;
                        }
                        catch (Exception ex)
                        {
                            logger.LogWarning(ex, "Deserialization attempt {Attempt} failed", attempt);
                        }
                    }

                    if (dto == null)
                    {
                        logger.LogError("Failed to deserialize after {MaxAttempts} attempts",
                            _settings.MaxDeserializeAttempts);
                        CommitIfNeeded(message);
                        continue;
                    }

                    using var scope = scopeFactory.CreateScope();
                    var rentService = scope.ServiceProvider.GetRequiredService<IApplicationService<RentDto, RentCreateUpdateDto, Guid>>();

                    var savedRent = await rentService.Create(dto);

                    CommitIfNeeded(message);
                    logger.LogInformation("Saved rent with Id: {RentId} from Kafka", savedRent.Id);
                }
                catch (ConsumeException ex) when (ex.Error.Code == ErrorCode.UnknownTopicOrPart)
                {
                    logger.LogWarning("Topic not available, retrying...");
                    await Task.Delay(1000, stoppingToken);
                }
                catch (ConsumeException ex)
                {
                    logger.LogError(ex, "Consume error: {Reason}", ex.Error.Reason);
                }
                catch (OperationCanceledException)
                {
                    logger.LogInformation("KafkaConsumer stopping");
                    break;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Unexpected error");
                }
            }
        }
        finally
        {
            try
            {
                consumer.Unsubscribe();
                consumer.Close();
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Error closing consumer");
            }

            logger.LogInformation("KafkaConsumer stopped");
        }
    }

    /// <summary>
    /// Commits the message offset if auto-commit is disabled
    /// </summary>
    /// <param name="message">The consumed message to commit</param>
    private void CommitIfNeeded(ConsumeResult<Ignore, string> message)
    {
        if (_settings.AutoCommitEnabled)
            return;
        try
        {
            consumer.Commit(message);
        }
        catch (KafkaException ex)
        {
            logger.LogWarning(ex, "Commit failed. Offset={Offset}", message.Offset.Value);
        }
    }
}