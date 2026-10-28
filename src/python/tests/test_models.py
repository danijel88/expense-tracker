import datetime as dt
from decimal import Decimal

from analytics.models import Expense


def test_from_json_parses_api_response():
    data = {
        "id": "3f2504e0-4f89-11d3-9a0c-0305e82c3301",
        "description": "Groceries",
        "amount": 42.75,
        "category": "Food",
        "date": "2026-10-28",
    }

    expense = Expense.from_json(data)

    assert expense.amount == Decimal("42.75")
    assert expense.date == dt.date(2026, 10, 28)
    assert expense.category == "Food"