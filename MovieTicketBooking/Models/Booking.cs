using MovieTicketBooking.Services;

namespace MovieTicketBooking.Models;

public class Booking
{
    private static int _nextBookingId = 1000;

    public int BookingID { get; set; }
    public DateTime BookingDate { get; set; }
    public int ShowID { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int NumberOfSeats { get; set; }
    public string SeatType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Email { get; set; } = string.Empty;
    public string BookingStatus { get; set; } = string.Empty;
    public List<int> SeatNumbers { get; set; } = new();

    public Show? Show { get; set; }

    private Booking() { }

    public Booking(int showID, string customerName, int numberOfSeats, string seatType, string email)
    {
        if (numberOfSeats < 1 || numberOfSeats > 4)
        {
            throw new ArgumentException("Number of seats must be between 1 and 4.");
        }

        BookingID = GenerateBookingID();
        BookingDate = DateTime.Now;
        ShowID = showID;
        CustomerName = customerName;
        NumberOfSeats = numberOfSeats;
        SeatType = NormalizeSeatType(seatType);
        Email = email;

        try
        {
            Amount = BookingService.CalculateAmount(showID, SeatType, numberOfSeats);
        }
        catch
        {
            Amount = 0m;
        }

        BookingStatus = "Reserved";
        SeatNumbers = new List<int>();
    }

    public static void SetNextBookingId(int maxExistingBookingId)
    {
        _nextBookingId = Math.Max(_nextBookingId, maxExistingBookingId + 1);
    }

    private static int GenerateBookingID()
    {
        return _nextBookingId++;
    }

    private static string NormalizeSeatType(string seatType)
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

    public void CancelBooking()
    {
        BookingStatus = "Cancelled";
    }

    public void DisplayBookingDetails()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Booking Details");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Booking ID      : {BookingID}");
        Console.WriteLine($"Booking Date    : {BookingDate}");
        Console.WriteLine($"Show ID         : {ShowID}");
        Console.WriteLine($"Customer Name   : {CustomerName}");
        Console.WriteLine($"Number of Seats : {NumberOfSeats}");
        Console.WriteLine($"Seat Type       : {SeatType}");
        Console.WriteLine($"Amount          : {Amount}");
        Console.WriteLine($"Email           : {Email}");
        Console.WriteLine($"Booking Status  : {BookingStatus}");
        Console.WriteLine($"Seat Numbers    : {string.Join(", ", SeatNumbers)}");
        Console.WriteLine("----------------------------------------");
    }
}
