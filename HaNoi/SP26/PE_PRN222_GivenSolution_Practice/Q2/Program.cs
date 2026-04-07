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
