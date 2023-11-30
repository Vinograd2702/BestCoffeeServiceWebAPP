using BestCoffeeService.BLL.DTO;

namespace BestCoffeeService.Models
{
    public class IndexViewModel
    {
        public IEnumerable<ClientOrderDTO> ClientOrderDTOs { get; }
        public PageViewModel PageViewModel { get; }
        public FilterViewModel FilterViewModel { get; }
        public SortViewModel SortViewModel { get; }
        public IndexViewModel(IEnumerable<ClientOrderDTO> clientOrderDTOs, PageViewModel pageViewModel,
            FilterViewModel filterViewModel, SortViewModel sortViewModel)
        {
            ClientOrderDTOs = clientOrderDTOs;
            PageViewModel = pageViewModel;
            FilterViewModel = filterViewModel;
            SortViewModel = sortViewModel;
        }
    }
}
