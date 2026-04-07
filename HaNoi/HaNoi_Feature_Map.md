# HaNoi Feature Map

File nay dung de tra nhanh khi di thi.
Muc tieu: can tinh nang nao thi vao dung muc do, xem project mau, mo dung file code, roi copy y tuong va ap dung.

## 1. Tong quan bo de

| Bo de | Project chinh | Dang bai | Cong nghe | Ghi chu nhanh |
|---|---|---|---|---|
| SP26 | `PE_PRN222_GivenSolution_Practice/Q1` | Socket server | Console + TCP + JSON | Server tra danh sach / filter theo command |
| SP26 | `PE_PRN222_GivenSolution_Practice/Q2` | Web CRUD | Razor Pages + EF Core + SQL Server | Mau dep nhat cho list/create/update/detail |
| SP26 | `Đề/PRN222_SP26_PE_L1_739461/PaperNo_4/1/EmployeeClient` | Socket client | Console + TCP | Client gui command den server port 5000 va in raw response |
| SP26 | `Đề/PRN222_SP26_PE_L1_739461/PaperNo_4/2/script1.sql` | SQL script | SQL Server script | Script tao DB Customer - Orders, phu hop de doi chieu schema |
| SP25 | `PE_PRN222_GivenSolution1/Project11` | Socket server | Console + TCP + EF Core | Server doc DB va tra JSON |
| SP25 | `PE_PRN222_GivenSolution1/Project12` | Desktop client | WPF + TCP + JSON | Client goi server, hien thi DataGrid |
| SP25 | `PE_PRN222_GivenSolution1/Project2` | Web CRUD | Razor Pages + EF Core | Co include, many-to-many, remove relation |
| SP25_BL5 | `PE_PRN222_GivenSolution1/Project11` | Socket server | Console + TCP + EF Core | Giong SP25, co the doi dataset |
| SP25_BL5 | `PE_PRN222_GivenSolution1/Project12` | Desktop client | WPF + TCP + JSON | Mau WPF client |
| SP25_BL5 | `PE_PRN222_GivenSolution1/Project2` | Web CRUD | Razor Pages + EF Core | Mau list/filter/sort ro rang |
| SU25 | `Q1/Q1` | Socket client | Console + TCP + JSON | Nhap ID, goi server, deserialize va in ket qua |
| SU25 | `Q2/Q2` | Web CRUD | Razor Pages + EF Core | Mau search bang chuoi + include |
| SU25_BL5 | `PE_PRN222_GivenSolution/Q1` | Socket client | Console + TCP + JSON | Client mau khac de doi chieu |
| SU25_BL5 | `PE_PRN222_GivenSolution/Q2` | Web CRUD | Razor Pages + EF Core | Filter theo Department + doc relation EmployeeSkill |
| SU25_BL5 | `ServerApp/Port3000Server` | Socket server | Console + TCP + JSON | Server mau tra list ket qua |
| SU25_BL5 | `ServerApp/Port4000Server` | Socket server | Console + TCP + JSON | Server mau tra object co nested list |

## 2. Doc project theo thu tu nao

Neu gap bai web:
- Mo `Program.cs` truoc de xem route goc, `AddDbContext`, `AddRazorPages`.
- Mo file trong `Pages/.../*.cshtml.cs` de xem truy van va xu ly `OnGet`, `OnPost`.
- Mo file `Pages/.../*.cshtml` de xem form, table, query string, button.
- Mo `Entities/*Context.cs` de xem quan he bang, khoa ngoai, many-to-many.

Neu gap bai socket:
- Server: mo `Program.cs` truoc de xem port, command, response JSON.
- Client: mo `Program.cs` hoac `MainWindow.xaml.cs` de xem ket noi, gui request, deserialize.
- Neu can map DB: mo `Entities/*Context.cs` va entity lien quan.

## 3. Tra theo tinh nang

### 3.1 Cau hinh ASP.NET Core + EF Core + SQL Server

- Muc dich: khoi tao web app, dang ky Razor Pages, ket noi DB.
- Mau uu tien:
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Program.cs`
  - `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2\Program.cs`
  - `HaNoi\SU25\Q2\Q2\Program.cs`
- Diem can xem:
  - `builder.Services.AddDbContext<...>(opt => opt.UseSqlServer(...))`
  - `builder.Services.AddRazorPages()`
  - `app.MapRazorPages()`
- Tu khoa search:
  - `AddDbContext`
  - `UseSqlServer`
  - `AddRazorPages`
  - `MapRazorPages`

### 3.2 Redirect tu trang goc sang trang chinh

- Muc dich: vao `/` thi nhay thang den trang list.
- Mau uu tien:
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Program.cs`
  - `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2\Program.cs`
  - `HaNoi\SP25_BL5\PE_PRN222_GivenSolution1\Project2\Program.cs`
- Doan can nho:
  - `app.MapGet("/", () => Results.Redirect("/Customer/List"));`
  - `app.MapGet("/", () => Results.Redirect("/Author"));`
- Tu khoa search:
  - `MapGet`
  - `Results.Redirect`

### 3.3 List du lieu bang Razor Pages

- Muc dich: doc danh sach tu DB va bind ra table.
- Mau uu tien:
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\List.cshtml.cs`
  - `HaNoi\SU25\Q2\Q2\Pages\Services\List.cshtml.cs`
  - `HaNoi\SU25_BL5\PE_PRN222_GivenSolution\Q2\Pages\Employee\List.cshtml.cs`
- Diem can xem:
  - tao `IQueryable<T>`
  - query dieu kien truoc
  - `ToList()` cuoi cung
- Tu khoa search:
  - `public void OnGet()`
  - `IQueryable`
  - `ToList()`

### 3.4 Filter bang query string / dropdown

- Muc dich: loc du lieu theo gia tri nguoi dung chon.
- Mau uu tien:
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\List.cshtml.cs`
  - `HaNoi\SP25_BL5\PE_PRN222_GivenSolution1\Project2\Pages\Instructor\List.cshtml.cs`
  - `HaNoi\SU25\Q2\Q2\Pages\Services\List.cshtml.cs`
  - `HaNoi\SU25_BL5\PE_PRN222_GivenSolution\Q2\Pages\Employee\List.cshtml.cs`
- Diem can xem:
  - `[BindProperty(SupportsGet = true)]`
  - `if (...) query = query.Where(...)`
  - load danh sach dropdown bang `Distinct()` hoac query bang phu
- Tu khoa search:
  - `SupportsGet = true`
  - `Where(`
  - `Distinct()`

### 3.5 Sort du lieu

- Muc dich: sap xep theo ID, name, date.
- Mau uu tien:
  - `HaNoi\SP25_BL5\PE_PRN222_GivenSolution1\Project2\Pages\Instructor\List.cshtml.cs`
  - `HaNoi\SP25_BL5\Đề\PE_PRN222_GivenSolution1\Project2\Controllers\InstructorController.cs`
- Diem can xem:
  - `switch (SortBy)` hoac `switch (sortBy)`
  - `OrderBy(...)`
- Tu khoa search:
  - `OrderBy(`
  - `switch`
  - `sortBy`

### 3.6 Search chuoi khong phan biet hoa thuong

- Muc dich: search text nhu title, fee type, name.
- Mau uu tien:
  - `HaNoi\SU25\Q2\Q2\Pages\Services\List.cshtml.cs`
- Diem can xem:
  - dua input ve `ToLower()`
  - query `Contains(...)`
  - tranh null bang `(field ?? "")`
- Tu khoa search:
  - `ToLower()`
  - `Contains(`

### 3.7 Create moi bang Razor Pages

- Muc dich: tao ban ghi moi va luu DB.
- Mau uu tien:
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\Create.cshtml.cs`
- Diem can xem:
  - `[BindProperty]`
  - tao object moi
  - `_context.Table.Add(...)`
  - `_context.SaveChanges()`
  - `RedirectToPage(...)`
- Tu khoa search:
  - `BindProperty`
  - `Add(`
  - `SaveChanges()`
  - `RedirectToPage`

### 3.8 Update bang Razor Pages

- Muc dich: hien du lieu len form, sua va luu lai.
- Mau uu tien:
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\Update.cshtml.cs`
- Diem can xem:
  - `OnGet(id)` de load object cu
  - gan gia tri vao property bind
  - `OnPost()` tim lai object cu roi set field moi
  - co 2 cach update: sua tren object cu hoac `_context.Update(obj)`
- Tu khoa search:
  - `OnGet(int id)`
  - `OnPost()`
  - `FirstOrDefault`
  - `Update(`

### 3.9 Detail page

- Muc dich: xem thong tin chi tiet 1 ban ghi.
- Mau uu tien:
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\Detail.cshtml.cs`
  - `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2\Pages\Author\Index.cshtml.cs`
- Diem can xem:
  - `FirstOrDefault(...)`
  - `return Page()`
  - truong hop can relation thi them `Include(...)`
- Tu khoa search:
  - `FirstOrDefault(`
  - `Include(`

### 3.10 Include navigation property

- Muc dich: doc kem du lieu bang lien quan.
- Mau uu tien:
  - `HaNoi\SU25\Q2\Q2\Pages\Services\List.cshtml.cs`
  - `HaNoi\SU25_BL5\PE_PRN222_GivenSolution\Q2\Pages\Employee\List.cshtml.cs`
  - `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2\Pages\Author\Index.cshtml.cs`
- Diem can xem:
  - `.Include(s => s.EmployeeNavigation)`
  - `.Include(e => e.Department)`
  - `.Include(e => e.Books)`
- Tu khoa search:
  - `Include(`

### 3.11 Many-to-many va remove relation

- Muc dich: entity co bang trung gian, can them/xoa lien ket.
- Mau uu tien:
  - `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2\Entities\LibraryManagementContext.cs`
  - `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2\Pages\Author\Index.cshtml.cs`
- Diem can xem:
  - context: `HasMany(...).WithMany(...).UsingEntity<Dictionary<string, object>>`
  - page model: `book.Authors.Remove(author)`
  - sau do `_context.SaveChanges()`
- Tu khoa search:
  - `WithMany`
  - `UsingEntity`
  - `.Remove(`

### 3.12 Load dropdown tu DB

- Muc dich: hien combo box/dropdown de nguoi dung chon gia tri filter/create/update.
- Mau uu tien:
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\List.cshtml.cs`
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\Create.cshtml.cs`
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\Update.cshtml.cs`
  - `HaNoi\SU25_BL5\PE_PRN222_GivenSolution\Q2\Pages\Employee\List.cshtml.cs`
- Diem can xem:
  - `Distinct().ToList()`
  - query danh sach bang phu nhu `Departments = _context.Departments.ToList()`

### 3.13 MVC Controller list/filter/sort

- Muc dich: neu bai ra MVC thay vi Razor Pages.
- Mau uu tien:
  - `HaNoi\SP25_BL5\Đề\PE_PRN222_GivenSolution1\Project2\Controllers\InstructorController.cs`
- Diem can xem:
  - nhan tham so tren action
  - query `_context.Instructors`
  - `ViewBag` de dua dropdown va gia tri dang chon ra view
  - `return View(...)`
- Tu khoa search:
  - `IActionResult List(`
  - `ViewBag`
  - `return View(`

### 3.14 EF Core DbContext va mapping quan he

- Muc dich: xem DB co bang gi, khoa chinh, khoa ngoai, many-to-many.
- Mau uu tien:
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Entities\Prn22226sprB11Context.cs`
  - `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2\Entities\LibraryManagementContext.cs`
- Diem can xem:
  - `DbSet<T>`
  - `OnModelCreating(...)`
  - `HasOne / WithMany / HasForeignKey`
  - `UsingEntity<Dictionary<string, object>>`
- Tu khoa search:
  - `DbSet<`
  - `OnModelCreating`
  - `HasForeignKey`
  - `UsingEntity`

### 3.15 Socket server co command don gian

- Muc dich: client gui 1 command, server xu ly va tra JSON.
- Mau uu tien:
  - `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q1\Program.cs`
- Diem can xem:
  - `TcpListener`
  - `AcceptTcpClientAsync()`
  - doc bytes, doi sang string command
  - xu ly `if (command == "ALL")`
  - `JsonSerializer.Serialize(...)`
- Tu khoa search:
  - `TcpListener`
  - `AcceptTcpClientAsync`
  - `JsonSerializer.Serialize`

### 3.16 Socket server doc DB va tra JSON

- Muc dich: server lay du lieu tu EF Core roi tra ve cho client.
- Mau uu tien:
  - `HaNoi\SP25\PE_PRN222_GivenSolution1\Project11\Program.cs`
- Diem can xem:
  - nhan command `GetStudents`
  - tao `DbContext`
  - query `db.Students.ToList()`
  - serialize va write stream
- Tu khoa search:
  - `new PePrn25sprB5Context()`
  - `db.Students.ToList()`

### 3.17 Socket server tra object co nested list

- Muc dich: response khong phai list don, ma la object chua thong tin tong + list con.
- Mau uu tien:
  - `HaNoi\SU25_BL5\ServerApp\Port4000Server\Program.cs`
- Diem can xem:
  - class `ServerResponse`
  - class `BorrowerRecord`
  - `GetBookBorrowerResponse(int bookId)`
  - sort nested list truoc khi tra
- Tu khoa search:
  - `class ServerResponse`
  - `BorrowerRecords`
  - `OrderBy(r => r.BorrowDate)`

### 3.18 Socket client console

- Muc dich: nhap input, gui len server, nhan JSON, deserialize, in ket qua.
- Mau uu tien:
  - `HaNoi\SP26\Đề\PRN222_SP26_PE_L1_739461\PaperNo_4\1\EmployeeClient\Program.cs`
  - `HaNoi\SU25\Q1\Q1\Program.cs`
  - `HaNoi\SU25_BL5\PE_PRN222_GivenSolution\Q1\Program.cs`
- Diem can xem:
  - `TcpClient client = new();`
  - `client.Connect(...)`
  - `StreamWriter` / `StreamReader` hoac `NetworkStream`
  - voi mau SP26: gui command text, `Shutdown(SocketShutdown.Send)`, doc den het stream roi in raw response
  - `JsonSerializer.Deserialize<List<ProjectDTO>>(...)`
  - bat `SocketException`
- Tu khoa search:
  - `TcpClient`
  - `Connect(`
  - `Deserialize<`
  - `SocketException`

### 3.19 WPF client goi socket server

- Muc dich: desktop app bam nut de load data tu server.
- Mau uu tien:
  - `HaNoi\SP25\PE_PRN222_GivenSolution1\Project12\MainWindow.xaml.cs`
  - `HaNoi\SP25_BL5\PE_PRN222_GivenSolution1\Project12\MainWindow.xaml.cs`
- Diem can xem:
  - method `ConnectAndLoadData()`
  - `client.Connect("127.0.0.1", 8080)`
  - `JsonSerializer.Deserialize<List<Student>>(response)`
  - `dgStudents.ItemsSource = students`
  - hien status thanh cong / that bai
- Tu khoa search:
  - `ItemsSource`
  - `ConnectAndLoadData`
  - `SocketException`

### 3.20 SQL script / database de doi chieu schema

- Muc dich: khi can xem bang, cot, du lieu mau, khoa ngoai.
- File nen mo:
  - `HaNoi\SP26\Đề\PRN222_SP26_PE_L1_739461\PaperNo_4\2\script1.sql`
  - `HaNoi\SP25\Đề\Given\UPLOAD\All\Database\script.sql`
  - `HaNoi\SP25_BL5\Đề\Given\UPLOAD\All\Database\script.sql`
  - `HaNoi\SP25\Đề\PRN222_PE_SP25_650244\PaperNo_2\All\Database\database2.sql`
  - `HaNoi\SU25_BL5\Đề\PRN222_SU25_B5_PE_212737\PaperNo_2\2\database.sql`
  - `HaNoi\SU25\Đề\PRN_Sum25_B1_23.sql`

### 3.21 SP26 bo mau moi vua them

- Muc dich: dung de hoc nhanh cap server-client cung mot bai va doi chieu SQL.
- Thanh phan nen mo:
  - server: `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q1\Program.cs`
  - client: `HaNoi\SP26\Đề\PRN222_SP26_PE_L1_739461\PaperNo_4\1\EmployeeClient\Program.cs`
  - script DB: `HaNoi\SP26\Đề\PRN222_SP26_PE_L1_739461\PaperNo_4\2\script1.sql`
- Diem can xem:
  - client SP26 gui command `ALL` hoac ten department den port `5000`
  - server SP26 doc command va tra JSON
  - script SQL SP26 tao 2 bang `Customer` va `Orders`, rat hop de doi chieu voi `SP26/Q2`

## 4. File nen uu tien mo truoc theo dang bai

### Web Razor Pages CRUD

1. `Program.cs`
2. `Pages/<Module>/List.cshtml.cs`
3. `Pages/<Module>/Create.cshtml.cs`
4. `Pages/<Module>/Update.cshtml.cs`
5. `Pages/<Module>/Detail.cshtml.cs`
6. `Entities/*Context.cs`
7. `Pages/<Module>/*.cshtml`

Mau goi y mo truoc:
- `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2`

### Web co relation / include / many-to-many

1. `Entities/*Context.cs`
2. `Pages/.../*.cshtml.cs`

Mau goi y mo truoc:
- `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2`

### Socket server

1. `Program.cs`
2. neu dung DB thi mo them `Entities/*Context.cs`

Mau goi y mo truoc:
- `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q1`
- `HaNoi\SP25\PE_PRN222_GivenSolution1\Project11`
- `HaNoi\SU25_BL5\ServerApp\Port4000Server`

### Socket client console

1. `Program.cs`
2. xem DTO trong cung file hoac model lien quan

Mau goi y mo truoc:
- `HaNoi\SU25\Q1\Q1`

### WPF client

1. `MainWindow.xaml.cs`
2. `MainWindow.xaml`

Mau goi y mo truoc:
- `HaNoi\SP25\PE_PRN222_GivenSolution1\Project12`

## 5. Search keyword dung nhanh

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

### EF Core

- `DbSet<`
- `OnModelCreating`
- `HasForeignKey`
- `WithMany`
- `UsingEntity`

## 6. Mau project nen hoc ky nhat

Neu chi duoc nho it project, uu tien 6 cai nay:

1. `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2`
   - Mau web CRUD ro rang nhat: list, create, update, detail, redirect, dropdown.
2. `HaNoi\SP25_BL5\PE_PRN222_GivenSolution1\Project2`
   - Mau filter + sort ro rang.
3. `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2`
   - Mau include + many-to-many + remove relation.
4. `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q1`
   - Mau socket server command-based rat de hoc.
5. `HaNoi\SP25\PE_PRN222_GivenSolution1\Project11`
   - Mau socket server ket hop EF Core.
6. `HaNoi\SU25\Q1\Q1`
   - Mau socket client console gon, de ap dung.

Bo sung nen nho:
- `HaNoi\SP26\Đề\PRN222_SP26_PE_L1_739461\PaperNo_4\1\EmployeeClient`
  - Mau socket client rat gon, hop de noi cap voi `SP26/Q1`.
- `HaNoi\SP26\Đề\PRN222_SP26_PE_L1_739461\PaperNo_4\2\script1.sql`
  - Mau SQL ngan, de map nhanh schema Customer - Orders.

## 7. Cach dung file nay luc di thi

1. Xac dinh bai thuoc dang nao: web, socket server, socket client, WPF.
2. Tim muc tinh nang gan nhat trong phan 3.
3. Mo dung file mau duoc liet ke.
4. Lay logic query, binding, save, socket, deserialize tu file mau.
5. Neu chua ro schema DB, mo them file SQL hoac `*Context.cs`.

## 8. Ghi chu cuoi

- `SP26/Q2` la mau web CRUD de hoc dau tien.
- `SP26/Đề/.../EmployeeClient` la mau socket client moi, rat hop de hoc cung `SP26/Q1`.
- `SP26/Đề/.../script1.sql` giup doi chieu nhanh schema cua bai SP26.
- `SP25/Project2` la mau relation phuc tap de hoc sau.
- `SP26/Q1` va `SP25/Project11` la 2 mau socket server quan trong nhat.
- `SU25/Q1` la mau socket client console de copy nhanh.
- `SP25/Project12` la mau WPF client de tham khao khi de ra desktop.
