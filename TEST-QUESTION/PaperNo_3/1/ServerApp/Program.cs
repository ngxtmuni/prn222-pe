using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

var services = new List<ServiceRoom>
{
    new(1, "Deluxe Room", "Daily", 35.0m, "Available", "Room with balcony"),
    new(2, "Family Room", "Monthly", 420.0m, "Occupied", "Room for family stay"),
    new(3, "VIP Room", "Monthly", 650.0m, "Available", "Large premium room"),
    new(4, "Standard Room", "Daily", 25.0m, "Maintenance", "Basic room"),
    new(5, "Garden Room", "Weekly", 180.0m, "Available", "Room with garden view")
};

JsonSerializerOptions jsonOptions = new() { WriteIndented = true };

TcpListener server = new(IPAddress.Parse("127.0.0.1"), 5303);
server.Start();
Console.WriteLine("ServiceRoomServer is running on 127.0.0.1:5303");
Console.WriteLine("Commands: ALL or a fee type such as Daily, Weekly, Monthly");

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
            string response = Query(command);
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

string Query(string command)
{
    if (string.Equals(command, "ALL", StringComparison.OrdinalIgnoreCase))
    {
        List<ServiceRoom> result = services.OrderBy(s => s.ServiceId).ToList();
        return JsonSerializer.Serialize(new ServiceResponse(true, "Returned all services.", result.Count, result), jsonOptions);
    }

    if (!string.IsNullOrWhiteSpace(command))
    {
        string feeType = command.Trim();
        List<ServiceRoom> result = services
            .Where(s => string.Equals(s.FeeType, feeType, StringComparison.OrdinalIgnoreCase))
            .OrderBy(s => s.ServiceId)
            .ToList();
        return JsonSerializer.Serialize(new ServiceResponse(true, $"Found {result.Count} service(s) with fee type '{feeType}'.", result.Count, result), jsonOptions);
    }

    return JsonSerializer.Serialize(new ErrorResponse(false, "Invalid command. Use ALL or a fee type."), jsonOptions);
}

record ServiceRoom(int ServiceId, string RoomTitle, string FeeType, decimal Price, string Status, string Description);
record ServiceResponse(bool Success, string Message, int Count, List<ServiceRoom> Data);
record ErrorResponse(bool Success, string Message);
