using BestCoffeeService.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCoffeeService.DAL.EFC
{
    public class BestCoffeeContext : DbContext //контекст данных для передачи настройки контекста через options
    {
        public DbSet<ClientOrder> ClientOrders { get; set; } = null!;
        public DbSet<TypeOfCoffee> TypeOfCoffees { get; set; } = null!;
        public BestCoffeeContext(DbContextOptions<BestCoffeeContext> options)
       : base(options)
        {
            Database.EnsureCreated();//создадим базу, если еще не создавали
        }
    }
}