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
using PrimaryPixels.Exceptions;
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

        [Test]
        public async Task ChangeUserPassword_ReturnsBadRequest_WhenUserIdNotFound()
        {
            SetUserContext("");

            var result = await _controller.ChangeUserPassword(new ChangePasswordRequest() { CurrentPassword = "oldPass", NewPassword = "newPass"});

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.That(badRequestResult.Value, Is.EqualTo("UserId not found."));
        }

        [Test]
        public async Task ChangeUserPassword_ReturnsBadRequest_WhenChangeFails()
        {
            var userId = "123";
            SetUserContext(userId);
            _repositoryMock.Setup(repo => repo.ChangePasswordAsync("oldPass", "newPass", userId)).ReturnsAsync(false);

            var result = await _controller.ChangeUserPassword(new ChangePasswordRequest { CurrentPassword = "oldPass", NewPassword = "newPass" });

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.That(badRequestResult.Value, Is.EqualTo("Failed to change password."));
        }

        [Test]
        public async Task ChangeUserPassword_ReturnsOk_WhenPasswordChangedSuccessfully()
        {
            var userId = "123";
            SetUserContext(userId);
            _repositoryMock.Setup(repo => repo.ChangePasswordAsync("oldPass", "newPass", userId)).ReturnsAsync(true);

            var result = await _controller.ChangeUserPassword(new ChangePasswordRequest { CurrentPassword = "oldPass", NewPassword = "newPass" });

            Assert.That(result, Is.InstanceOf<OkObjectResult>()); 
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.EqualTo("Password changed successfully."));
        }

        [Test]
        public async Task ForgotPassword_ReturnsNotFound_WhenEmailNotExists()
        {
            _repositoryMock.Setup(repo => repo.GetPasswordResetToken("unknown@example.com")).ThrowsAsync(new EmailNotFoundException(""));

            var result = await _controller.ForgotPassword(new ForgotPasswordRequest { Email = "unknown@example.com" });

            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task ForgotPassword_ReturnsOk_WhenEmailIsValid()
        {
            _repositoryMock.Setup(repo => repo.GetPasswordResetToken("test@example.com")).ReturnsAsync("resetToken");
            _configurationMock.Setup(config => config["FrontendUrl"]).Returns("https://frontend.com");

            var result = await _controller.ForgotPassword(new ForgotPasswordRequest { Email = "test@example.com" });

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task ResetPassword_ReturnsBadRequest_WhenFailed()
        {
            _repositoryMock.Setup(repo => repo.ResetPassword("test@example.com", "token123", "newPass"))
                .ReturnsAsync(false);

            var result = await _controller.ResetPassword(new ResetPasswordRequest { Email = "test@example.com", Token = "token123", NewPassword = "newPass" });

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task ResetPassword_ReturnsOk_WhenSuccessful()
        {
            _repositoryMock.Setup(repo => repo.ResetPassword("test@example.com", "token123", "newPass"))
                .ReturnsAsync(true);

            var result = await _controller.ResetPassword(new ResetPasswordRequest { Email = "test@example.com", Token = "token123", NewPassword = "newPass" });

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }



    }
}
