using System.Net.Sockets;
using System.Text;
using System.Text.Json;

Console.OutputEncoding = Encoding.UTF8;

JsonSerializerOptions jsonOptions = new()
{
    PropertyNameCaseInsensitive = true
};

while (true)
{
    Console.Write("Enter command (ALL, Ha Noi, Da Nang, Hai Phong, EXIT): ");
    string? command = Console.ReadLine()?.Trim();

    if (string.Equals(command, "EXIT", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    if (string.IsNullOrWhiteSpace(command))
    {
        Console.WriteLine("Command must not be empty.");
        Console.WriteLine();
        continue;
    }

    try
    {
        using TcpClient client = new();
        await client.ConnectAsync("127.0.0.1", 5101);
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
        PrintResponse(command, response, jsonOptions);
    }
    catch (SocketException ex)
    {
        Console.WriteLine($"Socket error: {ex.Message}");
    }
    catch (JsonException)
    {
        Console.WriteLine("Received invalid JSON from server.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unexpected error: {ex.Message}");
    }

    Console.WriteLine();
}

static void PrintResponse(string command, string response, JsonSerializerOptions jsonOptions)
{
    CustomerListResponse? customerResponse = JsonSerializer.Deserialize<CustomerListResponse>(response, jsonOptions);
    if (customerResponse?.Data != null)
    {
        Console.WriteLine(customerResponse.Message);
        foreach (CustomerDto item in customerResponse.Data)
        {
            Console.WriteLine($"Id: {item.CustomerId} | Name: {item.CustomerName} | Email: {item.Email} | City: {item.City}");
        }

        if (customerResponse.Count == 0)
        {
            Console.WriteLine($"No customers matched command '{command}'.");
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

public class CustomerDto
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}

public class CustomerListResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int Count { get; set; }
    public List<CustomerDto> Data { get; set; } = new();
}

public class ErrorResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}
