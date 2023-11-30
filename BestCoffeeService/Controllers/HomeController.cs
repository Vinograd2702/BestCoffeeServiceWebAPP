using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BestCoffeeService.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using BestCoffeeService.BLL.Interfaces;
using BestCoffeeService.BLL.DTO;
using BestCoffeeService.DAL.Entities;
using BestCoffeeService.BLL.Services;

namespace BestCoffeeService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICoffeeShopOrderService _coffeeShopOrderService;

        public HomeController(ICoffeeShopOrderService coffeeShopOrderService)
        {
            _coffeeShopOrderService = coffeeShopOrderService;
        }

        //

        public async Task<IActionResult> Index(string nameOfClient, int typeOfCoffee = 0, int page = 1,
                   SortState sortOrder = SortState.NameOfClientAsc)
        {
            int pageSize = 3;

            //фильтрация
            var response = await _coffeeShopOrderService.GetClientOrders();
            var clientOrders = response.Data;


            if (typeOfCoffee != 0)
            {
                clientOrders = clientOrders.Where(p => p.TypeOfCoffeeID == typeOfCoffee);
            }
            if (!string.IsNullOrEmpty(nameOfClient))
            {
                clientOrders = clientOrders.Where(p => p.NameOfClient!.Contains(nameOfClient));
            }

            // сортировка
            switch (sortOrder)
            {
                case SortState.NameOfClientDesc:
                    clientOrders = clientOrders.OrderByDescending(s => s.NameOfClient);
                    break;
                case SortState.OrderDateAsc:
                    clientOrders = clientOrders.OrderBy(s => s.OrderDate);
                    break;
                case SortState.OrderDateDesc:
                    clientOrders = clientOrders.OrderByDescending(s => s.OrderDate);
                    break;
                default:
                    clientOrders = clientOrders.OrderBy(s => s.NameOfClient);
                    break;
            }


            // пагинация
            var count = clientOrders.Count();
            var items = clientOrders.Skip((page - 1) * pageSize).Take(pageSize).ToList();


            var pages = new PageViewModel(count, page, pageSize);
            var typeOfCoffees = (await _coffeeShopOrderService.GetTypeOfCoffees()).Data.ToList();
            var filterModel = new FilterViewModel(typeOfCoffees, typeOfCoffee, nameOfClient);
            // формируем модель представления
            IndexViewModel viewModel = new IndexViewModel(
                items,
                pages,
                filterModel,
                new SortViewModel(sortOrder)
            );
            return View(viewModel);
        }

        public async Task<IActionResult> Create()//интерфейс метода 
        {
            ViewBag.TypeOfCoffees = new SelectList((await _coffeeShopOrderService.GetTypeOfCoffees()).Data.ToList(), "Id", "NameTypeOfCoffee");
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ClientOrderDTO clientOrderDTO)//метод insert элемента в базу данных (ввода)
        {
            _coffeeShopOrderService.CreateClientOrder(clientOrderDTO);
            return RedirectToAction("Index");//возврат на страничку Index
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id != null)
            {
                _coffeeShopOrderService.DeleteClientOrder(id);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

    }
}
