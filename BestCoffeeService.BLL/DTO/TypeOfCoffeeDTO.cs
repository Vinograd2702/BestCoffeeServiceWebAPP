using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCoffeeService.BLL.DTO
{
    public class TypeOfCoffeeDTO
    {
        public int Id { get; set; }
        public string? NameTypeOfCoffee { get; set; } //выбранный тип кофе 
        public string SizedCupOfCoffee { get; set; } //объём кружки
        public decimal PriceForCupOfCoffee { get; set; } //стоимость
    }
}
