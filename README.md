# dotnet-mrc-shop: Modern Microservice eShop

This project showcases a lightweight and scalable eShop architecture built with ASP.NET 8 microservices. It prioritizes performance, adaptability, and maintainability.

## Technology Stack

* **Database:**
    - **Postgres:** Reliable and feature-rich database for core eShop functionalities.
    - **Marten:** Enables NoSQL-like interactions with Postgres for simplified data access.
    - **Outbox Pattern:** Ensures data consistency across services with asynchronous operations.
* **Cache:** **Redis** 
* **Message Broker:** **RabbitMQ** 
* **Docker compose**

## Todo

- [❌] Integrate YARP (Reverse Proxy & API Gateway)