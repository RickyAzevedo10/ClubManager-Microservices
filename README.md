# Microservices Architecture

This repository contains the microservices-based version of the system, developed as part of the performance comparison study between **monolithic** and **microservices** architectures.  
The project represents the evolution of the initial monolithic system into a distributed architecture, designed to analyse aspects such as **latency**, **throughput**, and **scalability**.

---

## 🔄 Migration Process

The transition from the monolithic system to microservices followed an **incremental migration approach**, inspired by the **Strangler Fig pattern** (Martin Fowler).  
This method allowed both systems to coexist during the migration, ensuring stability and continuity while gradually refactoring the monolithic functionalities into independent services.  

Each new service was implemented with **clean architecture** principles and **domain-driven design (DDD)** concepts, ensuring modularity and a clear separation of responsibilities.

---

## ⚙️ Solution Architecture

Each microservice was designed to be **autonomous**, owning its **own SQL Server database** and handling its own logic independently.  
To maintain data integrity, a **data duplication strategy** was adopted — relevant information is replicated between services via **event-driven communication**, ensuring that all services remain decoupled yet consistent.  

The system also integrates:
- **Redis** for caching authentication tokens and user permissions.  
- **FluentValidation** for input validation.  
- **AutoMapper** for data transformation between domain models and DTOs.  
- **Docker Compose** for containerized deployment, using a **bridge network** to enable communication between services.  

---

## 📨 Communication Between Services

Communication between microservices follows an **event-driven architecture**, using:
- **RabbitMQ** as the message broker.
- **MassTransit** as the message bus abstraction layer.

This asynchronous approach decouples producers and consumers, allowing each service to scale, update, and deploy independently.  
Each microservice defines **message contracts** (events and commands), which are shared through a common library referenced across all services.

---

## 🌐 API Gateway Integration

An **API Gateway** was implemented using **YARP (Yet Another Reverse Proxy)**.  
It acts as a single entry point to the microservices ecosystem, routing incoming requests to the appropriate service based on pre-defined routes and clusters.  

Although authentication and authorization are handled within each service, the gateway is fully prepared to support centralized validation if required in future iterations.

---

## 🧩 Some Technologies Used

- **.NET 8 (ASP.NET Core)**
- **RabbitMQ**  
- **MassTransit**  
- **YARP (Yet Another Reverse Proxy)**  
- **Redis**  
- **SQL Server**  
- **Docker Compose**
