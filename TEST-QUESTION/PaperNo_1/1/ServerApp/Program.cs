using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

var customers = new List<Customer>
{
    new(1, "Nguyen Van A", "a@gmail.com", "Ha Noi"),
    new(2, "Tran Thi B", "b@gmail.com", "Da Nang"),
    new(3, "Le Van C", "c@gmail.com", "Ha Noi"),
    new(4, "Pham Thi D", "d@gmail.com", "Hai Phong"),
    new(5, "Hoang Thi E", "e@gmail.com", "Da Nang")
};

JsonSerializerOptions jsonOptions = new() { WriteIndented = true };

TcpListener server = new(IPAddress.Parse("127.0.0.1"), 5101);
server.Start();
Console.WriteLine("CustomerOrderServer is running on 127.0.0.1:5101");
Console.WriteLine("Commands: ALL or a city name such as Ha Noi, Da Nang, Hai Phong");

while (true)
{
    TcpClient client = await server.AcceptTcpClientAsync();
    _ = Task.Run(() => HandleClientAsync(client));
}

async Task HandleClientAsync(TcpClient client)
{
    using (client)
    {
        using NetworkStream stream = client.GetStream();

        try
        {
            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer);
            if (bytesRead <= 0)
            {
                return;
            }

            string command = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
            string response = BuildResponse(command);
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            await stream.WriteAsync(responseBytes);
            Console.WriteLine($"Handled command: {command}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

string BuildResponse(string command)
{
    if (string.Equals(command, "ALL", StringComparison.OrdinalIgnoreCase))
    {
        List<Customer> result = customers
            .OrderBy(c => c.CustomerId)
            .ToList();

        return JsonSerializer.Serialize(new CustomerResponse(
            true,
            $"Returned all {result.Count} customer(s).",
            result.Count,
            result), jsonOptions);
    }

    if (!string.IsNullOrWhiteSpace(command))
    {
        string city = command.Trim();
        List<Customer> result = customers
            .Where(c => string.Equals(c.City, city, StringComparison.OrdinalIgnoreCase))
            .OrderBy(c => c.CustomerId)
            .ToList();

        return JsonSerializer.Serialize(new CustomerResponse(
            true,
            $"Found {result.Count} customer(s) in {city}.",
            result.Count,
            result), jsonOptions);
    }

    return JsonSerializer.Serialize(new ErrorResponse(false, "Invalid command. Use ALL or a city name."), jsonOptions);
}

record Customer(int CustomerId, string CustomerName, string Email, string City);
record CustomerResponse(bool Success, string Message, int Count, List<Customer> Data);
record ErrorResponse(bool Success, string Message);
