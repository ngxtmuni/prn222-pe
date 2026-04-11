using Microsoft.EntityFrameworkCore;
using Q2.Models;

var builder = WebApplication.CreateBuilder(args);

//Use the connection string below to connect to the database.
var connectionStr = builder.Configuration.GetConnectionString("MyCnn");

builder.Services.AddDbContext<PRN222_TestQuestion_Paper3Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn")));

builder.Services.AddRazorPages();

var app = builder.Build();

app.MapGet("/", () => Results.Redirect("/Services/List"));
app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();

app.Run();
