namespace MovieTicketBooking.Exceptions;

// Thrown when movie duration is zero or negative.
public class InvalidDurationException : Exception
{
    public InvalidDurationException()
        : base("The mentioned movie duration is invalid. Please ensure to enter a valid duration")
    {
    }
}
