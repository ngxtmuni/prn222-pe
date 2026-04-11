using System.Text;

Console.OutputEncoding = Encoding.UTF8;

List<Book> books =
[
    new(1, "Clean Code", "Robert C. Martin", 2008),
    new(2, "Refactoring", "Martin Fowler", 1999),
    new(3, "Clean Architecture", "Robert C. Martin", 2017),
    new(4, "Domain-Driven Design", "Eric Evans", 2003),
    new(5, "Patterns of Enterprise Application Architecture", "Martin Fowler", 2002)
];

while (true)
{
    ShowMenu();
    Console.Write("Choose: ");
    string? choice = Console.ReadLine()?.Trim();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            Console.Write("Enter title keyword: ");
            string? keyword = Console.ReadLine()?.Trim();
            List<Book> searched = books
                .Where(b => !string.IsNullOrWhiteSpace(keyword) && b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .OrderBy(b => b.Title)
                .ToList();
            PrintBooks($"Books matching title keyword '{keyword}':", searched);
            break;
        case "2":
            Console.Write("Enter author name: ");
            string? author = Console.ReadLine()?.Trim();
            List<Book> filtered = books
                .Where(b => !string.IsNullOrWhiteSpace(author) && string.Equals(b.Author, author, StringComparison.OrdinalIgnoreCase))
                .OrderBy(b => b.Title)
                .ToList();
            PrintBooks($"Books by author '{author}':", filtered);
            break;
        case "3":
            Console.WriteLine("1. Sort by title");
            Console.WriteLine("2. Sort by publish year");
            Console.Write("Choose sort option: ");
            string? sortOption = Console.ReadLine()?.Trim();
            Console.WriteLine();
            IEnumerable<Book> sorted = sortOption switch
            {
                "1" => books.OrderBy(b => b.Title),
                "2" => books.OrderBy(b => b.PublishYear).ThenBy(b => b.Title),
                _ => Enumerable.Empty<Book>()
            };

            if (!sorted.Any())
            {
                Console.WriteLine("Invalid sort option.");
                Console.WriteLine();
                break;
            }

            PrintBooks("Sorted books:", sorted);
            break;
        case "4":
            Console.WriteLine("Book count by author:");
            foreach (var item in books
                .GroupBy(b => b.Author)
                .OrderBy(g => g.Key)
                .Select(g => new { Author = g.Key, Count = g.Count() }))
            {
                Console.WriteLine($"{item.Author}: {item.Count}");
            }
            Console.WriteLine();
            break;
        case "0":
            return;
        default:
            Console.WriteLine("Invalid choice.");
            Console.WriteLine();
            break;
    }
}

static void ShowMenu()
{
    Console.WriteLine("1. Search books by title");
    Console.WriteLine("2. Filter by author");
    Console.WriteLine("3. Sort by title or publish year");
    Console.WriteLine("4. Count books by author");
    Console.WriteLine("0. Exit");
}

static void PrintBooks(string title, IEnumerable<Book> items)
{
    List<Book> list = items.ToList();
    Console.WriteLine(title);
    if (list.Count == 0)
    {
        Console.WriteLine("No books found.");
        Console.WriteLine();
        return;
    }

    foreach (Book book in list)
    {
        Console.WriteLine($"Id: {book.BookId} | Title: {book.Title} | Author: {book.Author} | Publish Year: {book.PublishYear}");
    }

    Console.WriteLine();
}

public record Book(int BookId, string Title, string Author, int PublishYear);
