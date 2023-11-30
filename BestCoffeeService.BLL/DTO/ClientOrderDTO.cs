using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace BestCoffeeService.BLL.DTO
{
    public class ClientOrderDTO
    {
        public int Id { get; set; }
        public decimal Sum { get; set; } //стоимость заказа клиента

        public string NameOfClient { get; set; } // имя клиента

        public int TypeOfCoffeeID { get; set; } // заказанный тип кофе
        public string strTypeOfCoffee { get; set; } // заказанный тип кофе 

        public DateTime OrderDate { get; set; } // дата заказа

        public string OrderStatus { get; set; } // состояние заказа (выполнен / в процессе)

    }
}
