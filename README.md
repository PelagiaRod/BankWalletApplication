# BankWalletApplication

A simple and clean **Bank Wallet API** built with **.NET 8**, **Entity Framework Core**, and **SQLite**.  
This project demonstrates a layered architecture with **Domain**, **Application**, **Infrastructure**, and **API** layers, featuring:

- Wallets (Fiat and Crypto)
- Transactions
- Async database operations
- Repository pattern
- Minimal API endpoints
- Swagger for API testing

---

## Features

- **Fiat and Crypto Wallets** — Manage balances and transactions separately.
- **Entity Framework Core** — Database access with code-first migrations.
- **Async Operations** — Non-blocking I/O for scalable API performance.
- **Minimal API** — Lightweight endpoints using .NET 8.
- **Swagger UI** — Easily test all API endpoints.

---

## Project Structure

BankWalletApplication/
├─ src/
│  ├─ BankingWallet.API         # API project with endpoints
│  ├─ BankingWallet.Application # Application logic, DTOs, interfaces
│  ├─ BankingWallet.Domain      # Entities and domain logic
│  └─ BankingWallet.Infrastructure # EF Core DbContext, Repositories
└─ tests/                       # Unit and integration tests
