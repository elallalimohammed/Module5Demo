using Moq;

namespace UsersWebApiTest_Module3
{   
    
    [TestClass]
    public class OrdersControllerTests
    {
        [TestMethod]
        public async Task CreateOrder_ShouldReturnOk_WhenUserExists_AndPaymentSucceeds()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(u => u.GetUserById("user123"))
                .ReturnsAsync(new UserDto { Id = "user123", Name = "Test User" });

            var mockPaymentService = new Mock<IPaymentService>();
            mockPaymentService.Setup(p => p.ProcessPayment(It.IsAny<PaymentRequest>()))
                .ReturnsAsync(new PaymentResponse
                {
                    PaymentId = "pay123",
                    Status = "Success",
                    Amount = 100,
                    Currency = "USD"
                });

            var controller = new OrdersController(mockUserService.Object, mockPaymentService.Object);

            var request = new CreateOrderRequest
            {
                UserId = "user123",
                TotalAmount = 100,
                PaymentMethod = "CreditCard"
            };

            // Act
            var result = await controller.CreateOrder(request) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

      /* [TestMethod]
        public async Task CreateOrder_ShouldReturnBadRequest_WhenUserDoesNotExist()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(u => u.GetUserById("invalidUser"))
                .ReturnsAsync((UserDto?)null);

            var mockPaymentService = new Mock<IPaymentService>();

            var controller = new OrdersController(mockUserService.Object, mockPaymentService.Object);

            var request = new CreateOrderRequest
            {
                UserId = "invalidUser",
                TotalAmount = 100,
                PaymentMethod = "CreditCard"
            };

            // Act
            var result = await controller.CreateOrder(request) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Invalid user.", result.Value);
        }

        [TestMethod]
        public async Task CreateOrder_ShouldReturnBadRequest_WhenPaymentFails()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(u => u.GetUserById("user123"))
                .ReturnsAsync(new UserDto { Id = "user123", Name = "Test User" });

            var mockPaymentService = new Mock<IPaymentService>();
            mockPaymentService.Setup(p => p.ProcessPayment(It.IsAny<PaymentRequest>()))
                .ReturnsAsync(new PaymentResponse
                {
                    PaymentId = "pay999",
                    Status = "Failed",
                    Amount = 100,
                    Currency = "USD"
                });

            var controller = new OrdersController(mockUserService.Object, mockPaymentService.Object);

            var request = new CreateOrderRequest
            {
                UserId = "user123",
                TotalAmount = 100,
                PaymentMethod = "CreditCard"
            };

            // Act
            var result = await controller.CreateOrder(request) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Payment failed.", result.Value);
        } */
    }
    }


   public interface IPaymentClient
{
    PaymentResult ProcessPayment(decimal amount);
}

public class PaymentResult
{
    public bool Success { get; set; }
    public string PaymentId { get; set; }       // optional unique transaction id
    public string Status { get; set; }          // e.g., "Success" / "Failed"
    
}
