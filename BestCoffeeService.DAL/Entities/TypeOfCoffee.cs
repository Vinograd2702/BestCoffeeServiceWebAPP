using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCoffeeService.DAL.Entities
{
    public class TypeOfCoffee
    {
        public int Id { get; set; }
        public string NameTypeOfCoffee { get; set; } //выбранный тип кофе
        public decimal PriceForCupOfCoffee { get; set; } //стоимость
        public ICollection<ClientOrder> ClientOrder { get; set; } // колекция заказов этой позиции в магазине

    }
}
