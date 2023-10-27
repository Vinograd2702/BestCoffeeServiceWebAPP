namespace BestCoffeeService.Models
{
    public class SortViewModel
    {
        public SortState NameCoffeeShop { get; } // значение для сортировки по названию магазина
        public SortState CoffeePrice { get; }    // значение для сортировки по цене
        public SortState CoffeeRating { get; }   // значение для сортировки по рейтингу
        public SortState TypeOfCoffee { get; }   // значение для сортировки по типу напитка
        public SortState Current { get; }     // текущее значение сортировки

        public SortViewModel(SortState sortOrder)
        {
            NameCoffeeShop = sortOrder == SortState.NameCoffeeShopAsc ? SortState.NameCoffeeShopDesc : SortState.NameCoffeeShopAsc;
            CoffeePrice = sortOrder == SortState.CoffeePriceAsc ? SortState.CoffeePriceDesc : SortState.CoffeePriceAsc;
            CoffeeRating = sortOrder == SortState.CoffeeRatingAsc ? SortState.CoffeeRatingDesc : SortState.CoffeeRatingAsc;
            TypeOfCoffee = sortOrder == SortState.TypeOfCoffeeAsc ? SortState.TypeOfCoffeeDesc : SortState.TypeOfCoffeeAsc;
            Current = sortOrder;
        }
    }
}