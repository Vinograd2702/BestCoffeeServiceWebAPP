using Microsoft.EntityFrameworkCore;
using BestCoffeeService.Models;

var builder = WebApplication.CreateBuilder(args);

string connection = "Server = (localdb)\\mssqllocaldb;Database = coffeedb;Trusted_Connection=true";
builder.Services.AddDbContext<CoffeeShopMenuItemContext>(options => options.UseSqlServer(connection));

builder.Services.AddControllersWithViews();


var app = builder.Build();

app.MapDefaultControllerRoute();

app.Run();
