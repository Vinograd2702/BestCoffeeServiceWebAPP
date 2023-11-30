using BestCoffeeService.DAL.EFC;
using BestCoffeeService.DAL.Entities;
using BestCoffeeService.DAL.Enum;
using BestCoffeeService.DAL.Interfaces;
using BestCoffeeService.DAL.Repositories;
using BestCoffeeService.DAL.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace MyBackgroundService.Services
{
    public class OrderExecutorBackgroundService : BackgroundService /*IHostedService, IDisposable*//*BackgroundService*/
    {
 
        //private readonly IClientOrderRepository _clientOrderRepository;
        private readonly IServiceProvider _service;
        private readonly TimeSpan _interval = TimeSpan.FromSeconds(20);
        //public OrderExecutorBackgroundService(IClientOrderRepository clientOrderRepository)
        //{
        //    _clientOrderRepository = clientOrderRepository;
        //}

        public OrderExecutorBackgroundService(IServiceProvider service)
        {
            _service = service;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //TimeSpan leadTime =  TimeSpan.FromSeconds(15);


            while (!stoppingToken.IsCancellationRequested)
            {


                using ( var scope = _service.CreateScope())
                {
                    var clientOrderRepository = scope.ServiceProvider.GetRequiredService<IClientOrderRepository>();

                    try
                    {
                        TypeOfCoffee typeOfCoffee = clientOrderRepository.GetAll().Result.First().TypeOfCoffee;




                        ClientOrder backgroundServiceClientOrder = new ClientOrder()
                        {
                            NameOfClient = "Бекграунд сервис",
                            OrderDate = DateTime.Now,
                            TypeOfCoffee = typeOfCoffee,
                            TypeOfCoffeeID = typeOfCoffee.Id,
                            Sum = typeOfCoffee.PriceForCupOfCoffee,
                            OrderStatus = "В процессе"

                        };
                        clientOrderRepository.Create(backgroundServiceClientOrder);



                    }
                    catch(Exception ex)
                    {

                    }


                    //var clientOrders = clientOrderRepository.GetAll().Result;

                    //if (clientOrders.Count != 0)
                    //{
                    //    for (int i = 0; i < clientOrders.Count; i++)
                    //    {
                    //        TimeSpan interval = DateTime.Now.Subtract(clientOrders[i].OrderDate);
                    //        if ((clientOrders[i].OrderStatus!= "Выполнен") && (interval >= leadTime))
                    //        {
                    //            clientOrders[i].OrderStatus = "Выполнен";


                    //            clientOrderRepository.Update(clientOrders[i]);
                    //            //clientOrderRepository.Delete(clientOrders[i].Id);
                    //            //clientOrderRepository.Create(compliteOrder);
                    //        }

                    //    }


                    //}
                }

                    //var clientOrders = await _clientOrderRepository.GetAll();


                //_db.ClientOrders.Include(e => e.TypeOfCoffee).ToListAsync().Result;

                await Task.Delay(_interval, stoppingToken);

            }
        }
    }
}
