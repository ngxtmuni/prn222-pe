using System.Text;

Console.OutputEncoding = Encoding.UTF8;

List<Employee> employees =
[
    new(1, "Nguyen Van An", "HR", new DateTime(2019, 3, 15)),
    new(2, "Tran Thi Binh", "IT", new DateTime(2021, 7, 1)),
    new(3, "Le Quang Chau", "Finance", new DateTime(2018, 11, 20)),
    new(4, "Pham Minh Duc", "IT", new DateTime(2017, 9, 10)),
    new(5, "Hoang Thu Giang", "HR", new DateTime(2022, 1, 5)),
    new(6, "Vo Anh Khoa", "Marketing", new DateTime(2020, 6, 18))
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
            PrintEmployees("All employees:", employees.OrderBy(e => e.Id));
            break;
        case "2":
            Console.Write("Enter department: ");
            string? department = Console.ReadLine()?.Trim();
            List<Employee> filtered = employees
                .Where(e => !string.IsNullOrWhiteSpace(department) && string.Equals(e.Department, department, StringComparison.OrdinalIgnoreCase))
                .OrderBy(e => e.Name)
                .ToList();
            PrintEmployees($"Employees in department '{department}':", filtered);
            break;
        case "3":
            PrintEmployees("Employees sorted by name:", employees.OrderBy(e => e.Name));
            break;
        case "4":
            PrintEmployees("Employees sorted by hire date:", employees.OrderBy(e => e.HireDate));
            break;
        case "5":
            Console.WriteLine("Employee count by department:");
            foreach (var item in employees
                .GroupBy(e => e.Department)
                .OrderBy(g => g.Key)
                .Select(g => new { Department = g.Key, Count = g.Count() }))
            {
                Console.WriteLine($"{item.Department}: {item.Count}");
            }
            Console.WriteLine();
            break;
        case "6":
            Employee earliest = employees.OrderBy(e => e.HireDate).First();
            PrintEmployees("Employee with earliest hire date:", [earliest]);
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
    Console.WriteLine("1. Show all employees");
    Console.WriteLine("2. Filter by department");
    Console.WriteLine("3. Sort by name");
    Console.WriteLine("4. Sort by hire date");
    Console.WriteLine("5. Count employees by department");
    Console.WriteLine("6. Show earliest hire date employee");
    Console.WriteLine("0. Exit");
}

static void PrintEmployees(string title, IEnumerable<Employee> items)
{
    List<Employee> list = items.ToList();
    Console.WriteLine(title);
    if (list.Count == 0)
    {
        Console.WriteLine("No employees found.");
        Console.WriteLine();
        return;
    }

    foreach (Employee employee in list)
    {
        Console.WriteLine($"Id: {employee.Id} | Name: {employee.Name} | Department: {employee.Department} | Hire Date: {employee.HireDate:yyyy-MM-dd}");
    }

    Console.WriteLine();
}

public record Employee(int Id, string Name, string Department, DateTime HireDate);
