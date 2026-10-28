import datetime as dt
from dataclasses import dataclass
from decimal import Decimal
from uuid import UUID


@dataclass(frozen=True)
class Expense:
    id: UUID
    description: str
    amount: Decimal
    category: str
    date: dt.date

    @classmethod
    def from_json(cls, data: dict) -> "Expense":
        return cls(
            id=UUID(data["id"]),
            description=data["description"],
            amount=Decimal(str(data["amount"])),
            category=data["category"],
            date=dt.date.fromisoformat(data["date"]),
        )