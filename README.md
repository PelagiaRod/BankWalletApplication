# BankWalletApplication

A simple and clean **Bank Wallet API** built with **.NET 10**, **Entity Framework Core**, and **SQLite**.  
This project demonstrates a **Domain-Driven Design (DDD)** architecture with **Domain**, **Application**, **Infrastructure**, and **API** layers, featuring:

- Wallets (Fiat and Crypto)
- Transactions
- JWT Authentication & Authorization
- Refresh Token with revocation
- Async database operations
- Repository pattern
- Minimal API endpoints
- Swagger for API testing

---

## Features

- **Fiat and Crypto Wallets** — Manage balances and transactions separately.
- **JWT Authentication** — Secure endpoints with short-lived access tokens (15 min).
- **Refresh Token Rotation** — Long-lived refresh tokens stored and revoked in the database.
- **Role-based Authorization** — Protect endpoints based on user roles.
- **Entity Framework Core** — Database access with code-first migrations.
- **Async Operations** — Non-blocking I/O for scalable API performance.
- **Minimal API** — Lightweight endpoints using .NET 10.
- **Swagger UI** — Easily test all API endpoints including authenticated ones.

---

## Project Structure

```
BankWalletApplication/
├─ src/
│  ├─ BankingWallet.API                  # Minimal API endpoints, middleware
│  │  ├─ Endpoints/
│  │  │  ├─ AuthEndpoints.cs             # /auth/register, /login, /logout
│  │  │  └─ WalletEndpoints.cs           # /wallet (protected)
│  │  ├─ Extensions/
│  │  │  └─ ClaimsPrincipalExtensions.cs # JWT claims helpers
│  │  └─ ErrorHandling/
│  │     └─ ErrorHandlingMiddleware.cs   # Global error handling
│  │
│  ├─ BankingWallet.Application          # Application services, DTOs, interfaces
│  │  ├─ Auth/
│  │  │  ├─ Services/                    # IAuthService, IJwtTokenService, IPasswordHasher
│  │  │  └─ DTOs/                        # LoginRequest, RegisterRequest, AuthResponse
│  │  └─ Wallet/
│  │     ├─ Services/                    # IWalletAppService, IWalletTransferService
│  │     └─ DTOs/                        # CreateWalletRequest, TransferRequest
│  │
│  ├─ BankingWallet.Domain               # Entities, repository interfaces, exceptions
│  │  ├─ Auth/
│  │  │  ├─ Entities/                    # User, RefreshToken
│  │  │  ├─ Repositories/               # IUserRepository, IRefreshTokenRepository
│  │  │  └─ Exceptions/                 # UnauthorizedException
│  │  ├─ Wallet/
│  │  │  ├─ Entities/                    # Wallet, FiatWallet, CryptoWallet, Transaction
│  │  │  ├─ Repositories/               # IWalletRepository
│  │  │  └─ ValueObjects/               # Money, Currency
│  │  └─ Common/
│  │     └─ Entity.cs                    # Base entity with Id
│  │
│  └─ BankingWallet.Infrastructure       # EF Core, repositories, services
│     ├─ Auth/
│     │  ├─ Repositories/               # UserRepository, RefreshTokenRepository
│     │  └─ Services/                   # JwtTokenService, PasswordHasher
│     ├─ Wallet/
│     │  └─ Repositories/               # WalletRepository
│     └─ Persistence/
│        ├─ BankingWalletDbContext.cs    # EF Core DbContext
│        └─ Migrations/                 # EF Core migrations
│
└─ tests/                               # Unit and integration tests
```

---

## Authentication Flow

```
1. POST /auth/register      → create account → receive AccessToken + RefreshToken
2. POST /auth/login         → login          → receive AccessToken + RefreshToken
3. GET  /wallet             → send AccessToken in Authorization header
4. POST /auth/refresh-token → exchange RefreshToken → receive new tokens
5. POST /auth/logout        → revoke RefreshToken in database
```

---

## Getting Started

### Prerequisites
- .NET 10 SDK
- SQLite

### Run the API

```bash
git clone https://github.com/your-repo/BankWalletApplication
cd BankWalletApplication/src/BankingWallet.API
dotnet restore
dotnet ef database update --project ../BankingWallet.Infrastructure
dotnet run
```

### Environment Variables

Add to `appsettings.json` or use `dotnet user-secrets`:

```json
"Jwt": {
    "Key": "your-super-secret-key-minimum-32-characters",
    "Issuer": "BankingWallet",
    "Audience": "BankingWallet"
}
```

### Testing with Swagger
1. Navigate to `http://localhost:5000/swagger`
2. Call `POST /auth/register` or `POST /auth/login`
3. Copy the `accessToken` from the response
4. Click the 🔒 **Authorize** button and enter `Bearer <your_token>`
5. Test protected wallet endpoints

---

## Tech Stack

| Layer | Technology |
|---|---|
| API | ASP.NET Core Minimal API (.NET 10) |
| Authentication | JWT Bearer + Refresh Tokens |
| ORM | Entity Framework Core 7 |
| Database | SQLite |
| Documentation | Swagger / OpenAPI |
| Architecture | DDD (Domain-Driven Design) |