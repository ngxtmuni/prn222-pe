# HaNoi Feature Map

File nay la index tra nhanh.
Muc tieu: nhin 10-20 giay la biet nen mo project nao, file nao, search keyword gi.

## Quick Start

- Web CRUD: mo `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2`
- Socket server: mo `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q1`
- Socket client console: mo `HaNoi\SU25\Q1\Q1` hoac `HaNoi\SP26\Đề\PRN222_SP26_PE_L1_739461\PaperNo_4\1\EmployeeClient`
- WPF client: mo `HaNoi\SP25\PE_PRN222_GivenSolution1\Project12`
- SQL schema: mo `HaNoi\SP26\Đề\PRN222_SP26_PE_L1_739461\PaperNo_4\2\script1.sql`

## Project Tot Nhat

| Dang bai | Mau uu tien | Ly do |
|---|---|---|
| Web CRUD | `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2` | Day du list, create, update, detail, dropdown, redirect |
| Web relation | `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2` | Co include, many-to-many, remove relation |
| Web filter/sort | `HaNoi\SP25_BL5\PE_PRN222_GivenSolution1\Project2` | Filter + sort rat ro |
| Socket server co command | `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q1` | Server command-based de hoc nhat |
| Socket server + EF | `HaNoi\SP25\PE_PRN222_GivenSolution1\Project11` | Query DB roi tra JSON |
| Socket client console | `HaNoi\SU25\Q1\Q1` | Client co deserialize + DTO mapping |
| Socket client raw response | `HaNoi\SP26\Đề\PRN222_SP26_PE_L1_739461\PaperNo_4\1\EmployeeClient` | Gui command, doc het stream, in raw response |
| WPF client | `HaNoi\SP25\PE_PRN222_GivenSolution1\Project12` | Bam nut, load data, bind DataGrid |
| SQL script | `HaNoi\SP26\Đề\PRN222_SP26_PE_L1_739461\PaperNo_4\2\script1.sql` | Schema gon, de doi chieu nhanh |

## Tong Quan Bo De

| Bo de | Project chinh | Dang bai | Cong nghe |
|---|---|---|---|
| SP26 | `PE_PRN222_GivenSolution_Practice/Q1` | Socket server | Console + TCP + JSON |
| SP26 | `PE_PRN222_GivenSolution_Practice/Q2` | Web CRUD | Razor Pages + EF Core + SQL Server |
| SP26 | `Đề/PRN222_SP26_PE_L1_739461/PaperNo_4/1/EmployeeClient` | Socket client | Console + TCP |
| SP26 | `Đề/PRN222_SP26_PE_L1_739461/PaperNo_4/2/script1.sql` | SQL script | SQL Server script |
| SP25 | `PE_PRN222_GivenSolution1/Project11` | Socket server | Console + TCP + EF Core |
| SP25 | `PE_PRN222_GivenSolution1/Project12` | WPF client | WPF + TCP + JSON |
| SP25 | `PE_PRN222_GivenSolution1/Project2` | Web CRUD | Razor Pages + EF Core |
| SP25_BL5 | `PE_PRN222_GivenSolution1/Project11` | Socket server | Console + TCP + EF Core |
| SP25_BL5 | `PE_PRN222_GivenSolution1/Project12` | WPF client | WPF + TCP + JSON |
| SP25_BL5 | `PE_PRN222_GivenSolution1/Project2` | Web CRUD | Razor Pages + EF Core |
| SU25 | `Q1/Q1` | Socket client | Console + TCP + JSON |
| SU25 | `Q2/Q2` | Web CRUD | Razor Pages + EF Core |
| SU25_BL5 | `PE_PRN222_GivenSolution/Q1` | Socket client | Console + TCP + JSON |
| SU25_BL5 | `PE_PRN222_GivenSolution/Q2` | Web CRUD | Razor Pages + EF Core |
| SU25_BL5 | `ServerApp/Port3000Server` | Socket server | Console + TCP + JSON |
| SU25_BL5 | `ServerApp/Port4000Server` | Socket server | Console + TCP + JSON |

## Tra Nhanh Theo Tinh Nang

### Web

#### ASP.NET Core + EF Core
- Mau tot nhat:
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Program.cs`
  - `HaNoi\SU25\Q2\Q2\Program.cs`
- Mo file nao:
  - `Program.cs`
- Search:
  - `AddDbContext`
  - `UseSqlServer`
  - `AddRazorPages`
  - `MapRazorPages`

#### Redirect root
- Mau tot nhat:
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Program.cs`
  - `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2\Program.cs`
- Mo file nao:
  - `Program.cs`
- Search:
  - `MapGet`
  - `Results.Redirect`

#### List / Filter / Sort
- Mau tot nhat:
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\List.cshtml.cs`
  - `HaNoi\SP25_BL5\PE_PRN222_GivenSolution1\Project2\Pages\Instructor\List.cshtml.cs`
  - `HaNoi\SU25\Q2\Q2\Pages\Services\List.cshtml.cs`
- Mo file nao:
  - `Pages\...\List.cshtml.cs`
- Search:
  - `SupportsGet = true`
  - `Where(`
  - `OrderBy(`
  - `Distinct()`

#### Create
- Mau tot nhat:
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\Create.cshtml.cs`
- Mo file nao:
  - `Pages\...\Create.cshtml.cs`
- Search:
  - `BindProperty`
  - `Add(`
  - `SaveChanges()`
  - `RedirectToPage(`

#### Update
- Mau tot nhat:
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\Update.cshtml.cs`
- Mo file nao:
  - `Pages\...\Update.cshtml.cs`
- Search:
  - `OnGet(int id)`
  - `OnPost()`
  - `FirstOrDefault(`
  - `Update(`

#### Detail
- Mau tot nhat:
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\Detail.cshtml.cs`
  - `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2\Pages\Author\Index.cshtml.cs`
- Mo file nao:
  - `Pages\...\Detail.cshtml.cs`
  - `Pages\...\Index.cshtml.cs`
- Search:
  - `FirstOrDefault(`
  - `return Page()`

#### Include / relation
- Mau tot nhat:
  - `HaNoi\SU25\Q2\Q2\Pages\Services\List.cshtml.cs`
  - `HaNoi\SU25_BL5\PE_PRN222_GivenSolution\Q2\Pages\Employee\List.cshtml.cs`
  - `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2\Pages\Author\Index.cshtml.cs`
- Mo file nao:
  - `Pages\...\List.cshtml.cs`
  - `Pages\...\Index.cshtml.cs`
- Search:
  - `Include(`

#### Many-to-many / remove relation
- Mau tot nhat:
  - `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2\Entities\LibraryManagementContext.cs`
  - `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2\Pages\Author\Index.cshtml.cs`
- Mo file nao:
  - `Entities\*Context.cs`
  - `Pages\...\Index.cshtml.cs`
- Search:
  - `UsingEntity`
  - `WithMany`
  - `.Remove(`

#### MVC Controller
- Mau tot nhat:
  - `HaNoi\SP25_BL5\Đề\PE_PRN222_GivenSolution1\Project2\Controllers\InstructorController.cs`
- Mo file nao:
  - `Controllers\*.cs`
- Search:
  - `IActionResult List(`
  - `ViewBag`
  - `return View(`

#### DbContext / schema mapping
- Mau tot nhat:
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Entities\Prn22226sprB11Context.cs`
  - `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2\Entities\LibraryManagementContext.cs`
- Mo file nao:
  - `Entities\*Context.cs`
- Search:
  - `DbSet<`
  - `OnModelCreating`
  - `HasForeignKey`
  - `UsingEntity`

### Socket

#### Socket server co command
- Mau tot nhat:
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q1\Program.cs`
- Mo file nao:
  - `Program.cs`
- Search:
  - `TcpListener`
  - `AcceptTcpClientAsync`
  - `JsonSerializer.Serialize`

#### Socket server doc DB
- Mau tot nhat:
  - `HaNoi\SP25\PE_PRN222_GivenSolution1\Project11\Program.cs`
- Mo file nao:
  - `Program.cs`
  - `Entities\*Context.cs`
- Search:
  - `new PePrn25sprB5Context()`
  - `ToList()`
  - `WriteAsync`

#### Socket server tra object co nested list
- Mau tot nhat:
  - `HaNoi\SU25_BL5\ServerApp\Port4000Server\Program.cs`
- Mo file nao:
  - `Program.cs`
- Search:
  - `class ServerResponse`
  - `BorrowerRecords`
  - `OrderBy(r => r.BorrowDate)`

#### Socket client console + deserialize
- Mau tot nhat:
  - `HaNoi\SU25\Q1\Q1\Program.cs`
  - `HaNoi\SU25_BL5\PE_PRN222_GivenSolution\Q1\Program.cs`
- Mo file nao:
  - `Program.cs`
- Search:
  - `TcpClient`
  - `Deserialize<`
  - `SocketException`

#### Socket client raw response
- Mau tot nhat:
  - `HaNoi\SP26\Đề\PRN222_SP26_PE_L1_739461\PaperNo_4\1\EmployeeClient\Program.cs`
- Mo file nao:
  - `Program.cs`
- Search:
  - `ConnectAsync`
  - `Shutdown(SocketShutdown.Send)`
  - `MemoryStream`
  - `ReadAsync(buffer)`

#### WPF client
- Mau tot nhat:
  - `HaNoi\SP25\PE_PRN222_GivenSolution1\Project12\MainWindow.xaml.cs`
  - `HaNoi\SP25_BL5\PE_PRN222_GivenSolution1\Project12\MainWindow.xaml.cs`
- Mo file nao:
  - `MainWindow.xaml.cs`
  - `MainWindow.xaml`
- Search:
  - `ItemsSource`
  - `ConnectAndLoadData`
  - `SocketException`

### SQL

#### Script doi chieu schema
- Mau tot nhat:
  - `HaNoi\SP26\Đề\PRN222_SP26_PE_L1_739461\PaperNo_4\2\script1.sql`
  - `HaNoi\SP25\Đề\Given\UPLOAD\All\Database\script.sql`
  - `HaNoi\SP25_BL5\Đề\Given\UPLOAD\All\Database\script.sql`
  - `HaNoi\SU25\Đề\PRN_Sum25_B1_23.sql`
- Mo file nao:
  - `*.sql`
- Search:
  - `CREATE TABLE`
  - `FOREIGN KEY`
  - `INSERT INTO`

## Mo File Nao Truoc

| Dang bai | Thu tu mo |
|---|---|
| Web CRUD | `Program.cs` -> `Pages\...\List.cshtml.cs` -> `Create.cshtml.cs` -> `Update.cshtml.cs` -> `Detail.cshtml.cs` -> `Entities\*Context.cs` |
| Web relation | `Entities\*Context.cs` -> `Pages\...\Index.cshtml.cs` / `List.cshtml.cs` |
| Socket server | `Program.cs` -> neu can thi `Entities\*Context.cs` |
| Socket client console | `Program.cs` -> DTO/model lien quan |
| WPF client | `MainWindow.xaml.cs` -> `MainWindow.xaml` |
| SQL schema | `*.sql` -> `Entities\*Context.cs` |

## Search Keyword

### Web
- `AddDbContext`
- `UseSqlServer`
- `Results.Redirect`
- `BindProperty`
- `SupportsGet = true`
- `OnGet(`
- `OnPost(`
- `Where(`
- `OrderBy(`
- `Include(`
- `SaveChanges()`
- `RedirectToPage(`

### Socket
- `TcpListener`
- `TcpClient`
- `AcceptTcpClientAsync`
- `NetworkStream`
- `StreamReader`
- `StreamWriter`
- `JsonSerializer.Serialize`
- `JsonSerializer.Deserialize`
- `SocketException`
- `Shutdown(SocketShutdown.Send)`

### EF Core
- `DbSet<`
- `OnModelCreating`
- `HasForeignKey`
- `WithMany`
- `UsingEntity`

## Nho 6 Mau Nay

1. `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2`
2. `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q1`
3. `HaNoi\SP26\Đề\PRN222_SP26_PE_L1_739461\PaperNo_4\1\EmployeeClient`
4. `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2`
5. `HaNoi\SP25\PE_PRN222_GivenSolution1\Project11`
6. `HaNoi\SU25\Q1\Q1`

## Ghi Chu Cuoi

- Neu ra web CRUD: bat dau tu `SP26/Q2`.
- Neu ra socket server: bat dau tu `SP26/Q1`.
- Neu ra socket client: bat dau tu `SU25/Q1`, sau do doi chieu them `SP26/Đề/.../EmployeeClient`.
- Neu ra bai co relation phuc tap: mo `SP25/Project2`.
- Neu can schema gon de doi chieu nhanh: mo `SP26/Đề/.../script1.sql`.
