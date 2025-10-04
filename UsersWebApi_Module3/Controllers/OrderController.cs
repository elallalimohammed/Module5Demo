
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
            if (order == null || string.IsNullOrEmpty(order.UserId))
            {
                return BadRequest();
            }

            var user = await _userService.GetUserById(order.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Additional logic, e.g., save order
            return Ok();
        }

        // Other methods...

        



    }
}