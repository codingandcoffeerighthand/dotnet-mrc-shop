# dotnet-mrc-shop

A modern, containerized microservices-based e-commerce sample built with ASP.NET 8. This project demonstrates a practical architecture for an online shop using independently deployable services, event-driven communication, and Docker-based local development.

## Overview

The solution includes the following services:

- Catalog API: manages product catalog data
- Basket API: handles shopping basket operations
- Discount gRPC service: provides discount rules and pricing logic
- Order API: processes customer orders and integrates with downstream services

The application also uses PostgreSQL, Redis, and RabbitMQ to support data persistence, caching, and asynchronous messaging.

## Architecture Highlights

- Microservices-oriented design for separation of concerns
- gRPC-based communication for internal service-to-service calls
- Redis for caching and fast access to basket-related data
- PostgreSQL for durable storage
- RabbitMQ for asynchronous event-driven workflows
- Docker Compose for local orchestration

## Technology Stack

- .NET 8 / ASP.NET Core
- PostgreSQL
- Marten
- Redis
- RabbitMQ
- gRPC
- Docker Compose

## Prerequisites

Before running the project, make sure you have:

- .NET 8 SDK
- Docker Desktop or Docker Engine
- Docker Compose
- Optional: Visual Studio 2022 or VS Code

## Getting Started

1. Clone the repository:

   ```bash
   git clone https://github.com/codingandcoffeerighthand/dotnet-mrc-shop.git
   cd dotnet-mrc-shop
   ```

2. Start the infrastructure and services:

   ```bash
   docker compose up --build
   ```

3. Access the services once the containers are running:

   - Catalog API: http://localhost:6000
   - Basket API: http://localhost:6001
   - Discount gRPC: http://localhost:6002
   - Order API: http://localhost:6003
   - RabbitMQ Management UI: http://localhost:15672

## Project Structure

```text
services/
  Basket/
  Catalog/
  Discount/
  Order/
shared/
  Shared.Messaging/
```

Each service contains its own API or gRPC entry point, domain logic, and supporting infrastructure.

## Roadmap

- Add an API Gateway with YARP
- Improve observability and distributed tracing
- Expand automated testing and CI/CD pipelines

## License

This project is intended for educational and demonstration purposes.