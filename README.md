\# MO Store



Online store for handmade candles (and, later, succulents).

A learning project built step by step with production practices: Clean Architecture, a rich domain model, EF Core with PostgreSQL, and Docker-based local infrastructure.



\## Tech stack



\- .NET 10 (LTS), ASP.NET Core Minimal APIs

\- EF Core 10 + Npgsql, PostgreSQL 17

\- MongoDB 8, RabbitMQ 4 (planned: notifications service)

\- Docker Compose for local infrastructure



\## Project structure



```

src/

&#x20; MO.Store.Domain/          Entities and business rules. No dependencies.

&#x20; MO.Store.Application/     Use cases (in progress).

&#x20; MO.Store.Infrastructure/  EF Core DbContext, configurations, migrations, dev data seeding.

&#x20; MO.Store.Api/             Minimal API endpoints, composition root.

```



\## Getting started



\### Prerequisites



\- \[.NET SDK 10](https://dotnet.microsoft.com/download)

\- \[Docker Desktop](https://www.docker.com/products/docker-desktop/)

\- EF Core CLI tool:

&#x20; ```powershell

&#x20; dotnet tool install --global dotnet-ef --version "10.\*"

&#x20; ```



\### 1. Configure local secrets



Copy the environment template and set your own passwords:



```powershell

Copy-Item .env.example .env

```



Set the API connection string with User Secrets (use the `POSTGRES\_PASSWORD` value from `.env`):



```powershell

dotnet user-secrets set "ConnectionStrings:StoreDb" "Host=localhost;Port=5432;Database=mo\_store;Username=mo;Password=<your-password>" --project src/MO.Store.Api

```



\### 2. Start the infrastructure



```powershell

docker compose up -d

docker compose ps

```



Wait until `mo-postgres` shows `(healthy)`.



| Service    | Port  | Notes                                   |

| ---------- | ----- | --------------------------------------- |

| PostgreSQL | 5432  | Database `mo\_store`                     |

| MongoDB    | 27017 |                                         |

| RabbitMQ   | 5672  | Management UI: http://localhost:15672   |



\### 3. Apply database migrations



```powershell

dotnet ef database update --project src/MO.Store.Infrastructure --startup-project src/MO.Store.Api

```



\### 4. Run the API



```powershell

dotnet run --project src/MO.Store.Api

```



The API listens on http://localhost:5100.

In the Development environment, sample categories and products are seeded automatically on startup.



\### 5. Try it



\- `GET http://localhost:5100/api/products` returns the active products.

\- Ready-made requests: `src/MO.Store.Api/MO.Store.Api.http` (Visual Studio / VS Code REST Client).

\- OpenAPI document (Development only): http://localhost:5100/openapi/v1.json



\## Roadmap



1\. Full catalog: application layer, admin endpoints, validation, product images

2\. Customers and identity

3\. Orders with a status state machine

4\. Payments and receipts

5\. Notifications microservice (Outbox + RabbitMQ + MongoDB)

6\. Storefront with SSR

7\. AWS deployment with CI/CD

