namespace MovieTicketBooking.Models;

public class Theatre
{
    private static readonly HashSet<int> GeneratedIds = new();

    public int TheatreID { get; set; }
    public string TheatreName { get; set; } = string.Empty;
    public int NumberofSeats { get; set; }

    public ICollection<Show> Shows { get; set; } = new List<Show>();

    private Theatre() { }

    public Theatre(string theatreName, int numberofSeats)
    {
        TheatreName = theatreName;
        NumberofSeats = numberofSeats;
        TheatreID = GenerateTheatreID();
    }

    private static int GenerateTheatreID()
    {
        int theatreId;

        do
        {
            theatreId = Random.Shared.Next(1000, 1000000);
        }
        while (!GeneratedIds.Add(theatreId));

        return theatreId;
    }

    public void DisplayTheatreDetails()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Theatre Details");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Theatre ID     : {TheatreID}");
        Console.WriteLine($"Theatre Name   : {TheatreName}");
        Console.WriteLine($"Number of Seats: {NumberofSeats}");
        Console.WriteLine("----------------------------------------");
    }
}
