namespace ExpenseTracker.Core.Models;
 
public class Expense
{
    public Guid Id { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public string Category { get; init; } = string.Empty;
    public DateOnly Date { get; init; }
}