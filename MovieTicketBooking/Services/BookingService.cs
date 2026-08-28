using MovieTicketBooking.Data;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Services;

public static class BookingService
{
    public static MovieDataStore DataStore { get; set; } = new MovieDataStore();

    public static decimal CalculateAmount(int showId, string seatType, int numberOfSeats)
    {
        var show = DataStore.Shows.FirstOrDefault(s => s.ShowID == showId);
        if (show == null)
        {
            throw new KeyNotFoundException("Show not found.");
        }

        string normalizedSeatType = NormalizeSeatType(seatType);
        decimal rate = normalizedSeatType switch
        {
            "Platinum" => show.PlatinumSeatRate,
            "Gold" => show.GoldSeatRate,
            "Silver" => show.SilverSeatRate,
            _ => throw new ArgumentException("Seat type must be Platinum, Gold, or Silver.")
        };

        return rate * numberOfSeats;
    }

    public static List<int> GetAvailableSeats(int showId)
    {
        var show = DataStore.Shows.FirstOrDefault(s => s.ShowID == showId);
        if (show == null)
        {
            throw new KeyNotFoundException("Show not found.");
        }

        var theatre = DataStore.Theatres.FirstOrDefault(t => t.TheatreID == show.TheatreID);
        if (theatre == null)
        {
            throw new KeyNotFoundException("Theatre not found.");
        }

        var bookedSeats = DataStore.Bookings
            .Where(b => b.ShowID == showId && b.BookingStatus != "Cancelled")
            .SelectMany(b => b.SeatNumbers)
            .ToHashSet();

        return Enumerable.Range(1, theatre.NumberofSeats)
            .Where(seatNumber => !bookedSeats.Contains(seatNumber))
            .ToList();
    }

    public static Booking CreateBooking(int showId, string customerName, int numberOfSeats, string seatType, string email, List<int>? requestedSeatNumbers = null)
    {
        var show = DataStore.Shows.FirstOrDefault(s => s.ShowID == showId);
        if (show == null)
        {
            throw new KeyNotFoundException("Show not found.");
        }

        var theatre = DataStore.Theatres.FirstOrDefault(t => t.TheatreID == show.TheatreID);
        if (theatre == null)
        {
            throw new KeyNotFoundException("Theatre not found.");
        }

        if (numberOfSeats < 1 || numberOfSeats > 4)
        {
            throw new ArgumentException("Number of seats must be between 1 and 4.");
        }

        var availableSeats = GetAvailableSeats(showId);
        var selectedSeats = requestedSeatNumbers ?? availableSeats.Take(numberOfSeats).ToList();

        if (selectedSeats.Count != numberOfSeats)
        {
            throw new InvalidOperationException("More seats requested than available.");
        }

        foreach (var seatNumber in selectedSeats)
        {
            if (!availableSeats.Contains(seatNumber))
            {
                throw new InvalidOperationException("One or more selected seats are already booked.");
            }
        }

        var booking = new Booking(showId, customerName, numberOfSeats, seatType, email)
        {
            SeatNumbers = selectedSeats,
            Amount = CalculateAmount(showId, seatType, numberOfSeats)
        };

        DataStore.AddBookings(booking);
        return booking;
    }

    public static bool CancelBooking(int bookingId)
    {
        var booking = DataStore.Bookings.FirstOrDefault(b => b.BookingID == bookingId);
        if (booking == null)
        {
            return false;
        }

        booking.CancelBooking();
        return true;
    }

    public static string NormalizeSeatType(string seatType)
    {
        if (string.IsNullOrWhiteSpace(seatType))
        {
            throw new ArgumentException("Seat type must be Platinum, Gold, or Silver.");
        }

        if (seatType.Equals("Platinum", StringComparison.OrdinalIgnoreCase))
        {
            return "Platinum";
        }

        if (seatType.Equals("Gold", StringComparison.OrdinalIgnoreCase))
        {
            return "Gold";
        }

        if (seatType.Equals("Silver", StringComparison.OrdinalIgnoreCase))
        {
            return "Silver";
        }

        throw new ArgumentException("Seat type must be Platinum, Gold, or Silver.");
    }
}
