using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MovieTicketBooking.Database;

public class MovieDbContextFactory : IDesignTimeDbContextFactory<MovieDbContext>
{
    public MovieDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MovieDbContext>();
        optionsBuilder.UseSqlServer(DatabaseConnectionSettings.DefaultConnectionString);

        return new MovieDbContext(optionsBuilder.Options);
    }
}
