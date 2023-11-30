using BestCoffeeService.BLL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BestCoffeeService.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<TypeOfCoffeeDTO> typeOfCoffees, int typeOfCoffee, string nameOfClient)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            typeOfCoffees.Insert(0, new TypeOfCoffeeDTO { NameTypeOfCoffee = "Все", Id = 0 });
            TypeOfCoffees = new SelectList(typeOfCoffees, "Id", "NameTypeOfCoffee", typeOfCoffee);
            SelectedTypeOfCoffee = typeOfCoffee;
            SelectedName = nameOfClient;
        }
        public SelectList TypeOfCoffees { get; } // список видов кофе
        public int SelectedTypeOfCoffee { get; } // выбранный тип кофе
        public string SelectedName { get; } // введенное имя клиента
    }
}
