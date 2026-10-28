using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Api.Contracts;

public record CreateExpenseRequest(
    [Required, StringLength(200)] string Description,
    [Range(typeof(decimal), "0.01", "1000000",
        ParseLimitsInInvariantCulture = true,
        ConvertValueInInvariantCulture = true)] decimal Amount,
    [Required, StringLength(50)] string Category,
    DateOnly Date);