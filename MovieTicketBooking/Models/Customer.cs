namespace MovieTicketBooking.Models;

public class Customer
{
    private static readonly HashSet<int> GeneratedIds = new();

    public int CustomerID { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

    private Customer() { }

    public Customer(string customerName, string city)
    {
        CustomerName = customerName;
        City = city;
        CustomerID = GenerateCustomerID();
    }

    private static int GenerateCustomerID()
    {
        int customerId;

        do
        {
            customerId = Random.Shared.Next(1000, 1000000);
        }
        while (!GeneratedIds.Add(customerId));

        return customerId;
    }

    public void DisplayCustomerDetails()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Customer Details");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Customer ID   : {CustomerID}");
        Console.WriteLine($"Customer Name : {CustomerName}");
        Console.WriteLine($"City          : {City}");
        Console.WriteLine("----------------------------------------");
    }
}
