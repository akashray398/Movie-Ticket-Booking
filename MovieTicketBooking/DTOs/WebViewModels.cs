namespace MovieTicketBooking.DTOs;

public class BookingCreateViewModel
{
    public CreateBookingDto Booking { get; set; } = new();
    public IEnumerable<ShowDto> Shows { get; set; } = Array.Empty<ShowDto>();
    public string SeatNumbersText { get; set; } = string.Empty;
}

public class AdminDashboardViewModel
{
    public int Movies { get; set; }
    public int Theatres { get; set; }
    public int Customers { get; set; }
    public int Shows { get; set; }
    public int Bookings { get; set; }
}
