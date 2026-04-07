using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Q1
{
    internal class Program
    {
        private readonly static List<Product> products = new();

        static async Task Main(string[] args)
        {
            InitializeData();
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);
            server.Start();

            Console.WriteLine("Product Server is running on 127.0.0.1:5000");
            try
            {
                while (true)
                {
                    TcpClient client = await server.AcceptTcpClientAsync();

                    IPEndPoint? remoteEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                    string clientIp = remoteEndPoint?.Address.ToString() ?? "Unknown";
                    int clientPort = remoteEndPoint?.Port ?? 0;

                    Console.WriteLine($"Client connected from {clientIp}:{clientPort}");

                    _ = Task.Run(() => HandleClientAsync(client, clientIp, clientPort));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Server error: {e.Message}");
            }
            finally
            {
                server.Stop();
            }
        }
        private static async Task HandleClientAsync(TcpClient client, string clientIp, int clientPort)
        {
            try
            {
                using (client)
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead <= 0)
                    {
                        return;
                    }

                    string command = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                    string jsonResponse;

                    if (command == "ALL")
                    {
                        var result = products
                            .OrderBy(p => p.ProductId)
                            .Select(p => new
                            {
                                productId = p.ProductId,
                                productName = p.ProductName,
                                category = p.Category,
                                price = p.Price
                            })
                            .ToList();

                        jsonResponse = JsonSerializer.Serialize(result);
                        Console.WriteLine($"Query: ALL; Returning {result.Count} products");
                    }
                    else
                    {
                        var result = products
                            .Where(p => p.Category == command)
                            .OrderBy(p => p.ProductId)
                            .Select(p => new
                            {
                                productId = p.ProductId,
                                productName = p.ProductName,
                                category = p.Category,
                                price = p.Price
                            })
                            .ToList();

                        if (result.Any())
                        {
                            jsonResponse = JsonSerializer.Serialize(result);
                            Console.WriteLine($"Query: {command}; Found {result.Count} products");
                        }
                        else
                        {
                            var errorResponse = new
                            {
                                category = command,
                                status = "not found",
                                message = "No products found in this category"
                            };

                            jsonResponse = JsonSerializer.Serialize(errorResponse);
                            Console.WriteLine($"Query: {command}; Not Found");
                        }
                    }

                    byte[] responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
                    await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling client {clientIp}:{clientPort} - {ex.Message}");
            }
            finally
            {
                Console.WriteLine($"Client from {clientIp}:{clientPort} disconnected");
            }
        }

        private static void InitializeData()
        {
            products.AddRange(new[]
            {
                new Product { ProductId = "P001", ProductName = "Laptop", Category = "Electronics", Price = 1200 },
                new Product { ProductId = "P002", ProductName = "T-Shirt", Category = "Clothing", Price = 25 },
                new Product { ProductId = "P003", ProductName = "Headphones", Category = "Electronics", Price = 150 },
                new Product { ProductId = "P004", ProductName = "Jeans", Category = "Clothing", Price = 60 },
                new Product { ProductId = "P005", ProductName = "Rice (5kg)", Category = "Food", Price = 15 }
            });
        }
    }

    internal class Product
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Price { get; set; }
    }
}