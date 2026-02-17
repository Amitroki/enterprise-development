using Microsoft.AspNetCore.Mvc;
using CarRental.Generator.Generation;
using CarRental.Application.Contracts.Rent;

namespace CarRental.Generator.Controller;

/// <summary>
/// Controller for generating and publishing rental data to Kafka
/// </summary>
/// <param name="producer">Kafka producer for sending messages</param>
/// <param name="rentGenerator">Service for generating rental contracts</param>
/// <param name="logger">Logger instance</param>
[ApiController]
[Route("api/[controller]")]
public class GeneratorController(
    KafkaProducer producer,
    RentGeneratorService rentGenerator,
    ILogger<GeneratorController> logger) : ControllerBase
{
    /// <summary>
    /// Generates and publishes rental contracts to Kafka in batches
    /// </summary>
    /// <param name="totalCount">Total number of rentals to generate</param>
    /// <param name="batchSize">Number of rentals per batch</param>
    /// <param name="delayMs">Delay between batches in milliseconds</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result with generation statistics</returns>
    [HttpPost("rentals")]
    public async Task<ActionResult> GenerateRentals(
        [FromQuery] int totalCount,
        [FromQuery] int batchSize,
        [FromQuery] int delayMs,
        CancellationToken cancellationToken)
    {
        if (totalCount <= 0)
            return BadRequest("totalCount must be greater than 0.");

        if (batchSize <= 0)
            return BadRequest("batchSize must be greater than 0.");

        if (delayMs < 0)
            return BadRequest("delayMs must be greater than or equal to 0.");

        logger.LogInformation("Rental generation requested. TotalCount={TotalCount}, BatchSize={BatchSize}, DelayMs={DelayMs}",
            totalCount, batchSize, delayMs);

        var sent = 0;
        var batches = 0;

        try
        {
            while (sent < totalCount && !cancellationToken.IsCancellationRequested)
            {
                var remaining = totalCount - sent;
                var currentBatchSize = Math.Min(batchSize, remaining);

                IList<RentCreateUpdateDto> batch = rentGenerator.GenerateContract(currentBatchSize);

                await producer.ProduceMany(batch, cancellationToken);

                sent += currentBatchSize;
                batches++;

                if (sent < totalCount && delayMs > 0)
                {
                    await Task.Delay(delayMs, cancellationToken);
                }
            }

            logger.LogInformation("Generation finished. TotalSent={TotalSent}, Batches={Batches}", sent, batches);

            return Ok(new
            {
                TotalRequested = totalCount,
                TotalSent = sent,
                BatchSize = batchSize,
                DelayMs = delayMs,
                Batches = batches,
                Canceled = false
            });
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Generation was canceled. TotalSent={TotalSent}/{TotalCount}", sent, totalCount);

            return Ok(new
            {
                TotalRequested = totalCount,
                TotalSent = sent,
                BatchSize = batchSize,
                DelayMs = delayMs,
                Batches = batches,
                Canceled = true
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error during generation/publishing. TotalSent={TotalSent}/{TotalCount}", sent, totalCount);
            return StatusCode(500, "An error occurred while generating and sending rentals");
        }
    }
}