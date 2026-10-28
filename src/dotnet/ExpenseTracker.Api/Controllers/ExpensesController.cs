using ExpenseTracker.Api.Contracts;
using ExpenseTracker.Core.Models;
using ExpenseTracker.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _service;

    public ExpensesController(IExpenseService service) => _service = service;

    [HttpGet]
    public ActionResult<IReadOnlyList<Expense>> GetAll() => Ok(_service.GetAll());

    [HttpGet("{id:guid}")]
    public ActionResult<Expense> GetById(Guid id)
    {
        var expense = _service.GetById(id);
        return expense is null ? NotFound() : Ok(expense);
    }

    [HttpPost]
    public ActionResult<Expense> Create(CreateExpenseRequest request)
    {
        var expense = _service.Add(request.Description, request.Amount, request.Category, request.Date);
        return CreatedAtAction(nameof(GetById), new { id = expense.Id }, expense);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id) =>
        _service.Delete(id) ? NoContent() : NotFound();
}