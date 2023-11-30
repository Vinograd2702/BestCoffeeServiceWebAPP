using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCoffeeService.DAL.Entities
{
    public class ClientOrder
    {
        public int Id { get; set; }
        public decimal Sum { get; set; } //стоимость заказа клиента

        public string NameOfClient { get; set; } // имя клиента

        public int TypeOfCoffeeID { get; set; } // заказанный тип кофе
        public TypeOfCoffee TypeOfCoffee { get; set; } // заказанный тип кофе

        public DateTime OrderDate { get; set; } // дата заказа

        public string OrderStatus { get; set; } // состояние заказа (выполнен / в процессе)

    }
}
