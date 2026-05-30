# MiniPay Platform

## Project Description

MiniPay is a payment management platform built with ASP.NET Core Web API and a simple HTML, CSS, and JavaScript frontend.

The application allows administrators to manage payment providers and simulate payment transactions through a centralized dashboard.

---

## Technologies Used

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* HTML
* CSS
* JavaScript
* Swagger

---

## Features

### Payment Provider Management

* View all payment providers
* Add new payment providers
* Enable/Disable providers
* Store provider information in the database

### Payment Simulation

* Select an active provider
* Enter payment information
* Simulate a payment transaction
* Display transaction results in real time

### API Documentation

* Swagger UI available for testing API endpoints

---

## How to Run the Project

### Prerequisites

* .NET SDK 8.0 or newer
* SQL Server
* Visual Studio Code or Visual Studio

### Steps

1. Clone the repository

```bash
git clone <repository-url>
```

2. Navigate to the API project

```bash
cd MiniPay.API
```

3. Update the connection string in:

```text
appsettings.json
```

4. Apply database migrations

```bash
dotnet ef database update
```

5. Run the project

```bash
dotnet run
```

6. Open the application

```text
http://localhost:5214
```

7. Swagger documentation

```text
http://localhost:5214/swagger
```

---

## Possible Improvements

The following areas could be improved in future versions:

### Authentication & Authorization

* JWT Authentication
* User roles (Admin/User)

### Logging

* Serilog integration
* Error tracking and monitoring

### Validation

* Advanced request validation
* Better error messages

### Payment Integrations

* Real Stripe integration
* Real PayPal integration
* Real Klarna integration

### Transaction History

* Store and display transaction history
* Search and filtering

### Testing

* Unit tests
* Integration tests

### Security

* Rate limiting
* API key management
* Secure secrets management
