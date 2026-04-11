using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

List<Contract> contracts =
[
    new(1, "Apartment Lease A", "Apartment", "Nguyen Broker", 120000m),
    new(2, "Commercial Mall Unit", "Commercial", "Tran Broker", 450000m),
    new(3, "Land Lot Riverside", "Land", "Le Broker", 300000m),
    new(4, "Villa Garden Home", "Villa", "Pham Broker", 950000m),
    new(5, "Apartment Lease B", "Apartment", "Ho Broker", 135000m),
    new(6, "Commercial Office Hub", "Commercial", "Vu Broker", 520000m),
    new(7, "Land Lot Downtown", "Land", "Do Broker", 410000m),
    new(8, "Villa Luxury Bay", "Villa", "Bui Broker", 1250000m)
];

JsonSerializerOptions jsonOptions = new()
{
    WriteIndented = true
};

TcpListener server = new(IPAddress.Parse("127.0.0.1"), 5202);
server.Start();

Console.WriteLine("Broker Contract Server is running on 127.0.0.1:5202");
Console.WriteLine("Commands: ALL, Apartment, Commercial, Land, Villa");

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
        List<Contract> result = contracts
            .OrderBy(c => c.ContractId)
            .ToList();

        return JsonSerializer.Serialize(new ContractListResponse(
            true,
            $"Returned all {result.Count} contract(s).",
            result.Count,
            result), jsonOptions);
    }

    string[] validTypes = ["Apartment", "Commercial", "Land", "Villa"];
    bool isValidType = validTypes.Any(type => string.Equals(type, command, StringComparison.OrdinalIgnoreCase));
    if (!isValidType)
    {
        return JsonSerializer.Serialize(new ErrorResponse(false, "Invalid command. Use ALL, Apartment, Commercial, Land, or Villa."), jsonOptions);
    }

    List<Contract> filtered = contracts
        .Where(c => string.Equals(c.PropertyType, command, StringComparison.OrdinalIgnoreCase))
        .OrderBy(c => c.ContractId)
        .ToList();

    return JsonSerializer.Serialize(new ContractListResponse(
        true,
        $"Found {filtered.Count} contract(s) for property type '{command}'.",
        filtered.Count,
        filtered), jsonOptions);
}

public record Contract(int ContractId, string ContractTitle, string PropertyType, string BrokerName, decimal Value);
public record ContractListResponse(bool Success, string Message, int Count, List<Contract> Data);
public record ErrorResponse(bool Success, string Message);
