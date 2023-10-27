namespace BestCoffeeService.Models
{
    public class CoffeeShopMenuItem //позиция кофе в меню магазина
    {
        public int Id { get; set; }
        public string? NameCoffeeShop { get; set; } //название магазина кофе
        public int CoffeePrice { get; set; } //цена за кофе
        public int CoffeeRating { get; set; } //оценка качества кофе
        public int? TypeOfCoffeeID { get; set; } //тип кофе
        public TypeOfCoffee? TypeOfCoffee { get; set; } //тип кофе
    }

    public class TypeOfCoffee
    {
        public int Id { get; set; }
        public string? NameTypeOfCoffee { get; set; }
        public List<CoffeeShopMenuItem> CoffeeShopMenuItems { get; set; } = new();
    }
}
