using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Database;
using MovieTicketBooking.DTOs;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Services;

public class MovieService
{
    private readonly MovieDatabaseService _database;
    public MovieService(MovieDatabaseService database) => _database = database;

    public async Task<List<MovieDto>> GetAllAsync() => (await _database.GetAllMoviesAsync()).Select(ToDto).ToList();
    public async Task<MovieDto?> GetAsync(string id) => (await _database.GetMovieByIdAsync(id)) is { } movie ? ToDto(movie) : null;
    public async Task<List<MovieDto>> SearchAsync(string name) => (await _database.SearchMoviesAsync(name)).Select(ToDto).ToList();
    public async Task<MovieDto> CreateAsync(CreateMovieDto dto) { var movie = new Movie(dto.MovieName, dto.DirectorName, dto.ProducerName, dto.Duration, dto.Story, dto.Genre, dto.Language); await _database.AddMovieAsync(movie); return ToDto(movie); }
    public async Task<bool> UpdateAsync(string id, CreateMovieDto dto) => await _database.UpdateMovieAsync(id, new Movie(dto.MovieName, dto.DirectorName, dto.ProducerName, dto.Duration, dto.Story, dto.Genre, dto.Language));
    public Task<bool> DeleteAsync(string id) => _database.DeleteMovieAsync(id);
    private static MovieDto ToDto(Movie movie) => new(movie.MovieID, movie.MovieName, movie.DirectorName, movie.ProducerName, movie.Duration, movie.Story, movie.Genre, movie.Language);
}

public class TheatreService
{
    private readonly MovieDatabaseService _database;
    public TheatreService(MovieDatabaseService database) => _database = database;
    public async Task<List<TheatreDto>> GetAllAsync() => (await _database.GetAllTheatresAsync()).Select(ToDto).ToList();
    public async Task<TheatreDto?> GetAsync(int id) => (await _database.GetTheatreByIdAsync(id)) is { } theatre ? ToDto(theatre) : null;
    public async Task<TheatreDto> CreateAsync(CreateTheatreDto dto) { var theatre = new Theatre(dto.TheatreName, dto.NumberofSeats); await _database.AddTheatreAsync(theatre); return ToDto(theatre); }
    public Task<bool> UpdateAsync(int id, CreateTheatreDto dto) => _database.UpdateTheatreAsync(id, new Theatre(dto.TheatreName, dto.NumberofSeats));
    public Task<bool> DeleteAsync(int id) => _database.DeleteTheatreAsync(id);
    private static TheatreDto ToDto(Theatre theatre) => new(theatre.TheatreID, theatre.TheatreName, theatre.NumberofSeats);
}

public class CustomerService
{
    private readonly MovieDatabaseService _database;
    public CustomerService(MovieDatabaseService database) => _database = database;
    public async Task<List<CustomerDto>> GetAllAsync() => (await _database.GetAllCustomersAsync()).Select(ToDto).ToList();
    public async Task<CustomerDto?> GetAsync(int id) => (await _database.GetCustomerByIdAsync(id)) is { } customer ? ToDto(customer) : null;
    public async Task<CustomerDto> CreateAsync(CreateCustomerDto dto) { var customer = new Customer(dto.CustomerName, dto.City); await _database.AddCustomerWithLoginAsync(customer); return ToDto(customer); }
    public Task<bool> UpdateAsync(int id, CreateCustomerDto dto) => _database.UpdateCustomerAsync(id, new Customer(dto.CustomerName, dto.City));
    public Task<bool> DeleteAsync(int id) => _database.DeleteCustomerAsync(id);
    private static CustomerDto ToDto(Customer customer) => new(customer.CustomerID, customer.CustomerName, customer.City);
}

public class ShowService
{
    private readonly MovieDatabaseService _database;
    public ShowService(MovieDatabaseService database) => _database = database;
    public async Task<List<ShowDto>> GetAllAsync() => (await _database.GetAllShowsAsync()).Select(ToDto).ToList();
    public async Task<ShowDto?> GetAsync(int id) => (await _database.GetShowByIdAsync(id)) is { } show ? ToDto(show) : null;
    public async Task<ShowDto> CreateAsync(CreateShowDto dto) { ValidateDates(dto); if (await _database.GetMovieByIdAsync(dto.MovieID) == null || await _database.GetTheatreByIdAsync(dto.TheatreID) == null) throw new KeyNotFoundException("Movie or theatre not found."); var show = new Show(dto.MovieID, dto.TheatreID, dto.StartDate, dto.EndDate, dto.PlatinumSeatRate, dto.GoldSeatRate, dto.SilverSeatRate); await _database.AddShowAsync(show); return ToDto(await _database.GetShowByIdAsync(show.ShowID) ?? show); }
    public async Task<bool> UpdateAsync(int id, CreateShowDto dto) { ValidateDates(dto); if (await _database.GetMovieByIdAsync(dto.MovieID) == null || await _database.GetTheatreByIdAsync(dto.TheatreID) == null) throw new KeyNotFoundException("Movie or theatre not found."); return await _database.UpdateShowAsync(id, new Show(dto.MovieID, dto.TheatreID, dto.StartDate, dto.EndDate, dto.PlatinumSeatRate, dto.GoldSeatRate, dto.SilverSeatRate)); }
    public Task<bool> DeleteAsync(int id) => _database.DeleteShowAsync(id);
    public Task<List<int>> AvailableSeatsAsync(int id) => _database.GetAvailableSeatsAsync(id);
    private static void ValidateDates(CreateShowDto dto) { if (dto.EndDate <= dto.StartDate) throw new ArgumentException("EndDate must be after StartDate."); }
    private static ShowDto ToDto(Show show) => new(show.ShowID, show.MovieID, show.Movie?.MovieName, show.TheatreID, show.Theatre?.TheatreName, show.StartDate, show.EndDate, show.PlatinumSeatRate, show.GoldSeatRate, show.SilverSeatRate);
}

public class ApiBookingService
{
    private readonly MovieDatabaseService _database;
    public ApiBookingService(MovieDatabaseService database) => _database = database;
    public async Task<List<BookingDto>> GetAllAsync() => (await _database.GetAllBookingsAsync()).Select(ToDto).ToList();
    public async Task<BookingDto?> GetAsync(int id) => (await _database.GetBookingByIdAsync(id)) is { } booking ? ToDto(booking) : null;
    public async Task<BookingDto> CreateAsync(CreateBookingDto dto) { var show = await _database.GetShowByIdAsync(dto.ShowID) ?? throw new KeyNotFoundException("Show not found."); var seats = dto.SeatNumbers.Count == 0 ? null : dto.SeatNumbers; var available = await _database.GetAvailableSeatsAsync(dto.ShowID); var selected = seats ?? available.Take(dto.NumberOfSeats).ToList(); if (selected.Count != dto.NumberOfSeats || selected.Distinct().Count() != selected.Count || selected.Any(s => !available.Contains(s))) throw new InvalidOperationException("One or more selected seats are unavailable."); var booking = new Booking(dto.ShowID, dto.CustomerName, dto.NumberOfSeats, dto.SeatType, dto.Email) { SeatNumbers = selected, Amount = CalculateAmount(show, dto.SeatType, dto.NumberOfSeats) }; await _database.AddBookingAsync(booking); return ToDto(await _database.GetBookingByIdAsync(booking.BookingID) ?? booking); }
    public async Task<bool> UpdateAsync(int id, CreateBookingDto dto)
    {
        var existing = await _database.GetBookingByIdAsync(id) ?? throw new KeyNotFoundException("Booking not found.");
        var show = await _database.GetShowByIdAsync(dto.ShowID) ?? throw new KeyNotFoundException("Show not found.");
        var available = await _database.GetAvailableSeatsAsync(dto.ShowID);
        if (existing.ShowID == dto.ShowID)
        {
            available.AddRange(existing.SeatNumbers);
        }

        if (dto.SeatNumbers.Count != dto.NumberOfSeats || dto.SeatNumbers.Distinct().Count() != dto.SeatNumbers.Count || dto.SeatNumbers.Any(seat => !available.Contains(seat)))
        {
            throw new InvalidOperationException("One or more selected seats are unavailable.");
        }

        var updated = new Booking(dto.ShowID, dto.CustomerName, dto.NumberOfSeats, dto.SeatType, dto.Email) { BookingID = existing.BookingID, BookingDate = existing.BookingDate, SeatNumbers = dto.SeatNumbers, Amount = CalculateAmount(show, dto.SeatType, dto.NumberOfSeats), BookingStatus = existing.BookingStatus };
        return await _database.UpdateBookingAsync(id, updated);
    }
    public Task<bool> DeleteAsync(int id) => _database.DeleteBookingAsync(id);
    public Task<bool> CancelAsync(int id) => _database.CancelBookingAsync(id);
    public Task<List<int>> AvailableSeatsAsync(int id) => _database.GetAvailableSeatsAsync(id);
    private static decimal CalculateAmount(Show show, string seatType, int count) => BookingService.NormalizeSeatType(seatType) switch { "Platinum" => show.PlatinumSeatRate * count, "Gold" => show.GoldSeatRate * count, "Silver" => show.SilverSeatRate * count, _ => throw new ArgumentException("Invalid seat type.") };
    private static BookingDto ToDto(Booking b) => new(b.BookingID, b.ShowID, b.Show?.Movie?.MovieName, b.Show?.Theatre?.TheatreName, b.BookingDate, b.CustomerName, b.NumberOfSeats, b.SeatType, b.Amount, b.Email, b.BookingStatus, b.SeatNumbers);
}

public class LoginService
{
    private readonly MovieDatabaseService _database;
    public LoginService(MovieDatabaseService database) => _database = database;
    public async Task<LoginDetails?> AuthenticateAsync(LoginRequestDto request) { var login = await _database.GetLoginByIdAsync(request.LoginID); return login != null && login.Password == request.Password ? login : null; }
}
