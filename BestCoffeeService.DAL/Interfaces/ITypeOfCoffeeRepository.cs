using BestCoffeeService.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCoffeeService.DAL.Interfaces
{//специфические методы для репозитория типов кофе
    public interface ITypeOfCoffeeRepository : IBaseRepository<TypeOfCoffee>
    {
        Task<List<ClientOrder>> GetOrdersByType(int id);
    }
}