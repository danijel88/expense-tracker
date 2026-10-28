# ExpenseTracker

![.NET CI](https://github.com/danijel88/expense-tracker/actions/workflows/dotnet.yml/badge.svg)

A small expense tracking system built with **ASP.NET Core** and **Python**.
The .NET Web API stores and serves expenses; the Python analytics tool reads them
from the API and produces summary reports.

The project is also a showcase of a professional development workflow:
feature branches, Conventional Commits, pull requests, automated CI and a protected `main` branch.

## Architecture

```
┌──────────────────────┐   HTTP/JSON    ┌──────────────────────────┐
│  .NET Web API        │ ◀───────────── │  Python analytics (CLI)  │
│  ExpenseTracker.Api  │                │  expense-report          │
└──────────────────────┘                └──────────────────────────┘
```

## Tech stack

| Area      | .NET                         | Python                    |
|-----------|------------------------------|---------------------------|
| Language  | C# / .NET 10                 | Python 3.11+              |
| Framework | ASP.NET Core Web API         | httpx, argparse           |
| Testing   | xUnit                        | pytest (httpx.MockTransport) |
| Quality   | Data annotations validation  | ruff                      |
| CI        | GitHub Actions               | GitHub Actions (planned)  |

## Project structure

```
expense-tracker/
├── .github/workflows/      # CI pipelines
└── src/
    ├── dotnet/
    │   ├── ExpenseTracker.Api/     # Controllers, request contracts, startup
    │   ├── ExpenseTracker.Core/    # Entities and business logic
    │   └── ExpenseTracker.Tests/   # xUnit tests
    └── python/
        ├── analytics/              # API client, reports, CLI
        └── tests/                  # pytest tests
```

## Getting started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Python 3.11 or newer

### Run the API

```bash
cd src/dotnet
dotnet run --project ExpenseTracker.Api
```

The console shows the URL the API listens on (e.g. `http://localhost:5208`).
Sample requests are in `src/dotnet/ExpenseTracker.Api/ExpenseTracker.Api.http`.

### Run the analytics report

In a second terminal:

```bash
cd src/python
python3 -m venv .venv
source .venv/bin/activate
pip install -e ".[dev]"
expense-report --base-url http://localhost:5208
```

Example output:

```
Expenses: 3

By category
  Food                 15.50
  Misc                 10.00
  Transport            15.00

By