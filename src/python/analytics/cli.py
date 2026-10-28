import argparse
from decimal import Decimal

from analytics.client import ExpenseClient
from analytics.reports import total_by_category, total_by_month


def main(argv: list[str] | None = None) -> None:
    parser = argparse.ArgumentParser(description="Summarize expenses from the ExpenseTracker API.")
    parser.add_argument("--base-url", default="http://localhost:5208")
    args = parser.parse_args(argv)

    expenses = ExpenseClient(args.base_url).get_expenses()

    print(f"Expenses: {len(expenses)}")
    _print_section("By category", total_by_category(expenses))
    _print_section("By month", total_by_month(expenses))


def _print_section(title: str, totals: dict[str, Decimal]) -> None:
    print(f"\n{title}")
    for key, total in totals.items():
        print(f"  {key:<15} {total:>10.2f}")


if __name__ == "__main__":
    main()