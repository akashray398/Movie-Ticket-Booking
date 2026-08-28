using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.DTOs;
using MovieTicketBooking.Services;

namespace MovieTicketBooking.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly MovieService _service;
    public MoviesController(MovieService service) => _service = service;
    [HttpGet] public Task<List<MovieDto>> GetAll() => _service.GetAllAsync();
    [HttpGet("search")] public Task<List<MovieDto>> Search([FromQuery] string name = "") => _service.SearchAsync(name);
    [HttpGet("{id}")] public async Task<IActionResult> Get(string id) => (await _service.GetAsync(id)) is { } movie ? Ok(movie) : NotFound();
    [HttpPost] public async Task<IActionResult> Create(CreateMovieDto dto) { var movie = await _service.CreateAsync(dto); return CreatedAtAction(nameof(Get), new { id = movie.MovieID }, movie); }
    [HttpPut("{id}")] public async Task<IActionResult> Update(string id, CreateMovieDto dto) => await _service.UpdateAsync(id, dto) ? NoContent() : NotFound();
    [HttpDelete("{id}")] public async Task<IActionResult> Delete(string id) => await _service.DeleteAsync(id) ? NoContent() : NotFound();
}

[ApiController]
[Route("api/[controller]")]
public class TheatresController : ControllerBase
{
    private readonly TheatreService _service;
    public TheatresController(TheatreService service) => _service = service;
    [HttpGet] public Task<List<TheatreDto>> GetAll() => _service.GetAllAsync();
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id) => (await _service.GetAsync(id)) is { } theatre ? Ok(theatre) : NotFound();
    [HttpPost] public async Task<IActionResult> Create(CreateTheatreDto dto) { var theatre = await _service.CreateAsync(dto); return CreatedAtAction(nameof(Get), new { id = theatre.TheatreID }, theatre); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, CreateTheatreDto dto) => await _service.UpdateAsync(id, dto) ? NoContent() : NotFound();
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id) => await _service.DeleteAsync(id) ? NoContent() : NotFound();
}

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly CustomerService _service;
    public CustomersController(CustomerService service) => _service = service;
    [HttpGet] public Task<List<CustomerDto>> GetAll() => _service.GetAllAsync();
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id) => (await _service.GetAsync(id)) is { } customer ? Ok(customer) : NotFound();
    [HttpPost] public async Task<IActionResult> Create(CreateCustomerDto dto) { var customer = await _service.CreateAsync(dto); return CreatedAtAction(nameof(Get), new { id = customer.CustomerID }, customer); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, CreateCustomerDto dto) => await _service.UpdateAsync(id, dto) ? NoContent() : NotFound();
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id) => await _service.DeleteAsync(id) ? NoContent() : NotFound();
}

[ApiController]
[Route("api/[controller]")]
public class ShowsController : ControllerBase
{
    private readonly ShowService _service;
    public ShowsController(ShowService service) => _service = service;
    [HttpGet] public Task<List<ShowDto>> GetAll() => _service.GetAllAsync();
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id) => (await _service.GetAsync(id)) is { } show ? Ok(show) : NotFound();
    [HttpGet("{id:int}/available-seats")] public Task<List<int>> AvailableSeats(int id) => _service.AvailableSeatsAsync(id);
    [HttpPost] public async Task<IActionResult> Create(CreateShowDto dto) { var show = await _service.CreateAsync(dto); return CreatedAtAction(nameof(Get), new { id = show.ShowID }, show); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, CreateShowDto dto) => await _service.UpdateAsync(id, dto) ? NoContent() : NotFound();
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id) => await _service.DeleteAsync(id) ? NoContent() : NotFound();
}

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly ApiBookingService _service;
    public BookingsController(ApiBookingService service) => _service = service;
    [HttpGet] public Task<List<BookingDto>> GetAll() => _service.GetAllAsync();
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id) => (await _service.GetAsync(id)) is { } booking ? Ok(booking) : NotFound();
    [HttpGet("show/{showId:int}/available-seats")] public Task<List<int>> AvailableSeats(int showId) => _service.AvailableSeatsAsync(showId);
    [HttpPost] public async Task<IActionResult> Create(CreateBookingDto dto) { var booking = await _service.CreateAsync(dto); return CreatedAtAction(nameof(Get), new { id = booking.BookingID }, booking); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, CreateBookingDto dto) => await _service.UpdateAsync(id, dto) ? NoContent() : NotFound();
    [HttpPut("{id:int}/cancel")] public async Task<IActionResult> Cancel(int id) => await _service.CancelAsync(id) ? Ok(new { message = "Booking cancelled." }) : NotFound();
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id) => await _service.DeleteAsync(id) ? NoContent() : NotFound();
}

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly LoginService _service;
    public LoginController(LoginService service) => _service = service;
    [HttpPost] public async Task<IActionResult> Login(LoginRequestDto request) => await _service.AuthenticateAsync(request) is { } login ? Ok(new { success = true, login.LoginID, login.LoginType }) : Unauthorized(new { success = false, message = "Invalid LoginID or password." });
}
