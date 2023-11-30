using Microsoft.EntityFrameworkCore;
using BestCoffeeService.Models;
using BestCoffeeService.DAL.EFC;
using BestCoffeeService.BLL.Interfaces;
using BestCoffeeService.BLL.Services;
using BestCoffeeService.DAL.Interfaces;
using BestCoffeeService.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

string connection = "Server = (localdb)\\mssqllocaldb;Database = coffeedb;Trusted_Connection=true";

builder.Services.AddDbContext<BestCoffeeContext>(options =>//подключил бд с контекстом
{
    options.UseSqlServer(connection, sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    });
});

//AddScoped
builder.Services.AddScoped<ICoffeeShopOrderService, CoffeeShopOrderService>();//добавил сервис
builder.Services.AddScoped<IClientOrderRepository, ClientOrderRepository>();
builder.Services.AddScoped<ITypeOfCoffeeRepository, TypeOfCoffeeRepository>();
//builder.Services.AddDbContext<CoffeeShopMenuItemContext>(options => options.UseSqlServer(connection));
//builder.Services.AddSingleton
builder.Services.AddControllersWithViews();


var app = builder.Build();

app.MapDefaultControllerRoute();

app.Run();
