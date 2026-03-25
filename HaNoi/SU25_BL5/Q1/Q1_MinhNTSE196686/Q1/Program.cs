using System.Net.Sockets;
using System.Text.Json;

while (true)
{
    Console.Write("Enter Book ID (or press Enter to exit): ");
    string? input = Console.ReadLine();

    if (string.IsNullOrEmpty(input))
    {
        Console.WriteLine("Goodbye ! Book tracking client is shutting down");
        break;
    }

    if (!int.TryParse(input, out int bookId) || bookId <= 0)
    {
            Console.WriteLine("Invalid input! Please enter a valid Book ID (positive integer).");
            continue;
    }

    try
    {
        using TcpClient client = new();
        client.Connect("127.0.0.1", 4000);
        using NetworkStream stream = client.GetStream();
        using StreamWriter writer = new(stream) { AutoFlush = true };
        using StreamReader reader = new(stream);

        writer.WriteLine(bookId);
        string? json = reader.ReadLine();
        Console.WriteLine(json);
        ServerResponse? response = JsonSerializer.Deserialize<ServerResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (response?.BookExists == false)
        {
            Console.WriteLine($"Book with ID {bookId} does not exist");
        }

        if (response?.BookExists == true && response.BorrowerRecords.Count == 0)
        {
            Console.WriteLine($"No borrower records found for Book ID {bookId}");
        }

        if (response?.BorrowerRecords.Count > 0)
        {
            Console.WriteLine($"=== Borrower History for Book ID: {bookId}");
            var records = response.BorrowerRecords;
            for (int i = 0 ; i < records.Count; i++)
            {
                var record = records[i];
                Console.WriteLine($"Reader ID: {record.ReaderID}");
                Console.WriteLine($"Full Name: {record.FullName}");
                Console.WriteLine($"Email: {record.Email}");
                Console.WriteLine($"Borrow Date: {record.BorrowDate}");
                Console.WriteLine($"Return Date: {record.ReturnDate}");
                Console.WriteLine($"Status: {record.Status}");
                if (i < records.Count - 1)
                {
                    System.Console.WriteLine("---");
                }
            }
        }


    }
    catch (SocketException)
    {
        Console.WriteLine("Book tracking server is not running. Please try again later.");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Error: {e.Message}");
    }
}

public class BorrowerRecord
{
    public int ReaderID { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string Status { get; set; } = "";
}

public class ServerResponse
{
    public bool BookExists { get; set; }
    public string BookTitle { get; set; } = "";
    public List<BorrowerRecord> BorrowerRecords { get; set; } = new List<BorrowerRecord>();
}