namespace MovieTicketBooking.Exceptions;

// Thrown when an unsupported movie language is provided.
public class InvalidLanguageException : Exception
{
    public InvalidLanguageException()
        : base("The mentioned language is invalid. Please ensure to enter a valid language")
    {
    }
}
