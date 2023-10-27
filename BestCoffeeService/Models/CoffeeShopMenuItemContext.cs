using Microsoft.EntityFrameworkCore;

namespace BestCoffeeService.Models
{
    public class CoffeeShopMenuItemContext : DbContext //контекст данных для передачи настройки контекста через options
    {
        public DbSet<CoffeeShopMenuItem> CoffeeShopMenuItems { get; set; } = null!;
        public DbSet<TypeOfCoffee> TypeOfCoffees { get; set; } = null!;
        public CoffeeShopMenuItemContext(DbContextOptions<CoffeeShopMenuItemContext> options)
            : base(options)
        {
            Database.EnsureCreated();//создадим базу, если еще не создавали
        }
    }
}
