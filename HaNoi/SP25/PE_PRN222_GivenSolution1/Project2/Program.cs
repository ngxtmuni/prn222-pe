using Microsoft.EntityFrameworkCore;
using Project2.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryManagementContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn")));

builder.Services.AddRazorPages();
var app = builder.Build();

app.MapGet("/", () => Results.Redirect("/Author"));
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();


app.Run();
