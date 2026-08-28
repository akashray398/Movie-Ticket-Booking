namespace MovieTicketBooking.Models;

public class Show
{
    private static readonly HashSet<int> GeneratedIds = new();

    public int ShowID { get; set; }
    // MovieID is kept as string because Movie.MovieID is specified as
    // string in the assignment. The assignment's Show specification
    // mentions int, which is inconsistent.
    public string MovieID { get; set; } = string.Empty;
    public int TheatreID { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal PlatinumSeatRate { get; set; }
    public decimal GoldSeatRate { get; set; }
    public decimal SilverSeatRate { get; set; }

    public Movie? Movie { get; set; }
    public Theatre? Theatre { get; set; }
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    private Show() { }

    public Show(string movieID, int theatreID, DateTime startDate, DateTime endDate, decimal platinumSeatRate, decimal goldSeatRate, decimal silverSeatRate)
    {
        ShowID = GenerateShowID();
        MovieID = movieID;
        TheatreID = theatreID;
        StartDate = startDate;
        EndDate = endDate;
        PlatinumSeatRate = platinumSeatRate;
        GoldSeatRate = goldSeatRate;
        SilverSeatRate = silverSeatRate;
    }

    private static int GenerateShowID()
    {
        int showId;

        do
        {
            showId = Random.Shared.Next(1000, 1000000);
        }
        while (!GeneratedIds.Add(showId));

        return showId;
    }

    public void DisplayShowDetails()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Show Details");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Show ID                : {ShowID}");
        Console.WriteLine($"Movie ID              : {MovieID}");
        Console.WriteLine($"Theatre ID            : {TheatreID}");
        Console.WriteLine($"Start Date            : {StartDate:dd-MM-yyyy}");
        Console.WriteLine($"End Date              : {EndDate:dd-MM-yyyy}");
        Console.WriteLine($"Platinum Seat Rate    : {PlatinumSeatRate}");
        Console.WriteLine($"Gold Seat Rate        : {GoldSeatRate}");
        Console.WriteLine($"Silver Seat Rate      : {SilverSeatRate}");
        Console.WriteLine("----------------------------------------");
    }
}
