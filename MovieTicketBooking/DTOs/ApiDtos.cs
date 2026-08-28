using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.DTOs;

public class CreateMovieDto
{
    [Required] public string MovieName { get; set; } = string.Empty;
    [Required] public string DirectorName { get; set; } = string.Empty;
    [Required] public string ProducerName { get; set; } = string.Empty;
    [Range(0.01, double.MaxValue)] public double Duration { get; set; }
    public string Story { get; set; } = string.Empty;
    [Required] public string Genre { get; set; } = string.Empty;
    [Required] public string Language { get; set; } = string.Empty;
}

public class CreateTheatreDto
{
    [Required] public string TheatreName { get; set; } = string.Empty;
    [Range(1, int.MaxValue)] public int NumberofSeats { get; set; }
}

public class CreateCustomerDto
{
    [Required] public string CustomerName { get; set; } = string.Empty;
    [Required] public string City { get; set; } = string.Empty;
}

public class CreateShowDto
{
    [Required] public string MovieID { get; set; } = string.Empty;
    [Range(1, int.MaxValue)] public int TheatreID { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [Range(0, double.MaxValue)] public decimal PlatinumSeatRate { get; set; }
    [Range(0, double.MaxValue)] public decimal GoldSeatRate { get; set; }
    [Range(0, double.MaxValue)] public decimal SilverSeatRate { get; set; }
}

public class CreateBookingDto
{
    [Required] public string CustomerName { get; set; } = string.Empty;
    [EmailAddress] public string Email { get; set; } = string.Empty;
    [Range(1, int.MaxValue)] public int ShowID { get; set; }
    [Required] public string SeatType { get; set; } = string.Empty;
    [Range(1, 4)] public int NumberOfSeats { get; set; }
    public List<int> SeatNumbers { get; set; } = new();
}

public class LoginRequestDto
{
    [Required] public string LoginID { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
}

public record MovieDto(string MovieID, string MovieName, string DirectorName, string ProducerName, double Duration, string Story, string Genre, string Language);
public record TheatreDto(int TheatreID, string TheatreName, int NumberofSeats);
public record CustomerDto(int CustomerID, string CustomerName, string City);
public record ShowDto(int ShowID, string MovieID, string? MovieName, int TheatreID, string? TheatreName, DateTime StartDate, DateTime EndDate, decimal PlatinumSeatRate, decimal GoldSeatRate, decimal SilverSeatRate);
public record BookingDto(int BookingID, int ShowID, string? MovieName, string? TheatreName, DateTime BookingDate, string CustomerName, int NumberOfSeats, string SeatType, decimal Amount, string Email, string BookingStatus, List<int> SeatNumbers);
