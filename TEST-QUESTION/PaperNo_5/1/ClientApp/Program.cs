using System.Net.Sockets;
using System.Text;
using System.Text.Json;

JsonSerializerOptions jsonOptions = new() { PropertyNameCaseInsensitive = true };

while (true)
{
    Console.Write("Enter command (type EXIT to quit): ");
    string? command = Console.ReadLine();
    if (string.Equals(command, "EXIT", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    if (string.IsNullOrWhiteSpace(command))
    {
        Console.WriteLine("Command must not be empty.");
        continue;
    }

    try
    {
        using TcpClient client = new();
        await client.ConnectAsync("127.0.0.1", 5505);
        using NetworkStream stream = client.GetStream();
        byte[] requestBytes = Encoding.UTF8.GetBytes(command);
        await stream.WriteAsync(requestBytes);
        await stream.FlushAsync();
        client.Client.Shutdown(SocketShutdown.Send);

        using MemoryStream ms = new();
        byte[] buffer = new byte[1024];
        int bytesRead;
        while ((bytesRead = await stream.ReadAsync(buffer)) > 0)
        {
            await ms.WriteAsync(buffer.AsMemory(0, bytesRead));
        }

        string response = Encoding.UTF8.GetString(ms.ToArray());
        PrintResponse(response, jsonOptions);
    }
    catch (SocketException ex)
    {
        Console.WriteLine($"Socket error: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unexpected error: {ex.Message}");
    }

    Console.WriteLine();
}

static void PrintResponse(string response, JsonSerializerOptions jsonOptions)
{
    ProductResponse? data = JsonSerializer.Deserialize<ProductResponse>(response, jsonOptions);
    if (data?.Data != null)
    {
        Console.WriteLine(data.Message);
        foreach (ProductDto item in data.Data)
        {
            Console.WriteLine($"Id: {item.ProductId} | Name: {item.ProductName} | Category: {item.CategoryName} | Price: {item.Price} | Quantity: {item.Quantity} | Status: {item.Status}");
        }
        return;
    }

    ErrorResponse? error = JsonSerializer.Deserialize<ErrorResponse>(response, jsonOptions);
    if (error != null && !string.IsNullOrWhiteSpace(error.Message))
    {
        Console.WriteLine(error.Message);
        return;
    }

    Console.WriteLine(response);
}

public class ProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class ProductResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int Count { get; set; }
    public List<ProductDto> Data { get; set; } = new();
}

public class ErrorResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}
