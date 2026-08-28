using MovieTicketBooking.Exceptions;

namespace MovieTicketBooking.Models;

public class Movie
{
    private static readonly List<string> ValidLanguages = new()
    {
        "English",
        "Hindi",
        "Punjabi",
        "Tamil",
        "Telugu",
        "Malayalam",
        "Kannada",
        "Bengali",
        "Marathi"
    };

    public string MovieID { get; set; } = string.Empty;
    public string MovieName { get; set; } = string.Empty;
    public string DirectorName { get; set; } = string.Empty;
    public string ProducerName { get; set; } = string.Empty;
    public double Duration { get; set; }
    public string Story { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;

    public ICollection<Show> Shows { get; set; } = new List<Show>();

    private Movie() { }

    public Movie(string movieName, string directorName, string producerName, double duration, string story, string genre, string language)
    {
        ValidateLanguage(language);
        ValidateDuration(duration);

        MovieName = movieName;
        DirectorName = directorName;
        ProducerName = producerName;
        Duration = duration;
        Story = story;
        Genre = genre;
        Language = language;

        MovieID = GenerateMovieID();
    }

    private static void ValidateLanguage(string language)
    {
        if (string.IsNullOrWhiteSpace(language))
        {
            throw new InvalidLanguageException();
        }

        bool isValid = ValidLanguages.Any(validLanguage =>
            string.Equals(validLanguage, language.Trim(), StringComparison.OrdinalIgnoreCase));

        if (!isValid)
        {
            throw new InvalidLanguageException();
        }
    }

    private static void ValidateDuration(double duration)
    {
        if (duration <= 0)
        {
            throw new InvalidDurationException();
        }
    }

    private string GenerateMovieID()
    {
        string moviePrefix = GetFirstTwoCharacters(MovieName);
        string producerPrefix = GetFirstTwoCharacters(ProducerName);
        string genrePrefix = GetFirstTwoCharacters(Genre);
        string languagePrefix = GetFirstTwoCharacters(Language);

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

    public void DisplayMovieDetails()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Movie Details");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Movie ID       : {MovieID}");
        Console.WriteLine($"Movie Name     : {MovieName}");
        Console.WriteLine($"Director       : {DirectorName}");
        Console.WriteLine($"Producer       : {ProducerName}");
        Console.WriteLine($"Duration       : {Duration} hours");
        Console.WriteLine($"Story          : {Story}");
        Console.WriteLine($"Genre          : {Genre}");
        Console.WriteLine($"Language       : {Language}");
        Console.WriteLine("----------------------------------------");
    }
}
