using Microsoft.EntityFrameworkCore;
using Project2.Entities;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddDbContext<LibraryManagementContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn")));
builder.Services.AddRazorPages();

app.MapGet("/", () => "Hello World!");
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();


app.Run();
