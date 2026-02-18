using Microsoft.AspNetCore.Mvc;
using CarRental.Generator.Generation;
using CarRental.Application.Contracts.Rent;
using CarRental.Application.Contracts.Generator;

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
    /// <param name="request">Generation parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result with generation statistics</returns>
    [HttpPost("rentals")]
    public async Task<ActionResult<GenerateRentalsResponse>> GenerateRentals(
        [FromQuery] GenerateRentalsRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        logger.LogInformation("Rental generation requested. TotalCount={TotalCount}, BatchSize={BatchSize}, DelayMs={DelayMs}",
            request.TotalCount, request.BatchSize, request.DelayMs);

        var sent = 0;
        var batches = 0;

        try
        {
            while (sent < request.TotalCount && !cancellationToken.IsCancellationRequested)
            {
                var remaining = request.TotalCount - sent;
                var currentBatchSize = Math.Min(request.BatchSize, remaining);

                IList<RentCreateUpdateDto> batch = rentGenerator.GenerateContract(currentBatchSize);

                await producer.ProduceMany(batch, cancellationToken);

                sent += currentBatchSize;
                batches++;

                if (sent < request.TotalCount && request.DelayMs > 0)
                {
                    await Task.Delay(request.DelayMs, cancellationToken);
                }
            }

            logger.LogInformation("Generation finished. TotalSent={TotalSent}, Batches={Batches}", sent, batches);

            return Ok(new GenerateRentalsResponse
            {
                TotalRequested = request.TotalCount,
                TotalSent = sent,
                BatchSize = request.BatchSize,
                DelayMs = request.DelayMs,
                Batches = batches,
                Canceled = false
            });
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Generation was canceled. TotalSent={TotalSent}/{TotalCount}", sent, request.TotalCount);

            return Ok(new GenerateRentalsResponse
            {
                TotalRequested = request.TotalCount,
                TotalSent = sent,
                BatchSize = request.BatchSize,
                DelayMs = request.DelayMs,
                Batches = batches,
                Canceled = true
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error during generation/publishing. TotalSent={TotalSent}/{TotalCount}", sent, request.TotalCount);
            return StatusCode(500, "An error occurred while generating and sending rentals");
        }
    }
}