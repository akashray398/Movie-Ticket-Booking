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

 ---

## 🎟️ Booking Flow

The booking process follows a simple step-by-step flow from selecting a movie to confirming the ticket.

```mermaid
flowchart TD

    A[👤 User Login] --> B[🎬 View Movies]

    B --> C[📅 Select Show]

    C --> D[💺 Check Available Seats]

    D --> E[🎫 Select Seats]

    E --> F{Seats Available?}

    F -->|No| G[❌ Seat Not Available]

    F -->|Yes| H[💰 Calculate Booking Amount]

    H --> I[✅ Create Booking]

    I --> J[🎟️ Booking Confirmation]

    J --> K{Cancel Booking?}

    K -->|Yes| L[❌ Cancel Booking]

    L --> M[💺 Release Seats]

    K -->|No| N[✔ Booking Completed]

---
## 📂 Project Structure

The project is organized into separate folders to keep the code simple, structured, and easy to maintain.

```text
Movie-Ticket-Booking/
│
├── 📄 README.md
├── 📄 LICENSE
├── 📄 .gitignore
│
└── MovieTicketBooking/
    │
    ├── 🎮 Controllers/
    │   └── ApiControllers.cs
    │
    ├── 💾 Data/
    │   └── MovieDataStore.cs
    │
    ├── 🗄️ Database/
    │   ├── DatabaseConnectionSettings.cs
    │   ├── DatabaseInitializer.cs
    │   ├── MovieDatabaseService.cs
    │   ├── MovieDbContext.cs
    │   ├── MovieDbContextFactory.cs
    │   └── Migrations/
    │
    ├── 📦 DTOs/
    ├── ⚠️ Exceptions/
    ├── 📚 Models/
    ├── 🔧 Middleware/
    ├── ⚙️ Services/
    ├── 🖥️ Views/
    │
    ├── 🌐 Web/
    │   └── Controllers/
    │
    ├── 🎨 wwwroot/
    │   └── css/
    │
    ├── 📁 DataFiles/
    │
    ├── Program.cs
    ├── appsettings.json
    └── MovieTicketBooking.csproj
```

### 📌 Folder Overview

| Folder | Purpose |
|---|---|
| `Controllers` | Handles API requests |
| `Models` | Contains application data models |
| `DTOs` | Transfers data between API and application |
| `Services` | Contains business logic |
| `Database` | Handles EF Core and database configuration |
| `Middleware` | Handles application-level exceptions |
| `Views` | Contains Razor UI pages |
| `wwwroot` | Contains CSS and static files |
| `DataFiles` | Stores file-based application data |

---

## 🚀 Getting Started

Follow these steps to run the project on your local system.

### 📋 Prerequisites

Make sure you have the following installed:

- .NET SDK 10
- SQL Server Express
- Git
- Visual Studio or VS Code
- SQL Server Management Studio *(optional)*

### 1️⃣ Clone the Repository

```bash
git clone https://github.com/akashray398/Movie-Ticket-Booking.git
cd Movie-Ticket-Booking
```

### 2️⃣ Restore Dependencies

```bash
dotnet restore
```

### 3️⃣ Configure SQL Server

The project uses the following local SQL Server connection:

```text
Server=.\SQLEXPRESS;
Database=MovieTicketBookingDB;
Trusted_Connection=True;
TrustServerCertificate=True;
```

> Keep passwords, API keys, and private connection strings out of GitHub.

### 4️⃣ Update the Database

```bash
dotnet ef database update --project .\MovieTicketBooking\MovieTicketBooking.csproj
```

### 5️⃣ Run the Application

```bash
dotnet run --project .\MovieTicketBooking\MovieTicketBooking.csproj
```

After running, open the application URL displayed in the terminal.

---
## 🔑 Demo Credentials

Use the following credentials to test the login functionality.

### 👨‍💼 Administrator

```text
Login ID   : MOVIEADMIN
Password   : MOVIEADMIN
Login Type : A
```

### 👤 Customer

```text
Login ID   : 1001
Password   : 1001
Login Type : C
```

> **Note:** These credentials are provided only for project demonstration and testing purposes.

---
## 🌐 Application Routes & REST API

The application provides both **MVC pages** for the user interface and **REST API endpoints** for accessing and managing data.

### 🖥️ Application Routes

| Page | Route |
|---|---|
| 🏠 Home | `/` |
| 🎬 Movies | `/Movies` |
| 🏢 Theatres | `/Theatres` |
| 📅 Shows | `/Shows` |
| 💺 Available Seats | `/Shows/AvailableSeats/{showId}` |
| 👤 Customers | `/Customers` |
| 🎟️ Bookings | `/Bookings` |
| ➕ Create Booking | `/Bookings/Create` |
| ✅ Confirmation | `/Bookings/Confirmation/{bookingId}` |
| 🛠️ Admin Dashboard | `/Admin` |
| 🔐 Login | `/Login` |
| 📖 Swagger | `/swagger` |

### 🔗 REST API

#### 🎬 Movies

```http
GET    /api/movies
GET    /api/movies/{movieId}
GET    /api/movies/search?name=action
POST   /api/movies
PUT    /api/movies/{movieId}
DELETE /api/movies/{movieId}
```

#### 🏢 Theatres, Customers & Shows

```http
GET, POST, PUT, DELETE /api/theatres
GET, POST, PUT, DELETE /api/customers
GET, POST, PUT, DELETE /api/shows

GET /api/shows/{showId}/available-seats
```

#### 🎟️ Bookings & Login

```http
GET, POST, PUT, DELETE /api/bookings
GET /api/bookings/{bookingId}
GET /api/bookings/show/{showId}/available-seats

PUT  /api/bookings/{bookingId}/cancel
POST /api/login
```

> 📖 All available API endpoints can also be explored and tested using **Swagger** at `/swagger`.

---
## 🧠 Concepts Used & What I Learned

This project helped me understand how different **C# and .NET concepts** work together in a complete web application.

### 📚 Concepts Used

<p>
  <img src="https://img.shields.io/badge/C%23-OOP-239120?logo=csharp&logoColor=white" />
  <img src="https://img.shields.io/badge/LINQ-Querying-blueviolet" />
  <img src="https://img.shields.io/badge/EF_Core-ORM-512BD4" />
  <img src="https://img.shields.io/badge/REST-API-orange" />
  <img src="https://img.shields.io/badge/SQL-Database-CC2927" />
</p>

- C# and Object-Oriented Programming
- Classes and Objects
- Collections and LINQ
- Exception Handling
- ASP.NET Core MVC
- Razor Views
- REST API and CRUD Operations
- Entity Framework Core
- SQL Server
- DTOs
- Dependency Injection
- Middleware
- Service Layer
- Database Migrations
- Input Validation

### 🎯 What I Learned

While developing this project, I gained practical experience in:

- Building an ASP.NET Core MVC application
- Creating and testing REST APIs
- Connecting a C# application with SQL Server
- Performing CRUD operations
- Using Entity Framework Core and LINQ
- Organizing business logic using services
- Handling exceptions and validations
- Managing movie bookings and seat availability

---
## 🚀 Future Improvements

The current version covers the main movie booking functionalities. In the future, the project can be improved with more advanced features.

<p>
  <img src="https://img.shields.io/badge/Future-Enhancements-blueviolet" />
  <img src="https://img.shields.io/badge/Status-Planned-yellow" />
</p>

- 🔐 JWT Authentication & Role-Based Authorization
- 💳 Online Payment Integration
- 📧 Email Booking Confirmation
- 🎫 Downloadable Digital Tickets
- 💺 Interactive Seat Selection
- 🔔 Booking Notifications
- 📊 Admin Analytics Dashboard
- 🔍 Advanced Movie Search & Filtering
- ☁️ Cloud Deployment
- 📱 Improved Responsive UI

These improvements can make the application more secure, user-friendly, and closer to a real-world movie booking platform.

---
## 📜 License

<p>
  <img src="https://img.shields.io/badge/License-MIT-green.svg" />
</p>

This project is licensed under the **MIT License**.

Copyright © 2026 **Akash Yadav**

See the [LICENSE](LICENSE) file for complete license details.

---

## 👨‍💻 Author

<p>
  <img src="https://img.shields.io/badge/Developer-Akash_Yadav-blue" />
  <img src="https://img.shields.io/badge/GitHub-akashray398-181717?logo=github&logoColor=white" />
</p>

**Akash Yadav**

- GitHub: [@akashray398](https://github.com/akashray398)
- Repository: [Movie Ticket Booking System](https://github.com/akashray398/Movie-Ticket-Booking)

---

<p align="center">
  <b>🎬 Movie Ticket Booking System</b>
</p>

<p align="center">
  Built with ❤️ using C# • ASP.NET Core • Entity Framework Core • SQL Server
</p>

<p align="center">
  ⭐ If you found this project useful, consider giving the repository a star!
</p>
