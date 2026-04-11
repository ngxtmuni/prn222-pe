# HaNoi View CheatSheet

File nay chua snippet mau cho phan `.cshtml`.
Muc tieu: khi de ra view, form, table, filter, detail, action button, chi can mo file nay va thay ten model / property / route.

## 1. Cach Dung Nhanh

- Bai list: xem `table + foreach + link detail`.
- Bai filter dropdown: xem `form method="get" + select asp-for`.
- Bai search text: xem `input value="@Model.X"`.
- Bai create/update: xem `form method="post" + asp-for + validation summary`.
- Bai detail: xem `if (Model.Item == null)` + bang 2 cot.
- Bai remove relation: xem `asp-page-handler` + `asp-route-*`.

## 2. Razor Pages View Co Ban

### 2.1 Khung page co @page va @model

Dung khi:
- Tao 1 Razor Page view co ban.

Snippet:

```cshtml
@page
@model ProjectNamespace.Pages.Module.ListModel
@{
    Layout = null;
}

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Page Title</title>
</head>
<body>
    <h2>Page Content</h2>
</body>
</html>
```

Can thay:
- `ProjectNamespace.Pages.Module.ListModel`
- `Page Title`
- noi dung trong `body`

Nguon:
- `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\List.cshtml`

Luu y nhanh:
- `@page` bat buoc cho Razor Pages.
- `@model` phai trung voi class `PageModel` ben `.cshtml.cs`.

### 2.2 Table list co link detail

Dung khi:
- Hien danh sach.
- Muon click vao ten hoac ID de sang trang detail.

Snippet:

```cshtml
@page
@model ProjectNamespace.Pages.Module.ListModel

<h2>List</h2>

<table border="1" cellpadding="6" cellspacing="0">
    <thead>
        <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Email</th>
            <th>City</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model.Items)
        {
            <tr>
                <td>@item.Id</td>
                <td>
                    <a asp-page="/Module/Detail" asp-route-id="@item.Id">
                        @item.Name
                    </a>
                </td>
                <td>@item.Email</td>
                <td>@item.City</td>
            </tr>
        }
    </tbody>
</table>
```

Can thay:
- `Model.Items`
- `Id`, `Name`, `Email`, `City`
- `"/Module/Detail"`

Nguon:
- `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\List.cshtml`

Luu y nhanh:
- `asp-page` + `asp-route-id` la cach gon nhat de tao link detail dung chuan Razor Pages.
- Neu khong can tag helper, co the dung `href="/Module/Detail/@item.Id"`.

### 2.3 Form filter bang dropdown

Dung khi:
- Loc theo city, department, category, status.

Snippet:

```cshtml
<form method="get" style="margin-bottom:12px;">
    <select asp-for="SelectedValue">
        <option value="" selected="@(string.IsNullOrEmpty(Model.SelectedValue))">All</option>
        @for (int i = 0; i < Model.Options.Count; i++)
        {
            var option = Model.Options[i];
            if (string.Equals(Model.SelectedValue, option, StringComparison.OrdinalIgnoreCase))
            {
                <option value="@option" selected>@option</option>
            }
            else
            {
                <option value="@option">@option</option>
            }
        }
    </select>

    <button type="submit">Filter</button>
</form>
```

Can thay:
- `SelectedValue`
- `Model.Options`
- text `All`

Nguon:
- `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\List.cshtml`

Luu y nhanh:
- Dung `method="get"` de gia tri filter di theo URL.
- Cach render selected thu cong nay de dung cho bai thi.

### 2.4 Form filter bang dropdown + radio sort

Dung khi:
- Vua loc, vua sort tren cung 1 form.

Snippet:

```cshtml
<form method="get">
    Department
    <select asp-for="DepartmentId">
        @for (int i = 0; i < Model.Departments.Count; i++)
        {
            var d = Model.Departments[i];
            var isSelected = Model.DepartmentId == d.DepartmentId || (!Model.DepartmentId.HasValue && i == 0);
            if (isSelected)
            {
                <option value="@d.DepartmentId" selected>@d.DepartmentName</option>
            }
            else
            {
                <option value="@d.DepartmentId">@d.DepartmentName</option>
            }
        }
    </select>

    Contract Type
    <select asp-for="ContractType">
        <option value="true">Fulltime</option>
        <option value="false">Parttime</option>
    </select>

    <br />
    Sort by:
    <input type="radio" asp-for="SortBy" value="InstructorName" /> InstructorName
    <input type="radio" asp-for="SortBy" value="InstructorId" /> InstructorId
    <input type="radio" asp-for="SortBy" value="ContractDate" /> ContractDate
    <br />

    checkbox
    @foreach (var item in Model.Books)
    {
        <input type="checkbox" name="SelectedBookIds" value="@item.BookId" />
        <span>@item.Title</span>
    }
    [BindProperty]
    public List<int> SelectedBookIds { get; set; } = new();

    //example
    1. Radio
Dùng khi chỉ chọn 1.
<input type="radio" asp-for="SortBy" value="Name" /> Name
<input type="radio" asp-for="SortBy" value="Id" /> Id
[BindProperty(SupportsGet = true)]
public string? SortBy { get; set; }
Ý chính
- Nhiều radio cùng asp-for
- Mỗi cái khác value
- Chỉ nhận 1 giá trị cuối cùng được chọn
2. Checkbox Bool
Dùng khi field là bật/tắt, ví dụ IsActive, IsFulltime.
<input type="checkbox" asp-for="IsActive" />
<label asp-for="IsActive"></label>
[BindProperty]
public bool IsActive { get; set; }
Ý chính
- Cái này dùng asp-for rất ổn
- Bind trực tiếp về bool
3. Checkbox Multiple
Dùng khi chọn nhiều dòng, nhiều category, nhiều book, nhiều role.
@foreach (var book in Model.Books)
{
    <div>
        <input type="checkbox" name="SelectedBookIds" value="@book.BookId" />
        <span>@book.Title</span>
    </div>
}
[BindProperty]
public List<int> SelectedBookIds { get; set; } = new();
Ý chính
- Đây là chỗ khác radio
- Không nên nghĩ nó giống radio hoàn toàn
- Nhiều checkbox phải dùng cùng name
- Server sẽ gom các value đã tick thành List<int> hoặc List<string>
4. Select Một Giá Trị
Dùng khi dropdown chọn 1.
<select asp-for="DepartmentId">
    <option value="1">IT</option>
    <option value="2">HR</option>
</select>
[BindProperty(SupportsGet = true)]
public int DepartmentId { get; set; }
Ý chính
- Dễ nhất cho chọn 1
- Hay dùng cho filter hoặc create/update
5. Select Multiple
Dùng khi dropdown cho chọn nhiều.
<select name="SelectedBookIds" multiple>
    @foreach (var book in Model.Books)
    {
        <option value="@book.BookId">@book.Title</option>
    }
</select>
[BindProperty]
public List<int> SelectedBookIds { get; set; } = new();

    <input type="submit" value="Filter" />
</form>
```

Can thay:
- `DepartmentId`, `ContractType`, `SortBy`
- `Model.Departments`
- gia tri `value` cua radio sort

Nguon:
- `HaNoi\SP25_BL5\PE_PRN222_GivenSolution1\Project2\Pages\Instructor\List.cshtml`

Luu y nhanh:
- Cac input radio voi `asp-for` se bind ve property ben `PageModel`.
- Neu bai khong can radio, co the doi sang `select` de gon hon.

### 2.5 Form search bang input text

Dung khi:
- Search theo title, name, fee type, keyword.

Snippet:

```cshtml
<form method="get" action="/Services/List">
    <div class="mb-2">
        <label for="in_roomTitle">Room Title:</label>
        <input id="in_roomTitle"
               name="RoomTitle"
               type="text"
               class="form-control"
               value="@Model.RoomTitle" />
    </div>

    <div class="mb-2">
        <label for="in_feeType">Fee Type:</label>
        <input id="in_feeType"
               name="FeeType"
               type="text"
               class="form-control"
               value="@Model.FeeType" />
    </div>

    <button type="submit" class="btn btn-primary">Search</button>
</form>
```

Can thay:
- `action`
- `RoomTitle`, `FeeType`
- text label

Nguon:
- `HaNoi\SU25\Q2\Q2\Pages\Services\List.cshtml`

Luu y nhanh:
- Dung `value="@Model.X"` de giu lai gia tri da search.
- Neu da dung `asp-for`, co the bo `name` di. Neu muon ro rang va de nho thi giu nhu mau tren.

### 2.6 List co relation va format gia tri

Dung khi:
- Can hien thi field tu bang lien quan.
- Can format amount, date, status ngay tren view.

Snippet:

```cshtml
<table class="table mt-3">
    <thead>
        <tr>
            <th>Room Title</th>
            <th>Amount</th>
            <th>Status</th>
            <th>Employee</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model.Items)
        {
            var amount = (int)decimal.Round(item.Amount ?? 0m, 0, MidpointRounding.AwayFromZero);
            var paymentStatus = item.PaymentDate.HasValue ? "Paid" : "Unpaid";
            var employeeName = item.EmployeeNavigation?.Name ?? "N/A";

            <tr>
                <td>@item.RoomTitle</td>
                <td>@amount</td>
                <td>@paymentStatus</td>
                <td>@employeeName</td>
            </tr>
        }
    </tbody>
</table>
```

Can thay:
- `Model.Items`
- `Amount`, `PaymentDate`, `EmployeeNavigation`, `RoomTitle`

Nguon:
- `HaNoi\SU25\Q2\Q2\Pages\Services\List.cshtml`

Luu y nhanh:
- Dung `?.` khi navigation property co the null.
- View co the format gia tri de output dep hon ma khong can sua model.

### 2.7 Create form

Dung khi:
- Tao moi ban ghi.

Snippet:

```cshtml
@page
@model ProjectNamespace.Pages.Module.CreateModel

<h2>Create</h2>

<form method="post">
    <div style="margin-bottom:10px;">
        <label for="txt_name">Name</label><br />
        <input type="text" id="txt_name" asp-for="Name" />
    </div>

    <div style="margin-bottom:10px;">
        <label for="txt_email">Email</label><br />
        <input type="text" id="txt_email" asp-for="Email" />
    </div>

    <div style="margin-bottom:10px;">
        <label for="sl_value">Value</label><br />
        <select id="sl_value" asp-for="SelectedValue">
            @for (int i = 0; i < Model.Options.Count; i++)
            {
                var value = Model.Options[i];
                <option value="@value">@value</option>
            }
        </select>
    </div>

    <div asp-validation-summary="All" style="color:red"></div>

    <div style="margin-top:12px;">
        <button type="submit">Save</button>
        <a href="/Module/List">Back to List</a>
    </div>
</form>
```

Can thay:
- `CreateModel`
- `Name`, `Email`, `SelectedValue`
- `Model.Options`
- route `"/Module/List"`

Nguon:
- `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\Create.cshtml`

Luu y nhanh:
- `asp-validation-summary="All"` rat huu ich de hien loi tu `ModelState`.
- Nut back nen tro ve trang list de de test lai.

### 2.8 Update form

Dung khi:
- Sua ban ghi co san.

Snippet:

```cshtml
@page "{id:int}"
@model ProjectNamespace.Pages.Module.UpdateModel

@if (Model.Item == null)
{
    <div>Item not found.</div>
    <a href="/Module/List">Back to List</a>
}
else
{
    <form method="post">
        <input type="hidden" asp-for="Id" />

        <div>
            <label>ID</label><br />
            <input type="text" value="@Model.Id" readonly />
        </div>

        <div>
            <label>Name</label><br />
            <input type="text" asp-for="Name" />
        </div>

        <div>
            <label>Email</label><br />
            <input type="text" asp-for="Email" />
        </div>

        <div>
            <label>Value</label><br />
            <select asp-for="SelectedValue">
                @for (int i = 0; i < Model.Options.Count; i++)
                {
                    var value = Model.Options[i];
                    if (string.Equals(Model.SelectedValue, value, StringComparison.OrdinalIgnoreCase))
                    {
                        <option value="@value" selected>@value</option>
                    }
                    else
                    {
                        <option value="@value">@value</option>
                    }
                }
            </select>
        </div>

        <div asp-validation-summary="All" style="color:red"></div>

        <div style="margin-top:12px;">
            <button type="submit">Update</button>
            <a href="/Module/Detail/@Model.Id">Back to Detail</a>
            <a href="/Module/List">Back to List</a>
        </div>
    </form>
}
```

Can thay:
- `UpdateModel`
- `Item`, `Id`, `Name`, `Email`, `SelectedValue`, `Options`
- cac route `/Module/...`

Nguon:
- `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\Update.cshtml`

Luu y nhanh:
- `@page "{id:int}"` dung khi trang nhan `id` tu route.
- Hidden input giu khoa chinh khi submit.
- Readonly input de hien ID ma khong cho sua.

### 2.9 Detail page bang 2 cot

Dung khi:
- Hien thong tin chi tiet 1 ban ghi.

Snippet:

```cshtml
@page "{id:int}"
@model ProjectNamespace.Pages.Module.DetailModel

<h2>Detail</h2>

@if (Model.Item == null)
{
    <div class="alert alert-danger">Item not found.</div>
}
else
{
    <table class="table table-bordered">
        <tr>
            <th style="width:200px;">Id</th>
            <td>@Model.Item.Id</td>
        </tr>
        <tr>
            <th>Name</th>
            <td>@Model.Item.Name</td>
        </tr>
        <tr>
            <th>Email</th>
            <td>@Model.Item.Email</td>
        </tr>
    </table>

    <div class="mt-3">
        <a href="/Module/Update/@Model.Item.Id" class="btn btn-primary">Update</a>
        <a href="/Module/List" class="btn btn-secondary">Back to List</a>
    </div>
}
```

Can thay:
- `DetailModel`
- `Item.Id`, `Item.Name`, `Item.Email`
- route `/Module/...`

Nguon:
- `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\Detail.cshtml`

Luu y nhanh:
- Kieu bang 2 cot nay de doc va de lam nhat cho bai thi.

### 2.10 List co filter dropdown + action link detail

Dung khi:
- Muon ket hop filter va table trong cung mot trang.

Snippet:

```cshtml
<h1>List of authors</h1>

<form method="get">
    <label for="book">Filter by book</label>
    <select id="book" asp-for="BookId">
        <option value="0" selected="@(Model.BookId == 0)">All books</option>
        @foreach (var book in Model.Books)
        {
            <option value="@book.BookId" selected="@(Model.BookId == book.BookId)">@book.Title</option>
        }
    </select>
    <button type="submit">Filter</button>
</form>

<table>
    <thead>
        <tr>
            <th>AuthorId</th>
            <th>Name</th>
            <th>Books</th>
            <th>Action</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var a in Model.Authors)
        {
            <tr>
                <td>@a.AuthorId</td>
                <td>@a.Name</td>
                <td>@string.Join(", ", a.Books.Select(b => b.Title))</td>
                <td><a href="/Author/@a.AuthorId">View Details</a></td>
            </tr>
        }
    </tbody>
</table>
```

Can thay:
- `BookId`, `Books`, `Authors`
- route detail
- field hien thi trong bang

Nguon:
- `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2\Pages\Author.cshtml`

Luu y nhanh:
- `@string.Join(...)` rat hop khi can hien thi danh sach lien quan trong 1 cell.

### 2.11 Form post voi asp-page-handler

Dung khi:
- Can bam nut remove / action tren tung dong du lieu.
- Muon post ve mot handler rieng trong `PageModel`.

Snippet:

```cshtml
<table>
    <tbody>
        @foreach (var b in Model.Books)
        {
            <tr>
                <td>@b.BookId</td>
                <td>@b.Title</td>
                <td>
                    <form method="post"
                          asp-page-handler="RemoveAuthor"
                          asp-route-authorId="@Model.Author!.AuthorId"
                          asp-route-bookId="@b.BookId">
                        <button type="submit">Remove</button>
                    </form>
                </td>
            </tr>
        }
    </tbody>
</table>
```

Can thay:
- `RemoveAuthor`
- `authorId`, `bookId`
- ten collection va ten field

Nguon:
- `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2\Pages\Author\Index.cshtml`

Luu y nhanh:
- `asp-page-handler="RemoveAuthor"` se map vao method `OnPostRemoveAuthor(...)`.
- `asp-route-*` la cach de truyen tham so tu dong button.

## 3. MVC View Co Ban

### 3.1 Form filter / sort MVC

Dung khi:
- Bai dung controller + view thay vi Razor Pages.

Snippet:

```cshtml
<form method="get">
    <select name="departmentId">
        <option value="">All</option>
        @foreach (var d in ViewBag.Departments)
        {
            <option value="@d.DepartmentId">@d.DepartmentName</option>
        }
    </select>

    <select name="contractType">
        <option value="Fulltime">Fulltime</option>
        <option value="Parttime">Parttime</option>
    </select>

    <input type="radio" name="sortBy" value="InstructorId" /> InstructorId
    <input type="radio" name="sortBy" value="ContractDate" /> ContractDate

    <button type="submit">Filter</button>
</form>
```

Can thay:
- ten `name` trung voi tham so action trong controller.
- nguon dropdown co the tu `ViewBag`, `ViewData` hoac model.

Nguon:
- pattern doi chieu tu `HaNoi\SP25_BL5\Đề\PE_PRN222_GivenSolution1\Project2\Controllers\InstructorController.cs`

Luu y nhanh:
- MVC thuong bind theo `name` cua input.

### 3.2 Table list MVC / Razor deu dung duoc

Dung khi:
- Muon 1 bang list gon de tai su dung.

Snippet:

```cshtml
<table border="1">
    <thead>
        <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Department</th>
            <th>Date</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model.Items)
        {
            <tr>
                <td>@item.Id</td>
                <td>@item.Name</td>
                <td>@item.DepartmentNavigation?.DepartmentName</td>
                <td>@item.CreatedDate?.ToString("dd/MM/yyyy")</td>
            </tr>
        }
    </tbody>
</table>
```

Can thay:
- `Model.Items`
- ten field

Nguon:
- `HaNoi\SP25_BL5\PE_PRN222_GivenSolution1\Project2\Pages\Instructor\List.cshtml`

Luu y nhanh:
- Format date ngay trong view rat tien cho bai thi.

## 4. Mau Thay Nhanh

### 4.1 Table list skeleton

```cshtml
<table>
    <thead>
        <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Action</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model.Items)
        {
            <tr>
                <td>@item.Id</td>
                <td>@item.Name</td>
                <td><a asp-page="/Module/Detail" asp-route-id="@item.Id">View</a></td>
            </tr>
        }
    </tbody>
</table>
```

### 4.2 Filter form skeleton

```cshtml
<form method="get">
    <select asp-for="SelectedValue">
        <option value="">All</option>
        @foreach (var option in Model.Options)
        {
            <option value="@option">@option</option>
        }
    </select>
    <button type="submit">Filter</button>
</form>
```

### 4.3 Search form skeleton

```cshtml
<form method="get">
    <input type="text" name="Keyword" value="@Model.Keyword" />
    <button type="submit">Search</button>
</form>
```

### 4.4 Create / update form skeleton

```cshtml
<form method="post">
    <input type="hidden" asp-for="Id" />

    <div>
        <label>Name</label>
        <input asp-for="Name" />
    </div>

    <div>
        <label>Email</label>
        <input asp-for="Email" />
    </div>

    <div asp-validation-summary="All" style="color:red"></div>

    <button type="submit">Save</button>
</form>
```

### 4.5 Detail skeleton

```cshtml
@if (Model.Item == null)
{
    <div>Item not found.</div>
}
else
{
    <table>
        <tr><th>Id</th><td>@Model.Item.Id</td></tr>
        <tr><th>Name</th><td>@Model.Item.Name</td></tr>
    </table>
}
```

### 4.6 Handler form skeleton

```cshtml
<form method="post"
      asp-page-handler="RemoveItem"
      asp-route-id="@item.Id">
    <button type="submit">Remove</button>
</form>
```

## 5. Loi View Hay Gap

- Quen `method="get"` cho filter/search.
- Quen `method="post"` cho create/update/remove.
- Quen `asp-for` nen input khong bind ve model.
- Quen `asp-route-id` nen link detail sai.
- Quen `asp-page-handler` nen post khong vao dung method.
- Dropdown khong giu selected value sau khi submit.
- Khong dung `?.` khi navigation property co the null.
- Route page co `@page "{id:int}"` nhung link lai khong truyen `id`.
- Dung `View(...)`/MVC pattern cho Razor Pages hoac nguoc lai.

## 6. Nguon Mau Tot Nhat

- `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\List.cshtml`
- `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\Create.cshtml`
- `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\Update.cshtml`
- `HaNoi\SP26\PE_PRN222_GivenSolution_Practice\Q2\Pages\Customer\Detail.cshtml`
- `HaNoi\SU25\Q2\Q2\Pages\Services\List.cshtml`
- `HaNoi\SP25_BL5\PE_PRN222_GivenSolution1\Project2\Pages\Instructor\List.cshtml`
- `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2\Pages\Author.cshtml`
- `HaNoi\SP25\PE_PRN222_GivenSolution1\Project2\Pages\Author\Index.cshtml`
