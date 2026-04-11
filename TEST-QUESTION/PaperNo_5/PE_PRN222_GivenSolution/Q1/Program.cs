using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

List<Product> products =
[
    new(1, "Laptop", "Electronics", 1500m, 12, "Available"),
    new(2, "Smartphone", "Electronics", 900m, 20, "Available"),
    new(3, "Jeans", "Clothing", 45m, 35, "Available"),
    new(4, "Jacket", "Clothing", 75m, 14, "Low Stock"),
    new(5, "Rice", "Food", 18m, 40, "Available"),
    new(6, "Milk", "Food", 6m, 25, "Available")
];

JsonSerializerOptions jsonOptions = new()
{
    WriteIndented = true
};

TcpListener server = new(IPAddress.Parse("127.0.0.1"), 5505);
server.Start();

Console.WriteLine("Product Category Server is running on 127.0.0.1:5505");
Console.WriteLine("Commands: ALL, Electronics, Clothing, Food");

try
{
    while (true)
    {
        TcpClient client = await server.AcceptTcpClientAsync();
        _ = Task.Run(() => HandleClientAsync(client));
    }
}
finally
{
    server.Stop();
}

async Task HandleClientAsync(TcpClient client)
{
    using (client)
    using (NetworkStream stream = client.GetStream())
    {
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
        List<Product> result = products.OrderBy(p => p.ProductId).ToList();
        return JsonSerializer.Serialize(new ProductResponse(true, $"Returned all {result.Count} product(s).", result.Count, result), jsonOptions);
    }

    string[] validCategories = ["Electronics", "Clothing", "Food"];
    bool isValidCategory = validCategories.Any(category => string.Equals(category, command, StringComparison.OrdinalIgnoreCase));
    if (!isValidCategory)
    {
        return JsonSerializer.Serialize(new ErrorResponse(false, "Invalid command. Use ALL, Electronics, Clothing, or Food."), jsonOptions);
    }

    List<Product> filtered = products
        .Where(p => string.Equals(p.CategoryName, command, StringComparison.OrdinalIgnoreCase))
        .OrderBy(p => p.ProductId)
        .ToList();

    return JsonSerializer.Serialize(new ProductResponse(true, $"Found {filtered.Count} product(s) in category '{command}'.", filtered.Count, filtered), jsonOptions);
}

public record Product(int ProductId, string ProductName, string CategoryName, decimal Price, int Quantity, string Status);
public record ProductResponse(bool Success, string Message, int Count, List<Product> Data);
public record ErrorResponse(bool Success, string Message);
