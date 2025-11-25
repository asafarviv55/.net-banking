# Bank Headquarters

ASP.NET Core Razor Pages application with Identity for bank management.

## Tech Stack

- .NET 6.0
- ASP.NET Core Identity
- Entity Framework Core
- SQL Server

## Prerequisites

- .NET 6.0 SDK
- SQL Server

## Setup

1. Copy `appsettings.Example.json` to `appsettings.Development.json`
2. Update connection string

```bash
cd BankHeadQuarters
dotnet restore
dotnet ef database update
dotnet run
```

## Configuration

| Setting | Description |
|---------|-------------|
| `ConnectionStrings:BankContext` | SQL Server connection string |
