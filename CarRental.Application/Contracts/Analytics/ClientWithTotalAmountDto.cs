namespace CarRental.Application.Contracts.Analytics;

/// <summary>
/// Data transfer object for client financial statistics.
/// </summary>
/// <param name="Id">The unique identifier of the client.</param>
/// <param name="FullName">The concatenated full name of the client.</param>
/// <param name="TotalSpentAmount">The sum of all rental costs paid by the client.</param>
/// <param name="TotalRentsCount">Total number of times the client has rented vehicles.</param>
public record ClientWithTotalAmountDto(uint Id, string FullName, decimal TotalSpentAmount, int TotalRentsCount);