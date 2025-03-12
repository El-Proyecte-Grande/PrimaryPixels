using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using PrimaryPixels.Controllers.DerivedControllers;
using PrimaryPixels.Models.Order;
using PrimaryPixels.Services.Repositories;
using PrimaryPixels.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using PrimaryPixels.Exceptions;

namespace PrimaryPixelsTest.ControllerTests
{
    [TestFixture]
    public class OrderControllerTest
    {
        private Mock<ILogger<OrderController>> _loggerMock;
        private Mock<IRepository<Order>> _repositoryMock;
        private Mock<IOrderService> _orderServiceMock;
        private OrderController _orderController;
        private Order _order;
        private OrderDTO _orderDTO;
        private string userId = "123dfgb";

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<OrderController>>();
            _repositoryMock = new Mock<IRepository<Order>>();
            _orderServiceMock = new Mock<IOrderService>();
            _orderController = new OrderController(_loggerMock.Object, _repositoryMock.Object, _orderServiceMock.Object);
            _order = new Order()
            {
                Id = 1,
                UserId = "01ewdff345",
                OrderDate = new DateOnly(2023, 01, 12),
                FirstName = "Joe",
                LastName = "Smith",
                City = "London",
                Address = "Random street 230",
                Price = 20000
            };
            _orderDTO = new OrderDTO()
            {
                FirstName = "Joe",
                LastName = "Smith",
                City = "London",
                Address = "Random street 230",
                OrderProducts = It.IsAny<List<OrderDetailsDTO>>()
            };
        }

        [Test]
        public async Task AddMethodThrowsExceptionIfUserIdIsNotFound()
        {
            _orderController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal() }
            };

            var result = await _orderController.Add(_orderDTO);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.That(badRequestResult.Value, Is.EqualTo("UserId not found."));
        }

        [Test]
        public async Task AddMethodFailsIfOrderServiceThrowsException()
        {
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }));
            _orderController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            _orderServiceMock.Setup(x => x.CreateOrder(_orderDTO, userId)).ThrowsAsync(new Exception());

            var result = await _orderController.Add(_orderDTO);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task AddMethodReturnsIdOfCreatedOrderIfEverythingIsOk()
        {
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }));
            _orderController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            _orderServiceMock.Setup(x => x.CreateOrder(_orderDTO, userId)).ReturnsAsync(_order.Id);

            var result = await _orderController.Add(_orderDTO);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(_order.Id));
        }

        [Test]
        public async Task DeleteMethodFailsIfRepositoryThrowsException()
        {
            _repositoryMock.Setup(x => x.DeleteById(_order.Id)).ThrowsAsync(new OrderNotFoundException("Couldn't find order with this id!"));

            var result = await _orderController.Delete(_order.Id);

            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task DeleteMethodReturnsIdOfDeletedEntityIfEverythingIsOk()
        {
            _repositoryMock.Setup(x => x.DeleteById(_order.Id)).ReturnsAsync(_order.Id);

            var result = await _orderController.Delete(_order.Id);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(_order.Id));
        }

        [Test]
        public async Task GetAllMethodFailsIfRepositoryThrowsException()
        {
            _repositoryMock.Setup(x => x.GetAll()).ThrowsAsync(new Exception());

            var result = await _orderController.GetAll();

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task GetAllMethodReturnsAllOrderEntitiesIfEverythingIsOk()
        {
            var orders = new Order[] { _order, _order };
            _repositoryMock.Setup(x => x.GetAll()).ReturnsAsync(orders);

            var result = await _orderController.GetAll();

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(orders));
        }

        [Test]
        public async Task GetByIdMethodFailsIfRepositoryThrowsException()
        {
            _repositoryMock.Setup(x => x.GetById(_order.Id)).ThrowsAsync(new Exception());

            var result = await _orderController.GetById(_order.Id);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task GetByIdMethodReturnsRetrievedEntityIfEverythingIsOk()
        {
            _repositoryMock.Setup(x => x.GetById(_order.Id)).ReturnsAsync(_order);

            var result = await _orderController.GetById(_order.Id);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(_order));
        }

        [Test]
        public async Task UpdateMethodFailsIfRepositoryThrowsException()
        {
            _repositoryMock.Setup(x => x.Update(_order)).ThrowsAsync(new Exception());

            var result = await _orderController.Update(_order);

            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task UpdateMethodReturnsIdOfUpdatedEntityIfEverythingIsOk()
        {
            _repositoryMock.Setup(x => x.Update(_order)).ReturnsAsync(_order.Id);

            var result = await _orderController.Update(_order);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(_order.Id));
        }

    }
}
