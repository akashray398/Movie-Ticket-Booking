using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Database;

public class MovieDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Theatre> Theatres { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<LoginDetails> LoginDetails { get; set; }
    public DbSet<Show> Shows { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(m => m.MovieID);
            entity.Property(m => m.MovieID).HasMaxLength(50).ValueGeneratedNever();
            entity.Property(m => m.MovieName).IsRequired().HasMaxLength(200);
            entity.Property(m => m.DirectorName).HasMaxLength(200);
            entity.Property(m => m.ProducerName).HasMaxLength(200);
            entity.Property(m => m.Duration).HasColumnType("float");
            entity.Property(m => m.Story).HasMaxLength(1000);
            entity.Property(m => m.Genre).HasMaxLength(100);
            entity.Property(m => m.Language).HasMaxLength(100);
        });

        modelBuilder.Entity<Theatre>(entity =>
        {
            entity.HasKey(t => t.TheatreID);
            entity.Property(t => t.TheatreID).ValueGeneratedNever();
            entity.Property(t => t.TheatreName).IsRequired().HasMaxLength(200);
            entity.Property(t => t.NumberofSeats).IsRequired();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(c => c.CustomerID);
            entity.Property(c => c.CustomerID).ValueGeneratedNever();
            entity.Property(c => c.CustomerName).IsRequired().HasMaxLength(200);
            entity.Property(c => c.City).HasMaxLength(200);
        });

        modelBuilder.Entity<LoginDetails>(entity =>
        {
            entity.HasKey(l => l.LoginID);
            entity.Property(l => l.LoginID).HasMaxLength(100).ValueGeneratedNever();
            entity.Property(l => l.Password).IsRequired().HasMaxLength(200);
            entity.Property(l => l.LoginType).IsRequired().HasMaxLength(20);
        });

        modelBuilder.Entity<Show>(entity =>
        {
            entity.HasKey(s => s.ShowID);
            entity.Property(s => s.ShowID).ValueGeneratedNever();
            entity.Property(s => s.MovieID).IsRequired().HasMaxLength(50);
            entity.Property(s => s.TheatreID).IsRequired();
            entity.Property(s => s.StartDate).IsRequired();
            entity.Property(s => s.EndDate).IsRequired();
            entity.Property(s => s.PlatinumSeatRate).HasColumnType("decimal(18,2)");
            entity.Property(s => s.GoldSeatRate).HasColumnType("decimal(18,2)");
            entity.Property(s => s.SilverSeatRate).HasColumnType("decimal(18,2)");

            entity.HasOne(s => s.Movie)
                .WithMany(m => m.Shows)
                .HasForeignKey(s => s.MovieID)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(s => s.Theatre)
                .WithMany(t => t.Shows)
                .HasForeignKey(s => s.TheatreID)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(b => b.BookingID);
            entity.Property(b => b.BookingID).ValueGeneratedNever();
            entity.Property(b => b.BookingDate).IsRequired();
            entity.Property(b => b.ShowID).IsRequired();
            entity.Property(b => b.CustomerName).IsRequired().HasMaxLength(200);
            entity.Property(b => b.NumberOfSeats).IsRequired();
            entity.Property(b => b.SeatType).IsRequired().HasMaxLength(50);
            entity.Property(b => b.Amount).HasColumnType("decimal(18,2)");
            entity.Property(b => b.Email).HasMaxLength(200);
            entity.Property(b => b.BookingStatus).IsRequired().HasMaxLength(50);
            var seatNumbersProperty = entity.Property(b => b.SeatNumbers)
                .HasConversion(
                    v => string.Join("|", v),
                    v => string.IsNullOrWhiteSpace(v)
                        ? new List<int>()
                        : v.Split('|', StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToList())
                .HasMaxLength(500);
            seatNumbersProperty.Metadata.SetValueComparer(new ValueComparer<List<int>>(
                    (left, right) => left != null && right != null && left.SequenceEqual(right),
                    seats => seats == null ? 0 : seats.Aggregate(0, (hash, seat) => HashCode.Combine(hash, seat)),
                    seats => seats == null ? new List<int>() : seats.ToList()));

            entity.HasOne(b => b.Show)
                .WithMany(s => s.Bookings)
                .HasForeignKey(b => b.ShowID)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
