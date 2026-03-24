using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

while (true)
{
    Console.Write("Enter employee ID: ");
    string? input = Console.ReadLine();

    if (string.IsNullOrEmpty(input))
    {
        break;
    }

    if (!int.TryParse(input, out int employeeId) || employeeId <= 0)
    {
        Console.WriteLine("Invalid input! Please enter a valid integer.");
        continue;
    }

    try
    {
        using TcpClient client = new();
        client.Connect("127.0.0.1", 2000);
        using NetworkStream stream = client.GetStream();
        using StreamWriter writer = new(stream) { AutoFlush = true };
        using StreamReader reader = new(stream);

        writer.WriteLine(employeeId);
        string? json = reader.ReadLine();
        //Console.WriteLine("RAW JSON: " + json);
        if (string.IsNullOrEmpty(json))
        {
            Console.WriteLine($"No projects found for employee ID {employeeId}");
            continue;
        }

        List<ProjectDTO>? projects = JsonSerializer.Deserialize<List<ProjectDTO>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (projects == null || projects.Count == 0)
        {
            Console.WriteLine($"No projects found for employee ID {employeeId}.");
            continue;
        }
        Console.WriteLine($"Projects for employee ID {employeeId}:");

        foreach (ProjectDTO p in projects)
        {
            Console.WriteLine($"ID: {p.Id}");
            Console.WriteLine($"Title: {p.Title}");
            Console.WriteLine($"Description: {p.Description}");
            Console.WriteLine($"Position: {p.Position}");
            Console.WriteLine();
        }
    }
    catch (SocketException)
    {
        Console.WriteLine("Server is not running. Please try again later");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Error: {e.Message}");
    }

}

public class ProjectDTO
{
    [JsonPropertyName("ProjectId")]
    public int Id { get; set; }
    public string? Title { get; set; } = "";
    public string? Description { get; set; } = "";
    [JsonPropertyName("Role")]
    public string? Position { get; set; } = "";
}