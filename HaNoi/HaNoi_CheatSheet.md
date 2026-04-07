# HaNoi CheatSheet

File nay chua snippet mau de dung nhanh khi di thi.
Khong co gang day ly thuyet. Muc tieu la nhin snippet, thay cho dung entity/context/route/port, roi ap dung.

## 1. Cach Dung Nhanh

- Ra bai web CRUD: vao phan `Web CRUD`.
- Ra bai socket server: vao phan `Socket Server`.
- Ra bai socket client: vao phan `Socket Client`.
- Chua ro schema DB: mo them `HaNoi_Feature_Map.md` de tim file SQL hoac `*Context.cs`.

## 2. Web CRUD

### 2.1 Program.cs khoi tao web + EF Core

Dung khi:
- Bai Razor Pages.
- Can cau hinh DB + redirect tu trang goc.

Snippet:

```csharp
using Microsoft.EntityFrameworkCore;
using Q2.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Prn22226sprB11Context>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn")));

builder.Services.AddRazorPages();

var app = builder.Build();

app.MapGet("/", () => Results.Redirect("/Customer/List"));
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
```

Can thay:
- `Prn22226sprB11Context` -> ten `DbContext` cua bai.
- `MyCnn` -> ten connection string.
- `/Customer/List` -> route trang list chinh.

Nguon:
- `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Program.cs`

Luu y nhanh:
- Quen `AddDbContext` thi khong inject duoc context.
- Quen `MapRazorPages()` thi route Razor Pages khong chay.
- Quen redirect root thi vao `/` khong nhay den trang list.

### 2.2 List du lieu

Dung khi:
- Hien thi danh sach tu DB.
- Lam trang list co the mo rong them filter va sort.

Snippet:

```csharp
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ListModel : PageModel
{
    private readonly AppDbContext _context;

    public List<EntityName> Items { get; set; } = new();

    public ListModel(AppDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        IQueryable<EntityName> query = _context.EntityNames;
        Items = query.ToList();
    }
}
```

Can thay:
- `AppDbContext`
- `EntityName`
- `_context.EntityNames`
- `Items`

Nguon:
- `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\List.cshtml.cs`

Luu y nhanh:
- Nen dung `IQueryable<T>` truoc, de them filter/sort roi moi `ToList()`.
- Neu co relation, them `Include(...)` truoc khi `ToList()`.

### 2.3 Filter bang query string / dropdown

Dung khi:
- Loc theo city, department, fee type, id, status.
- Gia tri filter di theo URL.

Snippet:

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ListModel : PageModel
{
    private readonly AppDbContext _context;

    [BindProperty(SupportsGet = true)]
    public string? Keyword { get; set; }

    public List<string> Options { get; set; } = new();
    public List<EntityName> Items { get; set; } = new();

    public ListModel(AppDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        IQueryable<EntityName> query = _context.EntityNames;

        Options = _context.EntityNames
            .Select(e => e.FilterProperty)
            .Distinct()
            .ToList();

        if (!string.IsNullOrEmpty(Keyword))
        {
            query = query.Where(e => e.FilterProperty == Keyword);
        }

        Items = query.ToList();
    }
}
```

Can thay:
- `Keyword` -> ten bien filter.
- `FilterProperty` -> cot can loc.
- `string` -> doi sang `int`, `bool`, `DateTime` neu can.

Nguon:
- `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\List.cshtml.cs`
- `HaNoi\SP25_BL5\PE_PRN222_GivenSolution1\Project2\Pages\Instructor\List.cshtml.cs`

Luu y nhanh:
- Quen `SupportsGet = true` thi query string khong bind vao property.
- Dropdown thuong load bang `Distinct().ToList()` hoac tu bang lien quan.

### 2.4 Search chuoi khong phan biet hoa thuong

Dung khi:
- Tim gan dung theo title, name, fee type.

Snippet:

```csharp
if (!string.IsNullOrWhiteSpace(Keyword))
{
    var keyword = Keyword.ToLower();
    query = query.Where(e => (e.Name ?? "").ToLower().Contains(keyword));
}
```

Can thay:
- `Keyword`
- `e.Name`

Nguon:
- `HaNoi\SU25\Q2\Q2\Pages\Services\List.cshtml.cs`

Luu y nhanh:
- Dung `(field ?? "")` de tranh null.
- Neu DB lon thi `ToLower()` co the khong toi uu, nhung cho bai thi cach nay de dung nhat.

### 2.5 Sort du lieu

Dung khi:
- Sort theo ID, name, date.

Snippet:

```csharp
[BindProperty(SupportsGet = true)]
public string? SortBy { get; set; }

public void OnGet()
{
    IQueryable<EntityName> query = _context.EntityNames;

    switch (SortBy)
    {
        case "Id":
            query = query.OrderBy(e => e.Id);
            break;
        case "CreatedDate":
            query = query.OrderBy(e => e.CreatedDate);
            break;
        default:
            query = query.OrderBy(e => e.Name);
            break;
    }

    Items = query.ToList();
}
```

Can thay:
- `SortBy`
- cac `case`
- ten field `Id`, `CreatedDate`, `Name`

Nguon:
- `HaNoi\SP25_BL5\PE_PRN222_GivenSolution1\Project2\Pages\Instructor\List.cshtml.cs`
- `HaNoi\SP25_BL5\Đề\PE_PRN222_GivenSolution1\Project2\Controllers\InstructorController.cs`

Luu y nhanh:
- Thuong sort xong moi `ToList()`.
- Neu dang MVC controller thi tra `View(...)`, neu Razor Pages thi gan vao property.

### 2.6 Create

Dung khi:
- Them ban ghi moi.

Snippet:

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    [BindProperty]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string SelectedValue { get; set; } = string.Empty;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        try
        {
            var item = new EntityName
            {
                Name = Name,
                Email = Email,
                SomeProperty = SelectedValue
            };

            _context.EntityNames.Add(item);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Create failed: " + ex.Message);
            return Page();
        }

        return RedirectToPage("/Module/List");
    }
}
```

Can thay:
- `EntityName`
- `_context.EntityNames`
- ten field bind
- `"/Module/List"`

Nguon:
- `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\Create.cshtml.cs`

Luu y nhanh:
- Quen `[BindProperty]` thi form submit len khong co du lieu.
- Quen `_context.SaveChanges()` thi DB khong luu.
- Loi thi `return Page()` de giu lai form va thong bao.

### 2.7 Update

Dung khi:
- Sua du lieu co san.
- Can hien du lieu cu len form, sua xong luu lai.

Snippet:

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class UpdateModel : PageModel
{
    private readonly AppDbContext _context;

    [BindProperty]
    public int Id { get; set; }

    [BindProperty]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string SelectedValue { get; set; } = string.Empty;

    public UpdateModel(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet(int id)
    {
        var item = _context.EntityNames.FirstOrDefault(e => e.Id == id);
        if (item == null)
        {
            return Page();
        }

        Id = item.Id;
        Name = item.Name;
        Email = item.Email;
        SelectedValue = item.SomeProperty;

        return Page();
    }

    public IActionResult OnPost()
    {
        var item = _context.EntityNames.FirstOrDefault(e => e.Id == Id);
        if (item == null)
        {
            ModelState.AddModelError(string.Empty, "Item not found.");
            return Page();
        }

        item.Name = Name;
        item.Email = Email;
        item.SomeProperty = SelectedValue;
        _context.SaveChanges();

        return Page();
    }
}
```

Can thay:
- `EntityNames`
- `Id`, `Name`, `Email`, `SomeProperty`

Nguon:
- `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\Update.cshtml.cs`

Luu y nhanh:
- Cach de nho nhat: `OnGet` load du lieu cu, `OnPost` tim object cu roi set field moi.
- Neu muon, co the tao object moi roi `_context.Update(obj)`, nhung cach sua tren object cu de an toan hon trong bai thi.
- Sau update, co the `return Page()` hoac `RedirectToPage(...)` tuy yeu cau de.

### 2.8 Detail

Dung khi:
- Xem chi tiet 1 ban ghi.

Snippet:

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class DetailModel : PageModel
{
    private readonly AppDbContext _context;
    public EntityName? Item { get; set; }

    public DetailModel(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet(int id)
    {
        Item = _context.EntityNames.FirstOrDefault(e => e.Id == id);
        return Page();
    }
}
```

Can thay:
- `EntityName`
- `EntityNames`
- `Id`

Nguon:
- `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\Detail.cshtml.cs`

Luu y nhanh:
- Neu can kem relation, them `Include(...)` truoc `FirstOrDefault(...)`.

### 2.9 Include navigation property

Dung khi:
- Can doc kem du lieu bang lien quan.

Snippet:

```csharp
using Microsoft.EntityFrameworkCore;

public void OnGet()
{
    IQueryable<Service> query = _context.Services
        .Include(s => s.EmployeeNavigation);

    Services = query.OrderBy(s => s.Id).ToList();
}
```

Can thay:
- `Service`
- `_context.Services`
- `EmployeeNavigation`

Nguon:
- `HaNoi\SU25\Q2\Q2\Pages\Services\List.cshtml.cs`
- `HaNoi\SU25_BL5\PE_PRN222_GivenSolution\Q2\Pages\Employee\List.cshtml.cs`

Luu y nhanh:
- Quan sat ten navigation property trong entity de `Include(...)` cho dung.

### 2.10 Many-to-many / remove relation

Dung khi:
- Quan he nhieu-nhieu.
- Can xoa lien ket giua 2 object, khong phai xoa hẳn record chinh.

Snippet xoa relation:

```csharp
public IActionResult OnPostRemoveAuthor(int authorId, int bookId)
{
    var author = _context.Authors.FirstOrDefault(e => e.AuthorId == authorId);
    if (author == null)
    {
        return NotFound();
    }

    var book = _context.Books.Include(e => e.Authors).FirstOrDefault(e => e.BookId == bookId);
    if (book == null)
    {
        return NotFound();
    }

    book.Authors.Remove(author);
    _context.SaveChanges();

    return RedirectToPage(new { id = authorId });
}
```

Snippet mapping many-to-many trong `DbContext`:

```csharp
entity.HasMany(d => d.Authors).WithMany(p => p.Books)
    .UsingEntity<Dictionary<string, object>>(
        "BookAuthor",
        r => r.HasOne<Author>().WithMany()
            .HasForeignKey("AuthorId"),
        l => l.HasOne<Book>().WithMany()
            .HasForeignKey("BookId"),
        j =>
        {
            j.HasKey("BookId", "AuthorId");
            j.ToTable("BookAuthors");
        });
```

Can thay:
- ten entity va ten collection relation.

Nguon:
- `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2\Pages\Author\Index.cshtml.cs`
- `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2\Entities\LibraryManagementContext.cs`

Luu y nhanh:
- Xoa relation = `collection.Remove(obj)` + `SaveChanges()`.
- Muon xoa relation thi nho `Include(...)` collection truoc.

### 2.11 MVC Controller list/filter/sort

Dung khi:
- De ra MVC Controller/View thay vi Razor Pages.

Snippet:

```csharp
public IActionResult List(string departmentId, string contractType, string sortBy)
{
    var departments = _context.Departments.ToList();
    ViewBag.Departments = departments;

    var instructors = _context.Instructors
        .Where(i => departmentId == null || i.DepartmentNavigation.DepartmentId.ToString() == departmentId)
        .Where(i => contractType == null ||
               (contractType == "Fulltime" && i.IsFulltime == true) ||
               (contractType == "Parttime" && i.IsFulltime == false));

    switch (sortBy)
    {
        case "InstructorId":
            instructors = instructors.OrderBy(i => i.InstructorId);
            break;
        case "ContractDate":
            instructors = instructors.OrderBy(i => i.ContractDate);
            break;
        default:
            instructors = instructors.OrderBy(i => i.Fullname);
            break;
    }

    ViewBag.SelectedDepartment = departmentId;
    ViewBag.SelectedContract = contractType;
    ViewBag.SelectedSort = sortBy;

    return View(instructors.ToList());
}
```

Can thay:
- ten action
- ten table/entity
- ten `ViewBag`

Nguon:
- `HaNoi\SP25_BL5\Đề\PE_PRN222_GivenSolution1\Project2\Controllers\InstructorController.cs`

Luu y nhanh:
- MVC se tra `View(...)`, khong phai `Page()`.
- Gia tri dropdown dang chon hay dua qua `ViewBag`.

## 3. Socket Server

### 3.1 TCP server co command

Dung khi:
- Bai server nhan command nhu `ALL`, category, department, ID.

Snippet:

```csharp
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

private readonly static List<Product> products = new();

TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);
server.Start();

while (true)
{
    TcpClient client = await server.AcceptTcpClientAsync();
    _ = Task.Run(() => HandleClientAsync(client));
}

static async Task HandleClientAsync(TcpClient client)
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
            var result = products.OrderBy(p => p.ProductId).ToList();
            jsonResponse = JsonSerializer.Serialize(result);
        }
        else
        {
            var result = products.Where(p => p.Category == command).OrderBy(p => p.ProductId).ToList();
            jsonResponse = JsonSerializer.Serialize(result);
        }

        byte[] responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
        await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
    }
}
```

Can thay:
- port
- danh sach data hoac nguon data
- logic xu ly command

Nguon:
- `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q1\Program.cs`

Luu y nhanh:
- `Encoding.UTF8.GetString(...)` de doi bytes sang command.
- `JsonSerializer.Serialize(...)` de tra response.
- Neu muon tra message loi, co the serialize 1 object loi rieng.

### 3.2 Socket server doc DB roi tra JSON

Dung khi:
- Server query SQL bang EF Core roi tra list/object cho client.

Snippet:

```csharp
TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080);
server.Start();

while (true)
{
    var client = await server.AcceptTcpClientAsync();

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
    }

    client.Close();
}
```

Can thay:
- port
- command
- ten context / table

Nguon:
- `HaNoi\SP25\PE_PRN222_GivenSolution1\Project11\Program.cs`

Luu y nhanh:
- Phu hop bai co server + DB.
- Neu response phuc tap hon, dung object DTO/response class roi serialize.

### 3.3 Socket server tra object co nested list

Dung khi:
- Can tra object tong co metadata + list con.

Snippet:

```csharp
public class ServerResponse
{
    public bool BookExists { get; set; }
    public string BookTitle { get; set; } = "";
    public List<BorrowerRecord> BorrowerRecords { get; set; } = new();
}

private static ServerResponse GetBookBorrowerResponse(int bookId)
{
    var response = new ServerResponse();

    var book = books.FirstOrDefault(b => b.BookID == bookId);
    response.BookExists = book != null;

    if (!response.BookExists)
    {
        return response;
    }

    response.BookTitle = book.Title;

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

    response.BorrowerRecords = response.BorrowerRecords.OrderBy(r => r.BorrowDate).ToList();
    return response;
}
```

Can thay:
- ten response class
- ten detail class
- field can tra ve

Nguon:
- `HaNoi\SU25_BL5\ServerApp\Port4000Server\Program.cs`

Luu y nhanh:
- Kieu nay hop bai can tra ca thong tin tong + danh sach chi tiet.
- Build object xong moi serialize, de code de doc hon.

## 4. Socket Client

### 4.1 Client console gui request va deserialize JSON

Dung khi:
- Nhap ID / command.
- Server tra JSON co cau truc ro rang.

Snippet:

```csharp
using System.Net.Sockets;
using System.Text.Json;

using TcpClient client = new();
client.Connect("127.0.0.1", 2000);

using NetworkStream stream = client.GetStream();
using StreamWriter writer = new(stream) { AutoFlush = true };
using StreamReader reader = new(stream);

writer.WriteLine(employeeId);
string? json = reader.ReadLine();

List<ProjectDTO>? projects = JsonSerializer.Deserialize<List<ProjectDTO>>(
    json,
    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
```

Can thay:
- IP
- port
- request gui len
- kieu DTO nhan ve

Nguon:
- `HaNoi\SU25\Q1\Q1\Program.cs`

Luu y nhanh:
- Cach nay hop khi server tra text line-based.
- Neu server tra raw stream den het ket noi, xem muc 4.3.

### 4.2 DTO map ten JSON khac ten property C#

Dung khi:
- JSON field ten khac ten property trong class.

Snippet:

```csharp
using System.Text.Json.Serialization;

public class ProjectDTO
{
    [JsonPropertyName("ProjectId")]
    public int Id { get; set; }

    public string? Title { get; set; } = "";
    public string? Description { get; set; } = "";

    [JsonPropertyName("Role")]
    public string? Position { get; set; } = "";
}
```

Can thay:
- ten field JSON trong `[JsonPropertyName(...)]`
- ten property C#

Nguon:
- `HaNoi\SU25\Q1\Q1\Program.cs`

Luu y nhanh:
- Rat hop khi de muon doi ten field cho dep ma van map dung JSON.

### 4.3 Client doc raw response den het stream

Dung khi:
- Server khong tra theo tung line.
- Muon doc toan bo raw JSON/string den khi server dong stream.

Snippet:

```csharp
using System.Net.Sockets;
using System.Text;

using TcpClient client = new();
await client.ConnectAsync("127.0.0.1", 5000);

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
Console.WriteLine(response);
```

Can thay:
- IP
- port
- `command`

Nguon:
- `HaNoi\SP26\Đề\PRN222_SP26_PE_L1_739461\PaperNo_4\1\EmployeeClient\Program.cs`

Luu y nhanh:
- `Shutdown(SocketShutdown.Send)` rat quan trong trong kieu nay, de server biet client gui xong.
- Kieu nay hop bai tra raw JSON va client chi can in ra hoac tu parse sau.

### 4.4 Bat loi client

Dung khi:
- Server chua chay.
- Sai port.
- Loi parse hoac loi khac.

Snippet:

```csharp
try
{
    using TcpClient client = new();
    client.Connect("127.0.0.1", 2000);
    // logic gui / nhan
}
catch (SocketException ex)
{
    Console.WriteLine($"Socket error: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected error: {ex.Message}");
}
```

Nguon:
- `HaNoi\SU25\Q1\Q1\Program.cs`
- `HaNoi\SP26\Đề\PRN222_SP26_PE_L1_739461\PaperNo_4\1\EmployeeClient\Program.cs`

Luu y nhanh:
- Bai thi thuong can thong bao loi than thien thay vi crash.

## 5. Mau Thay Nhanh

### 5.1 Razor Pages list co filter

```csharp
private readonly DbContextName _context;

public List<EntityName> Items { get; set; } = new();

[BindProperty(SupportsGet = true)]
public string? Keyword { get; set; }

public void OnGet()
{
    IQueryable<EntityName> query = _context.TableName;

    if (!string.IsNullOrWhiteSpace(Keyword))
    {
        query = query.Where(e => (e.FilterProperty ?? "").Contains(Keyword));
    }

    Items = query.ToList();
}
```

### 5.2 Razor Pages create

```csharp
[BindProperty]
public string Name { get; set; } = string.Empty;

public IActionResult OnPost()
{
    var item = new EntityName
    {
        Name = Name
    };

    _context.TableName.Add(item);
    _context.SaveChanges();
    return RedirectToPage("/Module/List");
}
```

### 5.3 Razor Pages update

```csharp
public IActionResult OnGet(int id)
{
    var item = _context.TableName.FirstOrDefault(e => e.Id == id);
    if (item == null)
    {
        return Page();
    }

    Id = item.Id;
    Name = item.Name;
    return Page();
}

public IActionResult OnPost()
{
    var item = _context.TableName.FirstOrDefault(e => e.Id == Id);
    if (item == null)
    {
        return Page();
    }

    item.Name = Name;
    _context.SaveChanges();
    return Page();
}
```

### 5.4 Socket server basic

```csharp
TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), Port);
server.Start();

while (true)
{
    TcpClient client = await server.AcceptTcpClientAsync();
    _ = Task.Run(() => HandleClientAsync(client));
}
```

### 5.5 Socket client basic

```csharp
using TcpClient client = new();
client.Connect("127.0.0.1", Port);
using NetworkStream stream = client.GetStream();
```

## 6. Loi Hay Gap

- Quen `SupportsGet = true` khi dung query string.
- Quen `_context.SaveChanges()` sau `Add`, `Update`, `Remove`.
- `RedirectToPage` sai route.
- Search chuoi bi null vi khong dung `(field ?? "")`.
- `Include(...)` sai ten navigation property.
- Server va client sai port.
- Client raw-response quen `Shutdown(SocketShutdown.Send)`.
- Deserialize sai kieu hoac sai ten field JSON.
- MVC dung `return View(...)`, Razor Pages dung `return Page()` / `RedirectToPage(...)`.

## 7. Nguon Mau Quan Trong Nhat

- Web CRUD: `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2`
- Web relation: `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2`
- Web filter/sort: `HaNoi\SP25_BL5\PE_PRN222_GivenSolution1\Project2`
- Socket server: `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q1`
- Socket server + EF: `HaNoi\SP25\PE_PRN222_GivenSolution1\Project11`
- Socket client deserialize: `HaNoi\SU25\Q1\Q1`
- Socket client raw response: `HaNoi\SP26\Đề\PRN222_SP26_PE_L1_739461\PaperNo_4\1\EmployeeClient`
