using BestCoffeeService.DAL.EFC;
using BestCoffeeService.DAL.Entities;
using BestCoffeeService.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCoffeeService.DAL.Repositories
{
    public class TypeOfCoffeeRepository : ITypeOfCoffeeRepository
    {
        private readonly BestCoffeeContext _db;

        public TypeOfCoffeeRepository(BestCoffeeContext db)
        {
            _db = db;

            TypeOfCoffee Espresso = new TypeOfCoffee { NameTypeOfCoffee = "Эспрессо", PriceForCupOfCoffee = 85 };
            TypeOfCoffee Сappuccino = new TypeOfCoffee { NameTypeOfCoffee = "Капучино", PriceForCupOfCoffee = 100 };
            TypeOfCoffee Latte = new TypeOfCoffee { NameTypeOfCoffee = "Латте", PriceForCupOfCoffee = 110 };
            TypeOfCoffee Americano = new TypeOfCoffee { NameTypeOfCoffee = "Aмерикано", PriceForCupOfCoffee = 95 };
            
            ClientOrder firstClientOrder = new ClientOrder()
            {
                NameOfClient = "Иван",
                OrderDate = DateTime.Now,
                TypeOfCoffee = Сappuccino,
                TypeOfCoffeeID = Сappuccino.Id,
                OrderStatus = "В процессе"

            };

            if (!db.TypeOfCoffees.Any())
            {
                db.TypeOfCoffees.Add(Espresso);
                db.TypeOfCoffees.Add(Сappuccino);
                db.TypeOfCoffees.Add(Latte);
                db.TypeOfCoffees.Add(Americano);

                db.SaveChanges();
            }

            if(!db.ClientOrders.Any())
            {
                db.ClientOrders.Add(firstClientOrder);

                db.SaveChanges();
            }
        }

        public async Task<bool> Create(TypeOfCoffee item)
        {
            await _db.TypeOfCoffees.AddAsync(item);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            TypeOfCoffee typeOfCoffee = _db.TypeOfCoffees.Find(id);
            if (typeOfCoffee != null)
                _db.TypeOfCoffees.Remove(typeOfCoffee);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<TypeOfCoffee> Get(int id)
        {
            return await _db.TypeOfCoffees.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<TypeOfCoffee>> GetAll()
        {
            return await _db.TypeOfCoffees.ToListAsync();
        }

        async Task<List<ClientOrder>> ITypeOfCoffeeRepository.GetOrdersByType(int id) //проверь метод
        {
            return await _db.ClientOrders.Where(o => o.TypeOfCoffeeID == id).ToListAsync();
        }
    }
}
