using Project11.Entities;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080);
        server.Start();
        Console.WriteLine("Server started at http://localhost:8080/");

        while (true)
        {
            var client = await server.AcceptTcpClientAsync();
            Console.WriteLine("Client connected.");

            using var stream = client.GetStream();
            byte[] buffer = new byte[4096];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            if (request == "GetStudents")
            {
                using var db = new PePrn25sprB5Context();
                var students = db.Students.ToList();
                string json = JsonSerializer.Serialize(students);
                byte[] data = Encoding.UTF8.GetBytes(json);
                await stream.WriteAsync(data, 0, data.Length);
                Console.WriteLine("Sent students list to client.");
            }
            client.Close();
        }
    }
}