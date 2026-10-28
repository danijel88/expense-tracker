import datetime as dt
from decimal import Decimal
from uuid import uuid4

from analytics.models import Expense
from analytics.reports import total_by_category, total_by_month


def make(category: str, amount: str, date: str) -> Expense:
    return Expense(uuid4(), "test", Decimal(amount), category, dt.date.fromisoformat(date))


def test_total_by_category_sums_amounts():
    expenses = [
        make("Food", "10.50", "2026-10-01"),
        make("Food", "4.50", "2026-10-02"),
        make("Transport", "20", "2026-10-03"),
    ]

    assert total_by_category(expenses) == {
        "Food": Decimal("15.00"),
        "Transport": Decimal("20"),
    }


def test_total_by_month_groups_by_year_and_month():
    expenses = [
        make("Food", "10", "2026-09-30"),
        make("Food", "5", "2026-10-01"),
        make("Misc", "7", "2026-10-15"),
    ]

    assert total_by_month(expenses) == {
        "2026-09": Decimal("10"),
        "2026-10": Decimal("12"),
    }


def test_reports_handle_empty_input():
    assert total_by_category([]) == {}
    assert total_by_month([]) == {}