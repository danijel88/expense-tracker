using ExpenseTracker.Core.Services;

namespace ExpenseTracker.Tests;

public class InMemoryExpenseServiceTests
{
    private readonly InMemoryExpenseService _service = new();

    [Fact]
    public void Add_AssignsIdAndStoresExpense()
    {
        var expense = _service.Add("Coffe",3.50m,"Food",new DateOnly(2026,10,1));
        Assert.NotEqual(Guid.Empty, expense.Id);
        Assert.Equal(expense, _service.GetById(expense.Id));
    }
     [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void Add_Throws_WhenAmountIsNotPositive(decimal amount)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _service.Add("Invalid", amount, "Food", new DateOnly(2026, 10, 1)));
    }

    [Fact]
    public void GetById_ReturnsNull_WhenNotFound()
    {
        Assert.Null(_service.GetById(Guid.NewGuid()));
    }

    [Fact]
    public void GetAll_ReturnsNewestFirst()
    {
        _service.Add("Older", 10m, "Misc", new DateOnly(2026, 1, 1));
        _service.Add("Newer", 20m, "Misc", new DateOnly(2026, 6, 1));

        var all = _service.GetAll();

        Assert.Equal(["Newer", "Older"], all.Select(e => e.Description));
    }

    [Fact]
    public void Delete_RemovesExpense()
    {
        var expense = _service.Add("Taxi", 15m, "Transport", new DateOnly(2026, 10, 2));

        Assert.True(_service.Delete(expense.Id));
        Assert.Null(_service.GetById(expense.Id));
    }

    [Fact]
    public void Delete_ReturnsFalse_WhenNotFound()
    {
        Assert.False(_service.Delete(Guid.NewGuid()));
    }
}