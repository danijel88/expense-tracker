using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.Services;

public interface IExpenseService
{
    IReadOnlyList<Expense> GetAll();
    Expense? GetById(Guid id);
    Expense Add(string description, decimal amount, string category, DateOnly date);
    bool Delete(Guid id);
}