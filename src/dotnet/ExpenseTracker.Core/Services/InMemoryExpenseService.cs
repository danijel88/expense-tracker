using System.Collections.Concurrent;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.Services;

public class InMemoryExpenseService : IExpenseService
{
    private readonly ConcurrentDictionary<Guid, Expense> _expenses = new();

    public IReadOnlyList<Expense> GetAll() =>
        _expenses.Values.OrderByDescending(e => e.Date).ToList();

    public Expense? GetById(Guid id) =>
        _expenses.TryGetValue(id, out var expense) ? expense : null;

    public Expense Add(string description, decimal amount, string category, DateOnly date)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");

        var expense = new Expense
        {
            Id = Guid.NewGuid(),
            Description = description,
            Amount = amount,
            Category = category,
            Date = date
        };

        _expenses[expense.Id] = expense;
        return expense;
    }

    public bool Delete(Guid id) => _expenses.TryRemove(id, out _);
}