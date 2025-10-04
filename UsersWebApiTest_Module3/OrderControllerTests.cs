using Moq;
using UsersWebApi_Module3.Controllers;
using UsersWebApi_Module3.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UsersWebApi_Module3.Models;

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
        public async Task Create_NullOrder_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.Create(null);
            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
        

        
                [TestMethod]
                public async Task Create_MissingUserId_ReturnsBadRequest()
                {
                    // Arrange
                    var order = new Order { UserId = "" };

                    // Act
                    var result = await _controller.Create(order) as BadRequestResult;

                    // Assert
                    Assert.IsInstanceOfType(result, typeof(BadRequestResult));
                }
                

                [TestMethod]
                public async Task Create_UserDoesNotExist_ReturnsNotFound()
                {
                    // Arrange
                    var order = new Order { UserId = "123" };
                    _userServiceMock.Setup(s => s.GetUserById("123")).ReturnsAsync((User)null);

                    // Act
                    var result = await _controller.Create(order);

                    // Assert
                    var notFoundResult = result as NotFoundObjectResult;
                    Assert.IsNotNull(notFoundResult);
                   Assert.AreEqual("User not found", notFoundResult.Value);
                }
/*
                [TestMethod]
                public async Task Create_RepositoryFails_ReturnsBadRequest()
                {
                    // Arrange
                    var order = new Order { UserId = "123" };
                    _userServiceMock.Setup(s => s.GetUserById("123")).ReturnsAsync(new User { Id = "123" });
                    _paymentServiceMock.Setup(p => p.ProcessPayment(order)).ReturnsAsync(new PaymentResult { Success = true });
                    _orderRepoMock.Setup(r => r.CreateAsync(order)).ReturnsAsync(false);

                    // Act
                    var result = await _controller.Create(order);

                    // Assert
                    var badRequest = result as BadRequestObjectResult;
                    Assert.IsNotNull(badRequest);
                    Assert.AreEqual("Could not create order", badRequest.Value);
                }

                [TestMethod]
                public async Task Create_HappyPath_ReturnsCreatedAtAction()
                {
                    // Arrange
                    var order = new Order { Id = "order1", UserId = "123", Products = new List<string> { "prod1" } };
                    _userServiceMock.Setup(s => s.GetUserById("123")).ReturnsAsync(new User { Id = "123" });
                    _paymentServiceMock.Setup(p => p.ProcessPayment(order)).ReturnsAsync(new PaymentResult { Success = true });
                    _orderRepoMock.Setup(r => r.CreateAsync(order)).ReturnsAsync(true);

                    // Act
                    var result = await _controller.Create(order);

                    // Assert
                    var createdResult = result as CreatedAtActionResult;
                    Assert.IsNotNull(createdResult);
                    Assert.AreEqual("Create", createdResult.ActionName);
                    Assert.AreEqual(order, createdResult.Value);
                    Assert.AreEqual(order.Id, ((dynamic)createdResult.RouteValues)["id"]);
                }

                [TestMethod]
                public async Task Create_RepositoryThrowsException_ReturnsInternalServerError()
                {
                    // Arrange
                    var order = new Order { UserId = "123" };
                    _userServiceMock.Setup(s => s.GetUserById("123")).ReturnsAsync(new User { Id = "123" });
                    _paymentServiceMock.Setup(p => p.ProcessPayment(order)).ReturnsAsync(new PaymentResult { Success = true });
                    _orderRepoMock.Setup(r => r.CreateAsync(order)).ThrowsAsync(new Exception("DB error"));

                    // Act
                    var result = await _controller.Create(order);

                    // Assert
                    var objectResult = result as ObjectResult;
                    Assert.IsNotNull(objectResult);
                    Assert.AreEqual(500, objectResult.StatusCode);
                    StringAssert.Contains(objectResult.Value.ToString(), "Internal server error");
                }

                [TestMethod]
                public async Task Create_PaymentFails_ReturnsBadRequest()
                {
                    // Arrange
                    var order = new Order { UserId = "123" };
                    _userServiceMock.Setup(s => s.GetUserById("123")).ReturnsAsync(new User { Id = "123" });
                    _paymentServiceMock.Setup(p => p.ProcessPayment(order))
                        .ReturnsAsync(new PaymentResult { Success = false, Message = "Insufficient funds" });

                    // Act
                    var result = await _controller.Create(order);

                    // Assert
                    var badRequest = result as BadRequestObjectResult;
                    Assert.IsNotNull(badRequest);
                    Assert.AreEqual("Payment failed: Insufficient funds", badRequest.Value);
                }

                [TestMethod]
                public async Task Create_PaymentServiceThrows_ReturnsInternalServerError()
                {
                    // Arrange
                    var order = new Order { UserId = "123" };
                    _userServiceMock.Setup(s => s.GetUserById("123")).ReturnsAsync(new User { Id = "123" });
                    _paymentServiceMock.Setup(p => p.ProcessPayment(order)).ThrowsAsync(new Exception("Payment API timeout"));

                    // Act
                    var result = await _controller.Create(order);

                    // Assert
                    var objectResult = result as ObjectResult;
                    Assert.IsNotNull(objectResult);
                    Assert.AreEqual(500, objectResult.StatusCode);
                    StringAssert.Contains(objectResult.Value.ToString(), "Payment service error");
                }

                [TestMethod]
                public async Task Create_HappyPathWithPayment_ReturnsCreatedAtAction()
                {
                    // Arrange
                    var order = new Order { Id = "order2", UserId = "123", Products = new List<string> { "prod1", "prod2" } };
                    _userServiceMock.Setup(s => s.GetUserById("123")).ReturnsAsync(new User { Id = "123" });
                    _paymentServiceMock.Setup(p => p.ProcessPayment(order)).ReturnsAsync(new PaymentResult { Success = true });
                    _orderRepoMock.Setup(r => r.CreateAsync(order)).ReturnsAsync(true);

                    // Act
                    var result = await _controller.Create(order);

                    // Assert
                    var createdResult = result as CreatedAtActionResult;
                    Assert.IsNotNull(createdResult);
                    Assert.AreEqual("Create", createdResult.ActionName);
                    Assert.AreEqual(order, createdResult.Value);
                    Assert.AreEqual(order.Id, ((dynamic)createdResult.RouteValues)["id"]);
                }
                */

    }

    
}


