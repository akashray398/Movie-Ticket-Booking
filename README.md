# 🎬 Movie Ticket Booking System

A full-stack Movie Ticket Booking System developed using **C#, ASP.NET Core, Entity Framework Core, and SQL Server**.

I developed this project as part of my .NET learning and training to understand how C# concepts can be used in a complete web application.

## 📌 About the Project

The Movie Ticket Booking System allows users to view movies, theatres and shows, check available seats, and book movie tickets.

The project also includes an admin section for managing movies, theatres, shows, customers and bookings.

## ✨ Features

- View available movies and shows
- Movie and theatre management
- Customer management
- User login
- Check available seats
- Book movie tickets
- Booking confirmation
- Cancel bookings
- Admin dashboard
- REST API support
- Swagger API testing
- SQL Server database integration

## 🛠️ Technologies Used

- C#
- .NET 10
- ASP.NET Core
- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- LINQ
- Razor Views
- HTML
- CSS
- Bootstrap
- Swagger / OpenAPI

## 📂 Project Structure

Movie-Ticket-Booking/
│
├── Controllers/
├── Data/
├── Database/
├── DTOs/
├── Exceptions/
├── Models/
├── Middleware/
├── Services/
├── Views/
├── wwwroot/
├── Program.cs
├── appsettings.json
└── MovieTicketBooking.csproj

## ⚙️ How to Run

### 1. Clone the repository

git clone https://github.com/akashray398/Movie-Ticket-Booking.git

cd Movie-Ticket-Booking

### 2. Restore packages

dotnet restore

### 3. Update the database

dotnet ef database update --project .\MovieTicketBooking\MovieTicketBooking.csproj

### 4. Run the project

dotnet run --project .\MovieTicketBooking\MovieTicketBooking.csproj

After running the project, open the URL displayed in the terminal in your browser.

## 🔑 Demo Login

### Admin

Login ID: MOVIEADMIN  
Password: MOVIEADMIN  
Login Type: A

### Customer

Login ID: 1001  
Password: 1001  
Login Type: C

> These credentials are only provided for project demonstration.

## 📚 What I Learned

While developing this project, I practiced and learned:

- C# and Object-Oriented Programming
- ASP.NET Core MVC
- REST API development
- CRUD operations
- Entity Framework Core
- SQL Server integration
- LINQ
- Dependency Injection
- Exception Handling
- Middleware
- DTOs
- Database migrations
- Basic service-layer architecture

## 🚀 Future Improvements

Some features that can be added in the future:

- JWT authentication
- Online payment integration
- Email ticket confirmation
- Better seat selection UI
- Role-based authorization
- Deployment to cloud

## 👨‍💻 Author

**Akash Yadav**

GitHub: @akashray398

## 📄 License

This project is licensed under the MIT License.
