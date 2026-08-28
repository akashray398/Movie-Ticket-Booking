using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Database;
using MovieTicketBooking.Middleware;
using MovieTicketBooking.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDatabase") ?? DatabaseConnectionSettings.DefaultConnectionString));
builder.Services.AddScoped<MovieDatabaseService>();
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<TheatreService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ShowService>();
builder.Services.AddScoped<ApiBookingService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var database = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
	await DatabaseInitializer.SeedAsync(database);
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.MapControllers();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();

