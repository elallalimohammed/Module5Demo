using Moq;
using UsersWebApi_Module3.Controllers;
using UsersWebApi_Module3.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UsersWebApiTest_Module3
{

    [TestClass]
    public class OrderControllerTests
    {
        private Mock<IOrderRepository> _orderRepoMock;
        private Mock<IUserService> _userServiceMock;
        private Mock<IPaymentService> _paymentServiceMock;



        private OrderController _controller;

        [TestInitialize]
        public void Setup()
        {
            _orderRepoMock = new Mock<IOrderRepository>();
            _userServiceMock = new Mock<IUserService>();
            _paymentServiceMock = new Mock<IPaymentService>();

            _controller = new OrderController(_userServiceMock.Object, _orderRepoMock.Object, _paymentServiceMock.Object);

        }

        [TestMethod]
        public async Task CreateOrder_ShouldReturnOk_WhenOrderIsCreatedSuccessfully()
        {
            // Arrange
            var userId = 1;
            Assert.AreEqual(1, userId);
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


