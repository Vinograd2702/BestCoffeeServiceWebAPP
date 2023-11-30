using BestCoffeeService.BLL.DTO;
using BestCoffeeService.DAL.Entities;
using BestCoffeeService.DAL.Interfaces;
using BestCoffeeService.DAL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCoffeeService.BLL.Interfaces
{
    public interface ICoffeeShopOrderService
    {
        //заказы
        Task<IBaseResponse<IEnumerable<ClientOrderDTO>>> GetClientOrders();
        Task<IBaseResponse<ClientOrderDTO>> GetClientOrder(int id);
        Task<IBaseResponse<ClientOrderDTO>> GetClientOrderByName(string nameOfClient);
        Task<IBaseResponse<bool>> DeleteClientOrder(int id);
        Task<IBaseResponse<ClientOrderDTO>> CreateClientOrder(ClientOrderDTO clientOrderDTO);

        //типы кофе

        Task<IBaseResponse<TypeOfCoffeeDTO>> CreateTypeOfCoffee(TypeOfCoffeeDTO typeOfCoffeeDTO);
        Task<IBaseResponse<bool>> DeleteTypeOfCoffee(int id);
        Task<IBaseResponse<TypeOfCoffeeDTO>> GetTypeOfCoffee(int id);
        Task<IBaseResponse<IEnumerable<TypeOfCoffeeDTO>>> GetTypeOfCoffees();
        Task<IBaseResponse<IEnumerable<ClientOrderDTO>>> GetOrdersByType(int id); //получить заказы по ID типа кофе

    }
}
