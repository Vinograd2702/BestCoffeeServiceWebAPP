using BestCoffeeService.BLL.DTO;
using BestCoffeeService.BLL.Interfaces;
using BestCoffeeService.DAL.Entities;
using BestCoffeeService.DAL.Enum;
using BestCoffeeService.DAL.Interfaces;
using BestCoffeeService.DAL.Repositories;
using BestCoffeeService.DAL.Response;
using System.Collections.Generic;

namespace BestCoffeeService.BLL.Services
{
    public class CoffeeShopOrderService : ICoffeeShopOrderService
    {
        private readonly IClientOrderRepository _clientOrderRepository;
        private readonly ITypeOfCoffeeRepository _typeOfCoffeeRepository;

        public CoffeeShopOrderService(IClientOrderRepository clientOrderRepository, ITypeOfCoffeeRepository typeOfCoffeeRepository)
        {
            _clientOrderRepository = clientOrderRepository;
            _typeOfCoffeeRepository = typeOfCoffeeRepository;
        }

        public async Task<IBaseResponse<ClientOrderDTO>> CreateClientOrder(ClientOrderDTO clientOrderDTO)
        {
            var baseResponse = new BaseResponse<ClientOrderDTO>();

            var typeOfCoffee = new TypeOfCoffee();
            try
            {
                typeOfCoffee =  _typeOfCoffeeRepository.Get(clientOrderDTO.TypeOfCoffeeID).Result;

                if (typeOfCoffee == null)
                {
                    baseResponse.Description = "Заказ не найден";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.StatusCode = StatusCode.OK;
            }
            catch(Exception ex)
            {
                return new BaseResponse<ClientOrderDTO>()
                {
                    Description = $"[CreateClientOrder] : {ex.Message}",
                    StatusCode = StatusCode.IternalServerError
                };
            }

            ClientOrder clientOrder = new ClientOrder()
            {
                Sum = typeOfCoffee.PriceForCupOfCoffee,
                NameOfClient = clientOrderDTO.NameOfClient,
                TypeOfCoffeeID = typeOfCoffee.Id,
                TypeOfCoffee = typeOfCoffee,
                OrderDate = DateTime.Now,
                OrderStatus = "В процессе"
            };


            try
            {
                _clientOrderRepository.Create(clientOrder);

                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClientOrderDTO>()
                {
                    Description = $"[CreateClientOrder] : {ex.Message}",
                    StatusCode = StatusCode.IternalServerError
                };
            }

        }

        public async Task<IBaseResponse<bool>> DeleteClientOrder(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var clientOrder = _clientOrderRepository.Get(id).Result;
                if (clientOrder == null)
                {
                    baseResponse.Description = "Заказ не найден";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                _clientOrderRepository.Delete(id);
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteClientOrder] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<ClientOrderDTO>> GetClientOrder(int id)
        {
            var baseResponse = new BaseResponse<ClientOrderDTO>();
            try
            {
                var clientOrder = await _clientOrderRepository.Get(id);
                if (clientOrder == null)
                {
                    baseResponse.Description = "Заказ не найден";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                ClientOrderDTO clientOrderDTO = new ClientOrderDTO()
                {
                    Id = clientOrder.Id,
                    Sum = clientOrder.Sum,
                    NameOfClient = clientOrder.NameOfClient,
                    TypeOfCoffeeID = clientOrder.TypeOfCoffeeID,
                    strTypeOfCoffee = clientOrder.TypeOfCoffee.NameTypeOfCoffee,
                    OrderDate = clientOrder.OrderDate,
                    OrderStatus = clientOrder.OrderStatus
                };
                baseResponse.Data = clientOrderDTO;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClientOrderDTO>()
                {
                    Description = $"[GetClientOrder] : {ex.Message}"
                };
            }
        }


        public async Task<IBaseResponse<IEnumerable<ClientOrderDTO>>> GetClientOrders()
        {
            var baseResponse = new BaseResponse<IEnumerable<ClientOrderDTO>>();

            try
            {
                var clientOrders = await _clientOrderRepository.GetAll();
                if (clientOrders.Count == 0)
                {
                    baseResponse.Description = "Найднено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                var clientOrderDTOList = new List<ClientOrderDTO>();
                for (int i = 0; i < clientOrders.Count; i++)
                {
                    var clientOrderDTO = new ClientOrderDTO()
                    {
                        Id = clientOrders[i].Id,
                        Sum = clientOrders[i].Sum,
                        NameOfClient = clientOrders[i].NameOfClient,
                        TypeOfCoffeeID = clientOrders[i].TypeOfCoffeeID,
                        strTypeOfCoffee = clientOrders[i].TypeOfCoffee.NameTypeOfCoffee,
                        OrderDate = clientOrders[i].OrderDate,
                        OrderStatus = clientOrders[i].OrderStatus
                    };
                    clientOrderDTOList.Add(clientOrderDTO);
                }


                baseResponse.Data = clientOrderDTOList;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<ClientOrderDTO>>()
                {
                    Description = $"[GetClientOrders] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<ClientOrderDTO>> GetClientOrderByName(string nameOfClient)
        {
            var baseResponse = new BaseResponse<ClientOrderDTO>();
            try
            {
                var clientOrder = await _clientOrderRepository.GetByNameOfClient(nameOfClient);
                if (clientOrder == null)
                {
                    baseResponse.Description = "Заказ по имени клиента найден";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                ClientOrderDTO clientOrderDTO = new ClientOrderDTO()
                {
                    Id = clientOrder.Id,
                    Sum = clientOrder.Sum,
                    NameOfClient = clientOrder.NameOfClient,
                    TypeOfCoffeeID = clientOrder.TypeOfCoffeeID,
                    strTypeOfCoffee = clientOrder.TypeOfCoffee.NameTypeOfCoffee,
                    OrderDate = clientOrder.OrderDate,
                    OrderStatus = clientOrder.OrderStatus
                };

                baseResponse.Data = clientOrderDTO;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClientOrderDTO>()
                {
                    Description = $"[GetClientOrderByName] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<TypeOfCoffeeDTO>> CreateTypeOfCoffee(TypeOfCoffeeDTO typeOfCoffeeDTO)
        {
            var baseResponse = new BaseResponse<TypeOfCoffeeDTO>();

            try
            {
                var typeOfCoffee = new TypeOfCoffee()
                {

                    NameTypeOfCoffee = typeOfCoffeeDTO.NameTypeOfCoffee,
                    PriceForCupOfCoffee = typeOfCoffeeDTO.PriceForCupOfCoffee
                };

                await _typeOfCoffeeRepository.Create(typeOfCoffee);
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<TypeOfCoffeeDTO>()
                {
                    Description = $"[CreateTypeOfCoffee] : {ex.Message}",
                    StatusCode = StatusCode.IternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteTypeOfCoffee(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var typeOfCoffee = await _typeOfCoffeeRepository.Get(id);
                if (typeOfCoffee == null)
                {
                    baseResponse.Description = "Тип кофе не найден";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                _typeOfCoffeeRepository.Delete(id);
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteTypeOfCoffee] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<TypeOfCoffeeDTO>> GetTypeOfCoffee(int id)
        {
            var baseResponse = new BaseResponse<TypeOfCoffeeDTO>();
            try
            {
                var typeOfCoffee = await _typeOfCoffeeRepository.Get(id);
                if (typeOfCoffee == null)
                {
                    baseResponse.Description = "Тип кофе не найден";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                TypeOfCoffeeDTO typeOfCoffeeDTO = new TypeOfCoffeeDTO()
                {
                    Id = typeOfCoffee.Id,
                    NameTypeOfCoffee = typeOfCoffee.NameTypeOfCoffee,
                    PriceForCupOfCoffee = typeOfCoffee.PriceForCupOfCoffee
                };
                baseResponse.Data = typeOfCoffeeDTO;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<TypeOfCoffeeDTO>()
                {
                    Description = $"[GetTypeOfCoffee] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<TypeOfCoffeeDTO>>> GetTypeOfCoffees()
        {
            var baseResponse = new BaseResponse<IEnumerable<TypeOfCoffeeDTO>>();
            try
            {
                var typeOfCoffees = await _typeOfCoffeeRepository.GetAll();
                if (typeOfCoffees.Count == 0)
                {
                    baseResponse.Description = "Найднено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                var typeOfCoffeesDTOList = new List<TypeOfCoffeeDTO>();

                for (int i = 0; i < typeOfCoffees.Count; i++)
                {
                    var typeOfCoffeeDTO = new TypeOfCoffeeDTO()
                    {
                        Id = typeOfCoffees[i].Id,
                        NameTypeOfCoffee = typeOfCoffees[i].NameTypeOfCoffee,
                        PriceForCupOfCoffee = typeOfCoffees[i].PriceForCupOfCoffee
                    };
                    typeOfCoffeesDTOList.Add(typeOfCoffeeDTO);
                }

                baseResponse.Data = typeOfCoffeesDTOList;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<TypeOfCoffeeDTO>>()
                {
                    Description = $"[GetTypeOfCoffees] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<ClientOrderDTO>>> GetOrdersByType(int id)
        {
            var baseResponse = new BaseResponse<IEnumerable<ClientOrderDTO>>();
            try
            {
                var clientOrders = await _typeOfCoffeeRepository.GetOrdersByType(id);
                if (clientOrders == null)
                {
                    baseResponse.Description = "Заказов с таким типом кофе не найдено";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                var clientOrderDTOList = new List<ClientOrderDTO>();
                for (int i = 0; i < clientOrders.Count; i++)
                {
                    ClientOrderDTO clientOrderDTO = new ClientOrderDTO()
                    {
                        Id = clientOrders[i].Id,
                        Sum = clientOrders[i].Sum,
                        NameOfClient = clientOrders[i].NameOfClient,
                        TypeOfCoffeeID = clientOrders[i].TypeOfCoffeeID,
                        strTypeOfCoffee = clientOrders[i].TypeOfCoffee.NameTypeOfCoffee,
                        OrderDate = clientOrders[i].OrderDate,
                        OrderStatus = clientOrders[i].OrderStatus
                    };
                    clientOrderDTOList.Add(clientOrderDTO);
                }


                baseResponse.Data = clientOrderDTOList;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<ClientOrderDTO>>()
                {
                    Description = $"[GetOrdersByType] : {ex.Message}"
                };
            }
        }
    }
}

