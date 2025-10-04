
using Microsoft.AspNetCore.Mvc;
using UsersWebApi_Module3.Models;
using UsersWebApi_Module3.Repositories;     
namespace UsersWebApi_Module3.Controllers
{
    public class OrderController: ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentService _paymentService;


        public OrderController(IUserService userService, IOrderRepository orderRepository,
                                       IPaymentService paymentService)
        {
            _userService = userService;
            _orderRepository = orderRepository;
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest() as IActionResult;
            }

            // You can add more logic here as needed

            return Ok();
        }

        // Other methods...

        



    }
}