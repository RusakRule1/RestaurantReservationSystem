# La Maison — Restaurant Reservation System

A web-based table reservation system for La Maison restaurant. Guests can reserve a table through a public form, and staff can manage reservations through an admin dashboard.

## Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) 29.1.3 or later
- Docker Compose v2.40.3 or later (included with Docker Desktop)

## Running the Application

**First run:**

```bash
docker-compose up --build
```

**Subsequent runs:**

```bash
docker-compose up
```

The application will be available at **http://localhost:8080**

On startup, the database is automatically migrated and seeded with sample reservation data. No additional commands are required.

## Accessing the Application

| Page                    | URL                         |
| ----------------------- | --------------------------- |
| Public reservation form | http://localhost:8080       |
| Admin dashboard         | http://localhost:8080/admin |

## Project Structure

```
LaMaison/
  LaMaison.Core/             # Domain models, business rules
    Entities/                # Reservation entity
    Enums/                   # ReservationStatus
    Rules/                   # PrivateDiningRules, RegularDiningRules
  LaMaison.Infrastructure/   # Data access, service implementations
    Data/                    # ApplicationDbContext, DatabaseSeeder
    Services/                # ReservationService, IReservationService
  LaMaison.Web/              # Razor Pages, UI
    Pages/                   # Index, Confirmation, Admin/Index, Admin/Details
    Models/                  # ReservationInput
    wwwroot/                 # CSS, JavaScript
  LaMaison.Tests/            # xUnit unit tests
```

## Assumptions and Design Decisions

### Validation

- **Phone number**: E.164 international format required (e.g. `+385911234567`). Chosen for data consistency — the input placeholder guides the user.
- **Full name**: Minimum two words (first and last name), letters only. Automatically formatted to Title Case on save (e.g. `ivan horvat` → `Ivan Horvat`).
- **Email**: Custom regex requiring a domain with at least a dot and two characters after it — stricter than the default ASP.NET `[EmailAddress]` attribute which accepts `user@domain`.
- **Date range**: Only future dates within the next 30 days are selectable, as specified.

### Capacity Rules

- Regular dining: maximum 20 guests per time slot across all reservations combined.
- Private dining: maximum 1 reservation per time slot. Private dining capacity is completely separate from regular dining — a private and a regular reservation in the same time slot do not affect each other.
- Cancelled and completed reservations do not count towards capacity.

### Status Flow

```
Pending → Confirmed → Completed
        → Cancelled
```

New reservations start as `Pending`. Status changes are managed from the admin detail view.

### Reservation Reference Code

Reference codes follow the format `LM-XXXXX` where X is an alphanumeric character, generated from a random GUID segment.

### Real-time Updates

The admin dashboard fetches fresh data on every page interaction (filter, sort, navigation). There is no automatic real-time push — a deliberate decision given the project scope. Refreshing the page will always show the latest data.

### Architecture

The solution follows a layered architecture:

- **Core** — no external dependencies, pure domain logic and interfaces
- **Infrastructure** — depends only on Core, handles database and service implementations
- **Web** — depends on Core and Infrastructure, handles HTTP and UI

This separation keeps business logic independently testable without a database or HTTP context.

## Tests

6 unit tests covering the most critical business logic:

- Regular slot capacity enforcement (20 guest maximum per slot)
- Slot availability when capacity is not yet reached
- Cancelled reservations excluded from capacity calculations
- Private dining restricted to Fridays and Saturdays only
- Private dining slot exclusivity (one booking per slot)
- Private dining time window restricted to 18:00–22:00

Run tests:

```bash
dotnet test
```

## Known Limitations

- No authentication on the admin dashboard — as specified, authentication was out of scope.
- The confirmation page shows the reservation as "Pending Confirmation" since staff must manually confirm through the admin dashboard.
- Mobile responsiveness is handled by Bootstrap 5 with minor custom CSS adjustments.
