using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BestCoffeeService.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace BestCoffeeService.Controllers
{
    public class HomeController : Controller
    {
        CoffeeShopMenuItemContext db;//определили переменную контекста данных для работы с базой данных
        public HomeController(CoffeeShopMenuItemContext context)//определяем контекст в конструкторе контроллера
        {
            db = context;
            if (!db.TypeOfCoffees.Any())
            {
                TypeOfCoffee espresso = new TypeOfCoffee { NameTypeOfCoffee = "Эспрессо" };
                TypeOfCoffee cappuccino = new TypeOfCoffee { NameTypeOfCoffee = "Капучино" };
                TypeOfCoffee latte = new TypeOfCoffee { NameTypeOfCoffee = "Латте" };
                TypeOfCoffee americano = new TypeOfCoffee { NameTypeOfCoffee = "Aмерикано" };

                CoffeeShopMenuItem coffeeShopMenuItem1 = new CoffeeShopMenuItem
                {
                    CoffeePrice = 100,
                    CoffeeRating = 3,
                    NameCoffeeShop = "Магнит",
                    TypeOfCoffee = espresso
                };
                CoffeeShopMenuItem coffeeShopMenuItem2 = new CoffeeShopMenuItem
                {
                    CoffeePrice = 200,
                    CoffeeRating = 5,
                    NameCoffeeShop = "Кофикс",
                    TypeOfCoffee = cappuccino
                };
                CoffeeShopMenuItem coffeeShopMenuItem3 = new CoffeeShopMenuItem
                {
                    CoffeePrice = 250,
                    CoffeeRating = 4,
                    NameCoffeeShop = "Кофикс",
                    TypeOfCoffee = latte
                };
                CoffeeShopMenuItem coffeeShopMenuItem4 = new CoffeeShopMenuItem
                {
                    CoffeePrice = 150,
                    CoffeeRating = 4,
                    NameCoffeeShop = "Макдональдс",
                    TypeOfCoffee = latte
                };
                CoffeeShopMenuItem coffeeShopMenuItem5 = new CoffeeShopMenuItem
                {
                    CoffeePrice = 175,
                    CoffeeRating = 2,
                    NameCoffeeShop = "Твой кофе",
                    TypeOfCoffee = americano
                };


                db.TypeOfCoffees.AddRange(espresso, cappuccino, latte, americano);
                db.CoffeeShopMenuItems.AddRange(coffeeShopMenuItem1, coffeeShopMenuItem2,
                    coffeeShopMenuItem3, coffeeShopMenuItem4, coffeeShopMenuItem5);

                db.SaveChanges();
            }
        }

        public async Task<IActionResult> Index(string nameCoffeeShop, int typeOfCoffee = 0, int page = 1,
                   SortState sortOrder = SortState.NameCoffeeShopAsc)
        {
            int pageSize = 3;

            //фильтрация
            IQueryable<CoffeeShopMenuItem> coffeeShopMenuItems = db.CoffeeShopMenuItems.Include(x => x.TypeOfCoffee);

            if (typeOfCoffee != 0)
            {
                coffeeShopMenuItems = coffeeShopMenuItems.Where(p => p.TypeOfCoffeeID == typeOfCoffee);
            }
            if (!string.IsNullOrEmpty(nameCoffeeShop))
            {
                coffeeShopMenuItems = coffeeShopMenuItems.Where(p => p.NameCoffeeShop!.Contains(nameCoffeeShop));
            }

            // сортировка
            switch (sortOrder)
            {
                case SortState.NameCoffeeShopDesc:
                    coffeeShopMenuItems = coffeeShopMenuItems.OrderByDescending(s => s.NameCoffeeShop);
                    break;
                case SortState.CoffeePriceAsc:
                    coffeeShopMenuItems = coffeeShopMenuItems.OrderBy(s => s.CoffeePrice);
                    break;
                case SortState.CoffeePriceDesc:
                    coffeeShopMenuItems = coffeeShopMenuItems.OrderByDescending(s => s.CoffeePrice);
                    break;
                case SortState.CoffeeRatingAsc:
                    coffeeShopMenuItems = coffeeShopMenuItems.OrderBy(s => s.CoffeeRating);
                    break;
                case SortState.CoffeeRatingDesc:
                    coffeeShopMenuItems = coffeeShopMenuItems.OrderByDescending(s => s.CoffeeRating);
                    break;
                case SortState.TypeOfCoffeeAsc:
                    coffeeShopMenuItems = coffeeShopMenuItems.OrderBy(s => s.TypeOfCoffee!.NameTypeOfCoffee);
                    break;
                case SortState.TypeOfCoffeeDesc:
                    coffeeShopMenuItems = coffeeShopMenuItems.OrderByDescending(s => s.TypeOfCoffee!.NameTypeOfCoffee);
                    break;
                default:
                    coffeeShopMenuItems = coffeeShopMenuItems.OrderBy(s => s.NameCoffeeShop);
                    break;
            }


            // пагинация
            var count = await coffeeShopMenuItems.CountAsync();
            var items = await coffeeShopMenuItems.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            IndexViewModel viewModel = new IndexViewModel(
                items,
                new PageViewModel(count, page, pageSize),
                new FilterViewModel(db.TypeOfCoffees.ToList(), typeOfCoffee, nameCoffeeShop),
                new SortViewModel(sortOrder)
            );
            return View(viewModel);
        }

        public IActionResult Create()//интерфейс метода 
        {
            ViewBag.TypeOfCoffees = new SelectList(db.TypeOfCoffees, "Id", "NameTypeOfCoffee");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CoffeeShopMenuItem coffeeShopMenuItem)//метод insert элемента в базу данных (ввода)
        {
            coffeeShopMenuItem.TypeOfCoffee = db.TypeOfCoffees.FirstOrDefault(e => e.Id == coffeeShopMenuItem.TypeOfCoffeeID);
            db.CoffeeShopMenuItems.Add(coffeeShopMenuItem);//
            await db.SaveChangesAsync();
            return RedirectToAction("Index");//возврат на страничку Index
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                CoffeeShopMenuItem coffeeShopMenuItem = new CoffeeShopMenuItem { Id = id.Value };
                db.Entry(coffeeShopMenuItem).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

    }
}
