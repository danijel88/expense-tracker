import httpx

from analytics.models import Expense


class ExpenseClient:
    def __init__(self, base_url: str, transport: httpx.BaseTransport | None = None):
        self._http = httpx.Client(base_url=base_url, transport=transport, timeout=10.0)

    def get_expenses(self) -> list[Expense]:
        response = self._http.get("/api/expenses")
        response.raise_for_status()
        return [Expense.from_json(item) for item in response.json()]