using Microsoft.EntityFrameworkCore;
using Project2.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); 
builder.Services.AddDbContext<PePrn25sprB5Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Instructor}/{action=List}/{id?}");

app.Run();
