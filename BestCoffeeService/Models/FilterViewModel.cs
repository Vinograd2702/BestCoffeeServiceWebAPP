using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BestCoffeeService.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<TypeOfCoffee> typeOfCoffees, int typeOfCoffee, string nameCoffeeShop)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            typeOfCoffees.Insert(0, new TypeOfCoffee { NameTypeOfCoffee = "Все", Id = 0 });
            TypeOfCoffees = new SelectList(typeOfCoffees, "Id", "NameTypeOfCoffee", typeOfCoffee);
            SelectedTypeOfCoffee = typeOfCoffee;
            SelectedName = nameCoffeeShop;
        }
        public SelectList TypeOfCoffees { get; } // список видов кофе
        public int SelectedTypeOfCoffee { get; } // выбранный тип кофе
        public string SelectedName { get; } // введенное название магазина
    }
}
