namespace BestCoffeeService.Models
{
    public class IndexViewModel
    {
        public IEnumerable<CoffeeShopMenuItem> CoffeeShopMenuItems { get; }
        public PageViewModel PageViewModel { get; }
        public FilterViewModel FilterViewModel { get; }
        public SortViewModel SortViewModel { get; }
        public IndexViewModel(IEnumerable<CoffeeShopMenuItem> coffeeShopMenuItems, PageViewModel pageViewModel,
            FilterViewModel filterViewModel, SortViewModel sortViewModel)
        {
            CoffeeShopMenuItems = coffeeShopMenuItems;
            PageViewModel = pageViewModel;
            FilterViewModel = filterViewModel;
            SortViewModel = sortViewModel;
        }
    }
}
