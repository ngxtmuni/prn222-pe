using Microsoft.EntityFrameworkCore;
using Q2.Entities;

var builder = WebApplication.CreateBuilder(args);

//Use the connection string below to connect to the database.
var connectionStr = builder.Configuration.GetConnectionString("MyCnn");

builder.Services.AddDbContext<PePrnSum25B5WaContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn")));

builder.Services.AddRazorPages();


var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.MapGet("/", () => "Hello World!");

app.Run();
