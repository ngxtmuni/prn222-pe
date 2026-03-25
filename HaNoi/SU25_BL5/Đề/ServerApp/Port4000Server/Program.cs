using System.Net.Sockets;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Text;

namespace ServerLib
{
    public class BorrowerRecord
    {
        public int ReaderID { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = "";
    }

    public class ServerResponse
    {
        public bool BookExists { get; set; }
        public string BookTitle { get; set; } = "";
        public List<BorrowerRecord> BorrowerRecords { get; set; } = new List<BorrowerRecord>();
    }

    public class Reader
    {
        public int ReaderID { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public DateTime RegistrationDate { get; set; }
    }

    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string ISBN { get; set; } = "";
        public int PublishYear { get; set; }
        public string Category { get; set; } = "";
    }

    public class Server4000
    {
        private static List<Reader> readers = new List<Reader>();
        private static List<Book> books = new List<Book>();
        private static List<(int RecordID, int ReaderID, int BookID, DateTime BorrowDate, DateTime? ReturnDate, string Status)> borrowRecords = new List<(int, int, int, DateTime, DateTime?, string)>();

        static async Task Main(string[] args)
        {
            InitializeSampleData();

            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 4000);
            server.Start();

            Console.WriteLine("Book Tracking Server is running on 127.0.0.1:4000");
            Console.WriteLine("Waiting for client connections...");

            while (true)
            {
                try
                {
                    TcpClient client = await server.AcceptTcpClientAsync();
                    _ = Task.Run(() => HandleClient(client));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error accepting client: {ex.Message}");
                }
            }
        }

        private static async Task HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            try
            {
                // Read Book ID from client
                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string bookIdStr = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Console.WriteLine($"Received Book ID: {bookIdStr}");

                if (int.TryParse(bookIdStr, out int bookId))
                {
                    var response = GetBookBorrowerResponse(bookId);
                    string jsonResponse = JsonSerializer.Serialize(response);

                    byte[] responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
                    await stream.WriteAsync(responseBytes, 0, responseBytes.Length);

                    if (response.BookExists)
                    {
                        Console.WriteLine($"Sent {response.BorrowerRecords.Count} borrower records for Book ID: {bookId} ({response.BookTitle})");
                    }
                    else
                    {
                        Console.WriteLine($"Book ID {bookId} does not exist");
                    }
                }
                else
                {
                    // Send response for invalid Book ID format
                    var response = new ServerResponse { BookExists = false, BorrowerRecords = new List<BorrowerRecord>() };
                    string emptyResponse = JsonSerializer.Serialize(response);
                    byte[] responseBytes = Encoding.UTF8.GetBytes(emptyResponse);
                    await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling client: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }

        private static ServerResponse GetBookBorrowerResponse(int bookId)
        {
            var response = new ServerResponse();

            // Check if book exists
            var book = books.FirstOrDefault(b => b.BookID == bookId);
            response.BookExists = book != null;

            if (!response.BookExists)
            {
                return response; // Return with empty borrower records if book doesn't exist
            }

            response.BookTitle = book.Title;

            // Book exists, get borrower history
            foreach (var record in borrowRecords.Where(r => r.BookID == bookId))
            {
                var reader = readers.FirstOrDefault(r => r.ReaderID == record.ReaderID);
                if (reader != null)
                {
                    response.BorrowerRecords.Add(new BorrowerRecord
                    {
                        ReaderID = reader.ReaderID,
                        FullName = reader.FullName,
                        Email = reader.Email,
                        BorrowDate = record.BorrowDate,
                        ReturnDate = record.ReturnDate,
                        Status = record.Status
                    });
                }
            }

            // Sort by borrow date for consistent display
            response.BorrowerRecords = response.BorrowerRecords.OrderBy(r => r.BorrowDate).ToList();

            return response;
        }

        private static void InitializeSampleData()
        {
            // Sample Readers
            readers.AddRange(new[]
            {
                new Reader { ReaderID = 101, FullName = "John Smith", Email = "john@email.com", PhoneNumber = "123-456-7890", RegistrationDate = DateTime.Now.AddMonths(-6) },
                new Reader { ReaderID = 102, FullName = "Jane Doe", Email = "jane@email.com", PhoneNumber = "098-765-4321", RegistrationDate = DateTime.Now.AddMonths(-3) },
                new Reader { ReaderID = 103, FullName = "Bob Johnson", Email = "bob@email.com", PhoneNumber = "555-123-4567", RegistrationDate = DateTime.Now.AddMonths(-1) },
                new Reader { ReaderID = 104, FullName = "Alice Brown", Email = "alice@email.com", PhoneNumber = "777-888-9999", RegistrationDate = DateTime.Now.AddMonths(-2) }
            });

            // Sample Books
            books.AddRange(new[]
            {
                new Book { BookID = 1001, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", ISBN = "978-0-7432-7356-5", PublishYear = 1925, Category = "Fiction" },
                new Book { BookID = 1002, Title = "To Kill a Mockingbird", Author = "Harper Lee", ISBN = "978-0-06-112008-4", PublishYear = 1960, Category = "Fiction" },
                new Book { BookID = 1003, Title = "1984", Author = "George Orwell", ISBN = "978-0-452-28423-4", PublishYear = 1949, Category = "Dystopian" },
                new Book { BookID = 1004, Title = "Pride and Prejudice", Author = "Jane Austen", ISBN = "978-0-14-143951-8", PublishYear = 1813, Category = "Romance" },
                new Book { BookID = 1005, Title = "The Catcher in the Rye", Author = "J.D. Salinger", ISBN = "978-0-316-76948-0", PublishYear = 1951, Category = "Fiction" }
            });

            // Sample Borrow Records
            borrowRecords.AddRange(new[]
            {
                // Book 1001 (The Great Gatsby) - borrowed by multiple readers
                (1, 101, 1001, DateTime.Parse("2024-01-15"), (DateTime?)DateTime.Parse("2024-02-15"), "Returned"),
                (2, 103, 1001, DateTime.Parse("2024-04-01"), null, "Borrowed"),
                (3, 102, 1001, DateTime.Parse("2024-02-10"), DateTime.Parse("2024-03-15"), "Overdue"),
                
                // Book 1002 (To Kill a Mockingbird) - borrowed by some readers
                (4, 101, 1002, DateTime.Parse("2024-02-20"), null, "Borrowed"),
                (5, 104, 1002, DateTime.Parse("2024-03-10"), DateTime.Parse("2024-04-10"), "Returned"),
                
                // Book 1003 (1984) - borrowed by one reader
                (6, 101, 1003, DateTime.Parse("2024-01-10"), DateTime.Parse("2024-02-25"), "Overdue"),
                
                // Book 1004 (Pride and Prejudice) - borrowed by some readers
                (7, 102, 1004, DateTime.Parse("2024-03-01"), DateTime.Parse("2024-03-15"), "Returned"),
                (8, 104, 1004, DateTime.Parse("2024-04-15"), null, "Borrowed")
                
                // Book 1005 (The Catcher in the Rye) - exists but no borrow records
            });

            Console.WriteLine("Sample data initialized successfully!");
            Console.WriteLine($"Readers: {readers.Count}, Books: {books.Count}, Borrow Records: {borrowRecords.Count}");
        }
    }
}
