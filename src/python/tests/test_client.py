from decimal import Decimal

import httpx
import pytest

from analytics.client import ExpenseClient

SAMPLE = [
    {
        "id": "3f2504e0-4f89-11d3-9a0c-0305e82c3301",
        "description": "Groceries",
        "amount": 42.75,
        "category": "Food",
        "date": "2026-10-28",
    }
]


def test_get_expenses_parses_response():
    def handler(request: httpx.Request) -> httpx.Response:
        assert request.url.path == "/api/expenses"
        return httpx.Response(200, json=SAMPLE)

    client = ExpenseClient("http://test", transport=httpx.MockTransport(handler))

    expenses = client.get_expenses()

    assert len(expenses) == 1
    assert expenses[0].amount == Decimal("42.75")


def test_get_expenses_raises_on_server_error():
    transport = httpx.MockTransport(lambda request: httpx.Response(500))
    client = ExpenseClient("http://test", transport=transport)

    with pytest.raises(httpx.HTTPStatusError):
        client.get_expenses()