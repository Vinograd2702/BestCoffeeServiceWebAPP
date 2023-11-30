using BestCoffeeService.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCoffeeService.DAL.Interfaces
{
    public interface IClientOrderRepository : IBaseRepository<ClientOrder> //интерфейс специафических методов для заказов
    {
        Task<ClientOrder> GetByNameOfClient(string nameOfClient);

        Task<bool> Complite (int id);
    }
}
