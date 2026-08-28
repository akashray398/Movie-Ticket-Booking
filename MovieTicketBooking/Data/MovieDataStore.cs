using MovieTicketBooking.Models;

namespace MovieTicketBooking.Data;

public class MovieDataStore
{
    public List<Movie> Movies { get; set; }
    public List<Theatre> Theatres { get; set; }
    public List<Customer> Customers { get; set; }
    public List<LoginDetails> Logins { get; set; }
    public List<Show> Shows { get; set; }
    public List<Booking> Bookings { get; set; }

    public MovieDataStore()
    {
        Movies = new List<Movie>();
        Theatres = new List<Theatre>();
        Customers = new List<Customer>();
        Logins = new List<LoginDetails>();
        Shows = new List<Show>();
        Bookings = new List<Booking>();
    }

    public void AddMovie(Movie obj)
    {
        if (obj == null)
        {
            throw new NullReferenceException("Movie details can't be null");
        }

        Movies.Add(obj);
    }

    public void AddTheatre(Theatre obj)
    {
        if (obj == null)
        {
            throw new NullReferenceException("Theatre details can't be null");
        }

        Theatres.Add(obj);
    }

    public void AddCustomers(Customer obj)
    {
        if (obj == null)
        {
            throw new NullReferenceException("Customer details can't be null");
        }

        Customers.Add(obj);
    }

    public void AddLogin(LoginDetails obj)
    {
        if (obj == null)
        {
            throw new NullReferenceException("Login details can't be null");
        }

        Logins.Add(obj);
    }

    public void AddShows(Show obj)
    {
        if (obj == null)
        {
            throw new NullReferenceException("Show details can't be null");
        }

        Shows.Add(obj);
    }

    public void AddBookings(Booking obj)
    {
        if (obj == null)
        {
            throw new NullReferenceException("Booking details can't be null");
        }

        Bookings.Add(obj);
    }

    public bool UpdateMovie(string movieId, Movie updatedMovie)
    {
        if (updatedMovie == null)
        {
            return false;
        }

        var existingMovie = Movies.FirstOrDefault(m => m.MovieID == movieId);
        if (existingMovie == null)
        {
            return false;
        }

        existingMovie.MovieName = updatedMovie.MovieName;
        existingMovie.DirectorName = updatedMovie.DirectorName;
        existingMovie.ProducerName = updatedMovie.ProducerName;
        existingMovie.Duration = updatedMovie.Duration;
        existingMovie.Story = updatedMovie.Story;
        existingMovie.Genre = updatedMovie.Genre;
        existingMovie.Language = updatedMovie.Language;
        existingMovie.MovieID = movieId;
        existingMovie.MovieID = GenerateMovieID(existingMovie.MovieName, existingMovie.ProducerName, existingMovie.Genre, existingMovie.Language);

        return true;
    }

    public bool UpdateTheatre(int theatreId, Theatre updatedTheatre)
    {
        if (updatedTheatre == null)
        {
            return false;
        }

        var existingTheatre = Theatres.FirstOrDefault(t => t.TheatreID == theatreId);
        if (existingTheatre == null)
        {
            return false;
        }

        existingTheatre.TheatreName = updatedTheatre.TheatreName;
        existingTheatre.NumberofSeats = updatedTheatre.NumberofSeats;
        return true;
    }

    public bool UpdateCustomer(int customerId, Customer updatedCustomer)
    {
        if (updatedCustomer == null)
        {
            return false;
        }

        var existingCustomer = Customers.FirstOrDefault(c => c.CustomerID == customerId);
        if (existingCustomer == null)
        {
            return false;
        }

        existingCustomer.CustomerName = updatedCustomer.CustomerName;
        existingCustomer.City = updatedCustomer.City;
        return true;
    }

    public bool UpdateLogin(string loginId, LoginDetails updatedLogin)
    {
        if (updatedLogin == null)
        {
            return false;
        }

        var existingLogin = Logins.FirstOrDefault(l => l.LoginID == loginId);
        if (existingLogin == null)
        {
            return false;
        }

        existingLogin.Password = updatedLogin.Password;
        existingLogin.LoginType = updatedLogin.LoginType;
        return true;
    }

    public bool UpdateShow(int showId, Show updatedShow)
    {
        if (updatedShow == null)
        {
            return false;
        }

        var existingShow = Shows.FirstOrDefault(s => s.ShowID == showId);
        if (existingShow == null)
        {
            return false;
        }

        existingShow.MovieID = updatedShow.MovieID;
        existingShow.TheatreID = updatedShow.TheatreID;
        existingShow.StartDate = updatedShow.StartDate;
        existingShow.EndDate = updatedShow.EndDate;
        existingShow.PlatinumSeatRate = updatedShow.PlatinumSeatRate;
        existingShow.GoldSeatRate = updatedShow.GoldSeatRate;
        existingShow.SilverSeatRate = updatedShow.SilverSeatRate;
        return true;
    }

    public bool UpdateBooking(int bookingId, Booking updatedBooking)
    {
        if (updatedBooking == null)
        {
            return false;
        }

        var existingBooking = Bookings.FirstOrDefault(b => b.BookingID == bookingId);
        if (existingBooking == null)
        {
            return false;
        }

        existingBooking.ShowID = updatedBooking.ShowID;
        existingBooking.CustomerName = updatedBooking.CustomerName;
        existingBooking.NumberOfSeats = updatedBooking.NumberOfSeats;
        existingBooking.SeatType = updatedBooking.SeatType;
        existingBooking.Amount = updatedBooking.Amount;
        existingBooking.Email = updatedBooking.Email;
        existingBooking.BookingStatus = updatedBooking.BookingStatus;
        existingBooking.SeatNumbers = updatedBooking.SeatNumbers;
        return true;
    }

    public bool DeleteMovie(string movieId)
    {
        if (Shows.Any(s => s.MovieID == movieId))
        {
            Console.WriteLine("Cannot delete movie because it is linked to one or more shows.");
            return false;
        }

        var movie = Movies.FirstOrDefault(m => m.MovieID == movieId);
        if (movie == null)
        {
            return false;
        }

        Movies.Remove(movie);
        return true;
    }

    public bool DeleteTheatre(int theatreId)
    {
        if (Shows.Any(s => s.TheatreID == theatreId))
        {
            Console.WriteLine("Cannot delete theatre because it is linked to one or more shows.");
            return false;
        }

        var theatre = Theatres.FirstOrDefault(t => t.TheatreID == theatreId);
        if (theatre == null)
        {
            return false;
        }

        Theatres.Remove(theatre);
        return true;
    }

    public bool DeleteCustomer(int customerId)
    {
        var customer = Customers.FirstOrDefault(c => c.CustomerID == customerId);
        if (customer == null)
        {
            return false;
        }

        if (Bookings.Any(b => b.CustomerName.Equals(customer.CustomerName, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("Cannot delete customer because bookings exist for this customer.");
            return false;
        }

        Customers.Remove(customer);
        return true;
    }

    public bool DeleteLogin(string loginId)
    {
        var login = Logins.FirstOrDefault(l => l.LoginID == loginId);
        if (login == null)
        {
            return false;
        }

        Logins.Remove(login);
        return true;
    }

    public bool DeleteShow(int showId)
    {
        if (Bookings.Any(b => b.ShowID == showId))
        {
            Console.WriteLine("Cannot delete show because it is linked to one or more bookings.");
            return false;
        }

        var show = Shows.FirstOrDefault(s => s.ShowID == showId);
        if (show == null)
        {
            return false;
        }

        Shows.Remove(show);
        return true;
    }

    public bool DeleteBooking(int bookingId)
    {
        var booking = Bookings.FirstOrDefault(b => b.BookingID == bookingId);
        if (booking == null)
        {
            return false;
        }

        Bookings.Remove(booking);
        return true;
    }

    public List<Movie> SearchMovie(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return Movies;
        }

        return Movies
            .Where(m =>
                m.MovieID.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                m.MovieName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                m.Genre.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                m.Language.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Theatre> SearchTheatre(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return Theatres;
        }

        if (int.TryParse(keyword, out int theatreId))
        {
            return Theatres.Where(t => t.TheatreID == theatreId).ToList();
        }

        return Theatres
            .Where(t => t.TheatreName.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Customer> SearchCustomer(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return Customers;
        }

        if (int.TryParse(keyword, out int customerId))
        {
            return Customers.Where(c => c.CustomerID == customerId).ToList();
        }

        return Customers
            .Where(c =>
                c.CustomerName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                c.City.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<LoginDetails> SearchLogin(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return Logins;
        }

        return Logins
            .Where(l =>
                l.LoginID.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                l.LoginType.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Show> SearchShow(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return Shows;
        }

        if (int.TryParse(keyword, out int numericValue))
        {
            return Shows.Where(s => s.ShowID == numericValue || s.TheatreID == numericValue).ToList();
        }

        return Shows
            .Where(s =>
                s.MovieID.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Booking> SearchBooking(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return Bookings;
        }

        if (int.TryParse(keyword, out int bookingId))
        {
            return Bookings.Where(b => b.BookingID == bookingId || b.ShowID == bookingId).ToList();
        }

        return Bookings
            .Where(b =>
                b.CustomerName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                b.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                b.BookingStatus.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public void DisplayMovies()
    {
        if (Movies.Count == 0)
        {
            Console.WriteLine("No records found.");
            return;
        }

        foreach (var movie in Movies)
        {
            movie.DisplayMovieDetails();
        }
    }

    public void DisplayTheatres()
    {
        if (Theatres.Count == 0)
        {
            Console.WriteLine("No records found.");
            return;
        }

        foreach (var theatre in Theatres)
        {
            theatre.DisplayTheatreDetails();
        }
    }

    public void DisplayCustomers()
    {
        if (Customers.Count == 0)
        {
            Console.WriteLine("No records found.");
            return;
        }

        foreach (var customer in Customers)
        {
            customer.DisplayCustomerDetails();
        }
    }

    public void DisplayLogins()
    {
        if (Logins.Count == 0)
        {
            Console.WriteLine("No records found.");
            return;
        }

        foreach (var login in Logins)
        {
            login.DisplayLoginDetails();
        }
    }

    public void DisplayShows()
    {
        if (Shows.Count == 0)
        {
            Console.WriteLine("No records found.");
            return;
        }

        foreach (var show in Shows)
        {
            show.DisplayShowDetails();
        }
    }

    public void DisplayBookings()
    {
        if (Bookings.Count == 0)
        {
            Console.WriteLine("No records found.");
            return;
        }

        foreach (var booking in Bookings)
        {
            booking.DisplayBookingDetails();
        }
    }

    private static string GenerateMovieID(string movieName, string producerName, string genre, string language)
    {
        string moviePrefix = GetFirstTwoCharacters(movieName);
        string producerPrefix = GetFirstTwoCharacters(producerName);
        string genrePrefix = GetFirstTwoCharacters(genre);
        string languagePrefix = GetFirstTwoCharacters(language);

        return $"{moviePrefix}-{producerPrefix}-{genrePrefix}-{languagePrefix}".ToUpper();
    }

    private static string GetFirstTwoCharacters(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        return value.Substring(0, Math.Min(2, value.Length));
    }
}
