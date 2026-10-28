from collections import defaultdict
from collections.abc import Iterable
from decimal import Decimal

from analytics.models import Expense


def total_by_category(expenses: Iterable[Expense]) -> dict[str, Decimal]:
    totals: defaultdict[str, Decimal] = defaultdict(Decimal)
    for expense in expenses:
        totals[expense.category] += expense.amount
    return dict(sorted(totals.items()))


def total_by_month(expenses: Iterable[Expense]) -> dict[str, Decimal]:
    totals: defaultdict[str, Decimal] = defaultdict(Decimal)
    for expense in expenses:
        totals[f"{expense.date:%Y-%m}"] += expense.amount
    return dict(sorted(totals.items()))