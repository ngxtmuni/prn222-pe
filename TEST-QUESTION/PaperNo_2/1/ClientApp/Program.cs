using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

Console.OutputEncoding = Encoding.UTF8;
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
        await client.ConnectAsync("127.0.0.1", 5202);
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
    catch (Exception ex)
    {
        Console.WriteLine($"Unexpected error: {ex.Message}");
    }

    Console.WriteLine();
}

static void PrintResponse(string command, string response, JsonSerializerOptions jsonOptions)
{
    Console.WriteLine("Server response:");

    if (command.Equals("ALL", StringComparison.OrdinalIgnoreCase) ||
        !string.IsNullOrWhiteSpace(command))
    {
        ContractListResponse? contractResponse = JsonSerializer.Deserialize<ContractListResponse>(response, jsonOptions);
        if (contractResponse?.Data != null)
        {
            Console.WriteLine(contractResponse.Message);
            foreach (ContractDto item in contractResponse.Data)
            {
                Console.WriteLine($"Id: {item.ContractId} | Title: {item.ContractTitle} | Type: {item.PropertyType} | Broker: {item.BrokerName} | Value: {item.Value}");
            }
            return;
        }
    }

    ErrorResponse? error = JsonSerializer.Deserialize<ErrorResponse>(response, jsonOptions);
    if (error != null && !string.IsNullOrWhiteSpace(error.Message))
    {
        Console.WriteLine(error.Message);
        return;
    }

    Console.WriteLine(response);
}

public class ContractDto
{
    public int ContractId { get; set; }
    public string ContractTitle { get; set; } = string.Empty;
    public string PropertyType { get; set; } = string.Empty;
    public string BrokerName { get; set; } = string.Empty;
    public decimal Value { get; set; }
}

public class ContractListResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int Count { get; set; }
    public List<ContractDto> Data { get; set; } = new();
}

public class ErrorResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
