using System.Text;

Console.OutputEncoding = Encoding.UTF8;

List<Student> students =
[
    new(1, "Nguyen Van A", ["PRN222", "DBI202", "MAD101"]),
    new(2, "Tran Thi B", ["PRN222", "SWP391"]),
    new(3, "Le Van C", ["DBI202"]),
    new(4, "Pham Thi D", ["PRN222", "DBI202", "SWR302", "MAD101"]),
    new(5, "Hoang Van E", ["SWR302", "SWP391"])
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
            PrintStudents("All students:", students.OrderBy(s => s.StudentId));
            break;
        case "2":
            Console.Write("Enter course: ");
            string? course = Console.ReadLine()?.Trim();
            List<Student> filtered = students
                .Where(s => !string.IsNullOrWhiteSpace(course) && s.Courses.Any(c => string.Equals(c, course, StringComparison.OrdinalIgnoreCase)))
                .OrderBy(s => s.Name)
                .ToList();
            PrintStudents($"Students enrolled in '{course}':", filtered);
            break;
        case "3":
            Console.WriteLine("Student count in each course:");
            foreach (var item in students
                .SelectMany(s => s.Courses)
                .GroupBy(c => c)
                .OrderBy(g => g.Key)
                .Select(g => new { Course = g.Key, Count = g.Count() }))
            {
                Console.WriteLine($"{item.Course}: {item.Count}");
            }
            Console.WriteLine();
            break;
        case "4":
            PrintStudents("Students sorted by name:", students.OrderBy(s => s.Name));
            break;
        case "5":
            int maxCourseCount = students.Max(s => s.Courses.Count);
            List<Student> topStudents = students
                .Where(s => s.Courses.Count == maxCourseCount)
                .OrderBy(s => s.Name)
                .ToList();
            PrintStudents("Student(s) with most enrolled courses:", topStudents);
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
    Console.WriteLine("1. Show all students");
    Console.WriteLine("2. Filter by course");
    Console.WriteLine("3. Count students in each course");
    Console.WriteLine("4. Sort by student name");
    Console.WriteLine("5. Find student with most enrolled courses");
    Console.WriteLine("0. Exit");
}

static void PrintStudents(string title, IEnumerable<Student> items)
{
    List<Student> list = items.ToList();
    Console.WriteLine(title);
    if (list.Count == 0)
    {
        Console.WriteLine("No students found.");
        Console.WriteLine();
        return;
    }

    foreach (Student student in list)
    {
        Console.WriteLine($"Id: {student.StudentId} | Name: {student.Name} | Courses: {string.Join(", ", student.Courses)} | Total: {student.Courses.Count}");
    }

    Console.WriteLine();
}

public record Student(int StudentId, string Name, List<string> Courses);
