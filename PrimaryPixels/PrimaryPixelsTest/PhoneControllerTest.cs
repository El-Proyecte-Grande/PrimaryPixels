using Moq;
using Microsoft.Extensions.Logging;
using PrimaryPixels.Controllers.DerivedControllers.ProductControllers;
using PrimaryPixels.Services.Repositories;
using PrimaryPixels.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace PrimaryPixelsTest
{
    [TestFixture]
    public class PhoneControllerTest
    {
        private Mock<ILogger<PhoneController>> _loggerMock;
        private Mock<IRepository<Phone>> _repositoryMock;
        private PhoneController _phoneController;
        private Phone _phone;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<PhoneController>>();
            _repositoryMock = new Mock<IRepository<Phone>>();
            _phoneController = new PhoneController(_loggerMock.Object, _repositoryMock.Object);
            _phone = new Phone()
            {
                Availability = true,
                CardIndependency = true,
                Cpu = "Exynos 1234",
                Id = 10,
                Name = "Samsung Galaxy A21",
                InternalMemory = 128,
                Price = 200000,
                Ram = 8,
                TotalSold = 3,
                Image = ""
            };

        }

        [Test]
        public async Task AddMethodFailsIfRepositoryThrowsException()
        {
            _repositoryMock.Setup(x => x.Add(It.IsAny<Phone>())).ThrowsAsync(new Exception());

            var result = await _phoneController.Add(It.IsAny<Phone>());

            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task AddMethodReturnsTheIdOfTheAddedEntityIfEverythingIsOk()
        {
            _repositoryMock.Setup(x => x.Add(_phone)).ReturnsAsync(_phone.Id);

            var result = await _phoneController.Add(_phone);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okObject = result as OkObjectResult;
            Assert.That(okObject?.Value, Is.EqualTo(_phone.Id));
        }

        [Test]
        public async Task DeleteMethodFailsIfRepositoryThrowsException()
        {
            _repositoryMock.Setup(x => x.DeleteById(_phone.Id)).ThrowsAsync(new Exception());

            var result = await _phoneController.Delete(_phone.Id);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task DeleteMethodReturnsTheIdOfTheDeletedEntityIfEverythingIsOk()
        {
            _repositoryMock.Setup(x => x.DeleteById(_phone.Id)).ReturnsAsync(_phone.Id);

            var result = await _phoneController.Delete(_phone.Id);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okObject = result as OkObjectResult;
            Assert.That(okObject?.Value, Is.EqualTo(_phone.Id));
        }

        [Test]
        public async Task GetAllMethodFailsIfRepositoryThrowsException()
        {
            _repositoryMock.Setup(x => x.GetAll()).ThrowsAsync(new Exception());

            var result = await _phoneController.GetAll();

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task GetAllMethodReturnsAllPhoneEntitiesIfEverythingIsOk()
        {
            var phones = new Phone[] { _phone, _phone };
            _repositoryMock.Setup(x => x.GetAll()).ReturnsAsync(phones);

            var result = await _phoneController.GetAll();

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okObject = result as OkObjectResult;
            Assert.That(okObject?.Value, Is.EqualTo(phones));
        }

    }
}