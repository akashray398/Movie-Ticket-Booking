using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Database;

public class MovieDatabaseService
{
    private readonly MovieDbContext _context;

    public MovieDatabaseService(MovieDbContext context)
    {
        _context = context;
    }

    public async Task AddMovieAsync(Movie movie)
    {
        if (movie == null)
        {
            throw new ArgumentNullException(nameof(movie));
        }

        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();
    }

    public async Task AddTheatreAsync(Theatre theatre)
    {
        if (theatre == null)
        {
            throw new ArgumentNullException(nameof(theatre));
        }

        await _context.Theatres.AddAsync(theatre);
        await _context.SaveChangesAsync();
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }

        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task AddCustomerWithLoginAsync(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }

        await using var transaction = await _context.Database.BeginTransactionAsync();
        await _context.Customers.AddAsync(customer);
        await _context.LoginDetails.AddAsync(new LoginDetails(customer.CustomerID));
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    public async Task AddLoginAsync(LoginDetails login)
    {
        if (login == null)
        {
            throw new ArgumentNullException(nameof(login));
        }

        await _context.LoginDetails.AddAsync(login);
        await _context.SaveChangesAsync();
    }

    public async Task AddShowAsync(Show show)
    {
        if (show == null)
        {
            throw new ArgumentNullException(nameof(show));
        }

        await _context.Shows.AddAsync(show);
        await _context.SaveChangesAsync();
    }

    public async Task AddBookingAsync(Booking booking)
    {
        if (booking == null)
        {
            throw new ArgumentNullException(nameof(booking));
        }

        await _context.Bookings.AddAsync(booking);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Movie>> GetAllMoviesAsync()
    {
        return await _context.Movies
            .Include(m => m.Shows)
            .ToListAsync();
    }

    public async Task<List<Movie>> SearchMoviesAsync(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return await GetAllMoviesAsync();
        }

        return await _context.Movies
            .Where(m => m.MovieName.Contains(keyword) || m.Genre.Contains(keyword) || m.Language.Contains(keyword))
            .ToListAsync();
    }

    public async Task<List<int>> GetAvailableSeatsAsync(int showId)
    {
        var show = await _context.Shows.Include(s => s.Theatre).FirstOrDefaultAsync(s => s.ShowID == showId)
            ?? throw new KeyNotFoundException("Show not found.");
        var theatre = show.Theatre ?? throw new KeyNotFoundException("Theatre not found.");
        var bookings = await _context.Bookings
            .Where(b => b.ShowID == showId && b.BookingStatus != "Cancelled")
            .ToListAsync();
        var bookedSeats = bookings.SelectMany(b => b.SeatNumbers).ToList();
        return Enumerable.Range(1, theatre.NumberofSeats).Except(bookedSeats).ToList();
    }

    public async Task<bool> CancelBookingAsync(int bookingId)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingID == bookingId);
        if (booking == null)
        {
            return false;
        }

        booking.CancelBooking();
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Theatre>> GetAllTheatresAsync()
    {
        return await _context.Theatres
            .Include(t => t.Shows)
            .ToListAsync();
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<List<LoginDetails>> GetAllLoginsAsync()
    {
        return await _context.LoginDetails.ToListAsync();
    }

    public async Task<List<Show>> GetAllShowsAsync()
    {
        return await _context.Shows
            .Include(s => s.Movie)
            .Include(s => s.Theatre)
            .Include(s => s.Bookings)
            .ToListAsync();
    }

    public async Task<List<Booking>> GetAllBookingsAsync()
    {
        return await _context.Bookings
            .Include(b => b.Show)
            .ThenInclude(s => s!.Movie)
            .Include(b => b.Show)
            .ThenInclude(s => s!.Theatre)
            .ToListAsync();
    }

    public async Task<Movie?> GetMovieByIdAsync(string movieId)
    {
        return await _context.Movies
            .Include(m => m.Shows)
            .FirstOrDefaultAsync(m => m.MovieID == movieId);
    }

    public async Task<Theatre?> GetTheatreByIdAsync(int theatreId)
    {
        return await _context.Theatres
            .Include(t => t.Shows)
            .FirstOrDefaultAsync(t => t.TheatreID == theatreId);
    }

    public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(c => c.CustomerID == customerId);
    }

    public async Task<LoginDetails?> GetLoginByIdAsync(string loginId)
    {
        return await _context.LoginDetails
            .FirstOrDefaultAsync(l => l.LoginID == loginId);
    }

    public async Task<Show?> GetShowByIdAsync(int showId)
    {
        return await _context.Shows
            .Include(s => s.Movie)
            .Include(s => s.Theatre)
            .Include(s => s.Bookings)
            .FirstOrDefaultAsync(s => s.ShowID == showId);
    }

    public async Task<Booking?> GetBookingByIdAsync(int bookingId)
    {
        return await _context.Bookings
            .Include(b => b.Show)
            .ThenInclude(s => s!.Movie)
            .Include(b => b.Show)
            .ThenInclude(s => s!.Theatre)
            .FirstOrDefaultAsync(b => b.BookingID == bookingId);
    }

    public async Task<bool> UpdateMovieAsync(string movieId, Movie updatedMovie)
    {
        var existingMovie = await _context.Movies.FirstOrDefaultAsync(m => m.MovieID == movieId);
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

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateTheatreAsync(int theatreId, Theatre updatedTheatre)
    {
        var existingTheatre = await _context.Theatres.FirstOrDefaultAsync(t => t.TheatreID == theatreId);
        if (existingTheatre == null)
        {
            return false;
        }

        existingTheatre.TheatreName = updatedTheatre.TheatreName;
        existingTheatre.NumberofSeats = updatedTheatre.NumberofSeats;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateCustomerAsync(int customerId, Customer updatedCustomer)
    {
        var existingCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerID == customerId);
        if (existingCustomer == null)
        {
            return false;
        }

        existingCustomer.CustomerName = updatedCustomer.CustomerName;
        existingCustomer.City = updatedCustomer.City;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateLoginAsync(string loginId, LoginDetails updatedLogin)
    {
        var existingLogin = await _context.LoginDetails.FirstOrDefaultAsync(l => l.LoginID == loginId);
        if (existingLogin == null)
        {
            return false;
        }

        existingLogin.Password = updatedLogin.Password;
        existingLogin.LoginType = updatedLogin.LoginType;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateShowAsync(int showId, Show updatedShow)
    {
        var existingShow = await _context.Shows.FirstOrDefaultAsync(s => s.ShowID == showId);
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

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateBookingAsync(int bookingId, Booking updatedBooking)
    {
        var existingBooking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingID == bookingId);
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

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteMovieAsync(string movieId)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.MovieID == movieId);
        if (movie == null)
        {
            return false;
        }

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTheatreAsync(int theatreId)
    {
        var theatre = await _context.Theatres.FirstOrDefaultAsync(t => t.TheatreID == theatreId);
        if (theatre == null)
        {
            return false;
        }

        _context.Theatres.Remove(theatre);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCustomerAsync(int customerId)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerID == customerId);
        if (customer == null)
        {
            return false;
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteLoginAsync(string loginId)
    {
        var login = await _context.LoginDetails.FirstOrDefaultAsync(l => l.LoginID == loginId);
        if (login == null)
        {
            return false;
        }

        _context.LoginDetails.Remove(login);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteShowAsync(int showId)
    {
        var show = await _context.Shows.FirstOrDefaultAsync(s => s.ShowID == showId);
        if (show == null)
        {
            return false;
        }

        _context.Shows.Remove(show);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteBookingAsync(int bookingId)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingID == bookingId);
        if (booking == null)
        {
            return false;
        }

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();
        return true;
    }
}
