namespace BestCoffeeService.Models
{
    public class SortViewModel
    {
        public SortState NameOfClient { get; } // значение для сортировки по имени клиента
        public SortState OrderDate { get; }    // значение для сортировки по дате
        public SortState Current { get; }     // текущее значение сортировки

        public SortViewModel(SortState sortOrder)
        {
            NameOfClient = sortOrder == SortState.NameOfClientAsc ? SortState.NameOfClientDesc : SortState.NameOfClientAsc;
            OrderDate = sortOrder == SortState.OrderDateAsc ? SortState.OrderDateDesc : SortState.OrderDateAsc;
            Current = sortOrder;
        }
    }
}