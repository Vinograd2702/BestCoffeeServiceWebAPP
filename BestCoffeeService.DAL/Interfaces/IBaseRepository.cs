using BestCoffeeService.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCoffeeService.DAL.Interfaces
{
    public interface IBaseRepository<T> //общий интерфейс для всех типов репозиториев
    {
        Task<bool> Create(T item); //добавить объект в бд

        Task<T> Get(int id); //вернуть сущность по ID

        Task<List<T>> GetAll(); // вернуть все сущности заданного типа

        Task<bool> Delete(int id);// пока добавил удаление по ID

        Task<T> Update(T item);
    }
}
