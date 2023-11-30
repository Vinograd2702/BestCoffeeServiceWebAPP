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
    public class ClientOrderRepository : IClientOrderRepository
    {
        private readonly BestCoffeeContext _db;

        public ClientOrderRepository(BestCoffeeContext db)
        {
            _db = db;

        }

        public async Task<bool> Create(ClientOrder item)
        {
            try
            {
                await _db.ClientOrders.AddAsync(item);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            ClientOrder clientOrder = _db.ClientOrders.Find(id);
            if (clientOrder != null)
                _db.ClientOrders.Remove(clientOrder);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<ClientOrder> Get(int id)
        {
            return await _db.ClientOrders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<ClientOrder>> GetAll() 
        {
            return await _db.ClientOrders.Include(e=>e.TypeOfCoffee).ToListAsync();
        }

        public async Task<ClientOrder> GetByNameOfClient(string nameOfClient)
        {
            return await _db.ClientOrders.FirstOrDefaultAsync(o => o.NameOfClient == nameOfClient);
        }
    }
}
