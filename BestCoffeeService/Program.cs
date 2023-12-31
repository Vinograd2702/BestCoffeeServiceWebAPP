using Microsoft.EntityFrameworkCore;
using BestCoffeeService.Models;
using BestCoffeeService.DAL.EFC;
using BestCoffeeService.BLL.Interfaces;
using BestCoffeeService.BLL.Services;
using BestCoffeeService.DAL.Interfaces;
using BestCoffeeService.DAL.Repositories;
using MyBackgroundService.Services;

var builder = WebApplication.CreateBuilder(args);

string connection = "Server = (localdb)\\mssqllocaldb;Database = coffeedb;Trusted_Connection=true";

builder.Services.AddDbContext<BestCoffeeContext>(options =>//��������� �� � ����������
{
    options.UseSqlServer(connection, sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    });
});


builder.Services.AddScoped<ICoffeeShopOrderService, CoffeeShopOrderService>();//������� ������
builder.Services.AddScoped<IClientOrderRepository, ClientOrderRepository>();
builder.Services.AddScoped<ITypeOfCoffeeRepository, TypeOfCoffeeRepository>();

builder.Services.AddHostedService<OrderExecutorBackgroundService>();

builder.Services.AddControllersWithViews();


var app = builder.Build();

app.MapDefaultControllerRoute();

app.Run();
