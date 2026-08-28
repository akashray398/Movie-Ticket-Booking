using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Database;

public static class DatabaseInitializer
{
    public static async Task SeedAsync(MovieDbContext context)
    {
        await context.Database.MigrateAsync();

        await using var transaction = await context.Database.BeginTransactionAsync();

        if (!await context.Movies.AnyAsync())
        {
            await context.Movies.AddRangeAsync(
                CreateMovie("IN-RO-AC-HI", "Inception", "Christopher Nolan", "Emma Thomas", 2.7, "A dream within a dream.", "Action", "English"),
                CreateMovie("DU-RA-DR-EN", "Dune", "Denis Villeneuve", "Mary Parent", 2.8, "A journey across Arrakis.", "Drama", "English"),
                CreateMovie("JA-RA-CO-EN", "Jawan", "Atlee", "Gauri Khan", 2.6, "A story of justice and courage.", "Comedy", "Hindi"));
        }

        if (!await context.Theatres.AnyAsync())
        {
            await context.Theatres.AddRangeAsync(
                CreateTheatre(101, "PVR Screen 1", 100),
                CreateTheatre(102, "INOX Screen 2", 80));
        }

        if (!await context.Customers.AnyAsync())
        {
            await context.Customers.AddRangeAsync(
                CreateCustomer(1001, "Asha", "Delhi"),
                CreateCustomer(1002, "Rahul", "Mumbai"));
        }

        if (!await context.LoginDetails.AnyAsync(l => l.LoginID == "MOVIEADMIN"))
        {
            await context.LoginDetails.AddAsync(new LoginDetails(true));
        }

        foreach (var customerId in new[] { 1001, 1002 })
        {
            var loginId = customerId.ToString();
            if (!await context.LoginDetails.AnyAsync(l => l.LoginID == loginId))
            {
                await context.LoginDetails.AddAsync(new LoginDetails(customerId));
            }
        }

        await context.SaveChangesAsync();

        if (!await context.Shows.AnyAsync())
        {
            await context.Shows.AddRangeAsync(
                CreateShow(2001, "IN-RO-AC-HI", 101, 320m, 220m, 170m),
                CreateShow(2002, "DU-RA-DR-EN", 101, 350m, 240m, 180m),
                CreateShow(2003, "JA-RA-CO-EN", 102, 300m, 200m, 150m));
            await context.SaveChangesAsync();
        }

        if (!await context.Bookings.AnyAsync())
        {
            await context.Bookings.AddAsync(new Booking(2001, "Asha", 2, "Gold", "asha@example.com")
            {
                BookingID = 5001,
                SeatNumbers = new List<int> { 1, 2 },
                Amount = 440m,
                BookingStatus = "Reserved"
            });
            await context.SaveChangesAsync();
        }

        await transaction.CommitAsync();
    }

    private static Movie CreateMovie(string id, string name, string director, string producer, double duration, string story, string genre, string language)
    {
        return new Movie(name, director, producer, duration, story, genre, language) { MovieID = id };
    }

    private static Theatre CreateTheatre(int id, string name, int seats)
    {
        return new Theatre(name, seats) { TheatreID = id };
    }

    private static Customer CreateCustomer(int id, string name, string city)
    {
        return new Customer(name, city) { CustomerID = id };
    }

    private static Show CreateShow(int id, string movieId, int theatreId, decimal platinum, decimal gold, decimal silver)
    {
        return new Show(movieId, theatreId, DateTime.Today.AddDays(1), DateTime.Today.AddDays(1).AddHours(3), platinum, gold, silver)
        {
            ShowID = id
        };
    }
}
