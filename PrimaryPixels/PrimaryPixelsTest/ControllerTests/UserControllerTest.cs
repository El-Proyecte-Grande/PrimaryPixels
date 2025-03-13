using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using PrimaryPixels.Controllers;
using PrimaryPixels.DTO;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixelsTest.ControllerTests
{
    [TestFixture]
    public class UserControllerTest
    {
        private Mock<IUserRepository> _repositoryMock;
        private Mock<IEmailSender> _emailSenderMock;
        private Mock<IConfiguration> _configurationMock;
        private UserController _controller;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IUserRepository>();
            _emailSenderMock = new Mock<IEmailSender>();
            _configurationMock = new Mock<IConfiguration>();
            _controller = new UserController(_repositoryMock.Object, _emailSenderMock.Object, _configurationMock.Object);
        }

        private void SetUserContext(string userId)
        {
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId) };
            var identity = new ClaimsIdentity(claims);
            var user = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } };
        }

        [Test]
        public async Task GetUserInfos_ReturnsBadRequest_WhenUserIdNotFound()
        {
            SetUserContext("");

            var result = await _controller.GetUserInfos();

            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = (BadRequestObjectResult)result.Result;
            Assert.That(badRequestResult.Value, Is.EqualTo("UserId not found."));
        }

        [Test]
        public async Task GetUserInfos_ReturnsOk_WithUserInfo()
        {
            var userId = "123";
            var userInfo = new UserResponse ("Test", "test@example.com");
            _repositoryMock.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(userInfo);
            SetUserContext(userId);

            var result = await _controller.GetUserInfos();

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = (OkObjectResult)result.Result;
            Assert.That(okResult.Value, Is.EqualTo(userInfo));
        }

    }
}
