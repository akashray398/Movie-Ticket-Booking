namespace MovieTicketBooking.Models;

public class LoginDetails
{
    public string LoginID { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string LoginType { get; set; } = string.Empty;

    private LoginDetails() { }

    public LoginDetails(int customerID)
    {
        LoginID = customerID.ToString();
        Password = LoginID;
        LoginType = "C";
    }

    public LoginDetails(bool isAdmin)
    {
        if (isAdmin)
        {
            LoginID = "MOVIEADMIN";
            Password = "MOVIEADMIN";
            LoginType = "A";
        }
        else
        {
            LoginID = string.Empty;
            Password = string.Empty;
            LoginType = string.Empty;
        }
    }

    public void DisplayLoginDetails()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Login Details");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Login ID   : {LoginID}");
        Console.WriteLine($"Password   : {Password}");
        Console.WriteLine($"Login Type : {LoginType}");
        Console.WriteLine("----------------------------------------");
    }
}
