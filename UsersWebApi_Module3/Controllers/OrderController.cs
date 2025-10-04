
using Microsoft.AspNetCore.Mvc;
using UsersWebApi_Module3.Models;
using UsersWebApi_Module3.Repositories;     
namespace UsersWebApi_Module3.Controllers
{
    public class OrderController
    {
        private readonly IUserService _userService;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentService _paymentService;



        public OrderController(IUserService userService, IOrderRepository orderRepository, IPaymentService paymentService)
        {
            _userService = userService;
            _orderRepository = orderRepository;
            _paymentService = paymentService;
        }



        // Other methods...

        



    }
}