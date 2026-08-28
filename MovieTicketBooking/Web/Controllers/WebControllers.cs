using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.DTOs;
using MovieTicketBooking.Services;

namespace MovieTicketBooking.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}

public class MoviesController : Controller
{
    private readonly MovieService _service;
    public MoviesController(MovieService service) => _service = service;
    public async Task<IActionResult> Index() => View(await _service.GetAllAsync());
    public async Task<IActionResult> Details(string id) => (await _service.GetAsync(id)) is { } movie ? View(movie) : NotFound();
}

public class TheatresController : Controller
{
    private readonly TheatreService _service;
    public TheatresController(TheatreService service) => _service = service;
    public async Task<IActionResult> Index() => View(await _service.GetAllAsync());
}

public class ShowsController : Controller
{
    private readonly ShowService _service;
    public ShowsController(ShowService service) => _service = service;
    public async Task<IActionResult> Index() => View(await _service.GetAllAsync());
    public async Task<IActionResult> AvailableSeats(int id) => View("AvailableSeats", await _service.AvailableSeatsAsync(id));
}

public class CustomersController : Controller
{
    private readonly CustomerService _service;
    public CustomersController(CustomerService service) => _service = service;
    public async Task<IActionResult> Index() => View(await _service.GetAllAsync());
}

public class BookingsController : Controller
{
    private readonly ApiBookingService _service;
    private readonly ShowService _shows;
    public BookingsController(ApiBookingService service, ShowService shows) { _service = service; _shows = shows; }
    public async Task<IActionResult> Index() => View(await _service.GetAllAsync());
    [HttpGet] public async Task<IActionResult> Create() => View(new BookingCreateViewModel { Shows = await _shows.GetAllAsync() });
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Create(BookingCreateViewModel model)
    {
        if (!string.IsNullOrWhiteSpace(model.SeatNumbersText))
        {
            if (!model.SeatNumbersText.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).All(value => int.TryParse(value, out _)))
            {
                ModelState.AddModelError(nameof(model.SeatNumbersText), "Seat numbers must be comma-separated numbers.");
            }
            else
            {
                model.Booking.SeatNumbers = model.SeatNumbersText.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToList();
            }
        }

        if (!ModelState.IsValid)
        {
            model.Shows = await _shows.GetAllAsync();
            return View(model);
        }

        var booking = await _service.CreateAsync(model.Booking);
        return RedirectToAction(nameof(Confirmation), new { id = booking.BookingID });
    }
    public async Task<IActionResult> Confirmation(int id) => (await _service.GetAsync(id)) is { } booking ? View(booking) : NotFound();
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Cancel(int id) { await _service.CancelAsync(id); return RedirectToAction(nameof(Index)); }
}

public class AdminController : Controller
{
    private readonly MovieService _movies;
    private readonly TheatreService _theatres;
    private readonly CustomerService _customers;
    private readonly ShowService _shows;
    private readonly ApiBookingService _bookings;
    public AdminController(MovieService movies, TheatreService theatres, CustomerService customers, ShowService shows, ApiBookingService bookings) { _movies = movies; _theatres = theatres; _customers = customers; _shows = shows; _bookings = bookings; }
    public async Task<IActionResult> Index() => View(new AdminDashboardViewModel { Movies = (await _movies.GetAllAsync()).Count, Theatres = (await _theatres.GetAllAsync()).Count, Customers = (await _customers.GetAllAsync()).Count, Shows = (await _shows.GetAllAsync()).Count, Bookings = (await _bookings.GetAllAsync()).Count });
}

public class LoginController : Controller
{
    private readonly LoginService _service;
    public LoginController(LoginService service) => _service = service;
    [HttpGet] public IActionResult Index() => View(new LoginRequestDto());
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Index(LoginRequestDto request) { if (!ModelState.IsValid) return View(request); var login = await _service.AuthenticateAsync(request); if (login == null) { ModelState.AddModelError(string.Empty, "Invalid LoginID or password."); return View(request); } HttpContext.Session.SetString("LoginID", login.LoginID); HttpContext.Session.SetString("LoginType", login.LoginType); return RedirectToAction("Index", "Home"); }
    [HttpPost, ValidateAntiForgeryToken] public IActionResult Logout() { HttpContext.Session.Clear(); return RedirectToAction("Index", "Home"); }
}
