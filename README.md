# 🎬 Movie Ticket Booking System

<p align="center">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/C%23-Programming-239120?logo=csharp&logoColor=white" />
  <img src="https://img.shields.io/badge/ASP.NET_Core-MVC%20%7C%20Web_API-512BD4?logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/Entity_Framework-Core-512BD4" />
  <img src="https://img.shields.io/badge/SQL_Server-Database-CC2927?logo=microsoftsqlserver&logoColor=white" />
  <img src="https://img.shields.io/badge/Swagger-API-85EA2D?logo=swagger&logoColor=black" />
  <img src="https://img.shields.io/badge/License-MIT-green.svg" />
</p>

<p align="center">
  A full-stack Movie Ticket Booking System built using
  <b>C#, ASP.NET Core, Entity Framework Core and SQL Server.</b>
</p>

---

## 📌 About the Project

Movie Ticket Booking System is a full-stack .NET application developed to understand how C# and ASP.NET Core concepts work together in a complete web application.

The application allows users to view movies and shows, check available seats, book tickets and cancel bookings.

It also includes an admin section for managing movies, theatres, shows, customers and bookings.

---

## ✨ Features

- 🎥 View and manage movies
- 🏢 Theatre management
- 📅 Show management
- 👤 Customer management
- 🔐 Login system
- 💺 Check available seats
- 🎟️ Book movie tickets
- 💰 Automatic amount calculation
- ✅ Booking confirmation
- ❌ Booking cancellation
- 🪑 Seat availability management
- 🛠️ Admin dashboard
- 🌐 REST API support
- 📖 Swagger / OpenAPI
- 🗄️ SQL Server integration
- ⚠️ Validation and exception handling

---

## 🛠️ Technology Stack

<p>
  <img src="https://img.shields.io/badge/C%23-Language-239120?logo=csharp&logoColor=white" />
  <img src="https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/ASP.NET_Core-Web_Framework-512BD4" />
  <img src="https://img.shields.io/badge/MVC-Razor_Views-blue" />
  <img src="https://img.shields.io/badge/Web_API-REST-orange" />
  <img src="https://img.shields.io/badge/EF_Core-ORM-purple" />
  <img src="https://img.shields.io/badge/SQL_Server-Database-CC2927?logo=microsoftsqlserver&logoColor=white" />
  <img src="https://img.shields.io/badge/LINQ-Querying-blueviolet" />
  <img src="https://img.shields.io/badge/Bootstrap-UI-7952B3?logo=bootstrap&logoColor=white" />
  <img src="https://img.shields.io/badge/Swagger-API_Testing-85EA2D?logo=swagger&logoColor=black" />
</p>

| Area | Technology |
|---|---|
| Language | C# |
| Runtime | .NET 10 |
| Framework | ASP.NET Core |
| Frontend | MVC, Razor, HTML, CSS, Bootstrap |
| API | ASP.NET Core Web API |
| ORM | Entity Framework Core |
| Database | SQL Server |
| Querying | LINQ |
| API Testing | Swagger / OpenAPI |
| Architecture | MVC + Service Layer + Dependency Injection |

---

## 🏗️ System Architecture

```mermaid
flowchart TD

    User[👤 User / Admin]

    User --> MVC[🌐 ASP.NET Core MVC]
    User --> API[🔗 Web API]

    MVC --> Controller[🎮 Controllers]
    API --> Controller

    Controller --> Service[⚙️ Service Layer]

    Service --> Validation[✅ Business Logic & Validation]

    Validation --> EF[📦 Entity Framework Core]

    EF --> DB[(🗄️ SQL Server Database)]

    Service --> DTO[📄 DTOs]

    Middleware[⚠️ Exception Middleware] --> Controller
