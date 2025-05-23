﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimaryPixels.Services.Authentication;
using Moq;
using PrimaryPixels.Controllers;
using Microsoft.AspNetCore.Identity.Data;
using PrimaryPixels.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PrimaryPixelsTest.ControllerTests
{
    [TestFixture]
    public class AuthControllerTest
    {
        private Mock<IAuthService> _authServiceMock;
        private AuthController _authController;

        [SetUp]
        public void Setup()
        {
            _authServiceMock = new Mock<IAuthService>();
            _authController = new AuthController(_authServiceMock.Object);
        }

        [Test]
        public async Task RegisterMethodFailsIfRequestIsNotValid()
        {
            var invalidRegistrationRequest = new RegistrationRequest("", "Test", "password");
            _authController.ModelState.AddModelError("Email", "Email field is missing!");

            var result = await _authController.Register(invalidRegistrationRequest);

            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
                var badRequestResult = result.Result as BadRequestObjectResult;
                Assert.That(badRequestResult, Is.Not.Null);
                Assert.That(badRequestResult?.Value, Is.InstanceOf<SerializableError>());

                var errors = badRequestResult?.Value as SerializableError;
                Assert.That(errors?.ContainsKey("Email"), Is.True);
                Assert.That(((string[])errors["Email"])[0], Is.EqualTo("Email field is missing!"));

            });

        }

        [Test]
        public async Task RegisterMethodFailsIfRegistrationIsNotSuccessful()
        {
            var registrationRequest = new RegistrationRequest("random@random.com", "Test", "password");
            var notSuccessfulAuthResult = new AuthResult(false, registrationRequest.Username, registrationRequest.Email, "");
            notSuccessfulAuthResult.ErrorMessages.Add("DuplicateUserName", "User name 'Test' is already taken.");
            _authController.ModelState.Clear();
            _authServiceMock.Setup(x => x.RegisterAsync(registrationRequest.Username, registrationRequest.Email, registrationRequest.Password, "User")).ReturnsAsync(notSuccessfulAuthResult);

            var result = await _authController.Register(registrationRequest);

            Assert.Multiple(() => {
                Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
                var badRequestResult = result.Result as BadRequestObjectResult;
                Assert.That(badRequestResult, Is.Not.Null);
                Assert.That(badRequestResult?.Value, Is.InstanceOf<SerializableError>());
                var errors = badRequestResult?.Value as SerializableError;
                Assert.That(errors?.ContainsKey("DuplicateUserName"), Is.True);
                Assert.That(((string[])errors["DuplicateUserName"])[0], Is.EqualTo("User name 'Test' is already taken."));
            });
        }

        [Test]
        public async Task RegisterMethodReturnsRegistrationResponseIfEverythingIsOk()
        {
            var registrationRequest = new RegistrationRequest("random@random.com", "Joe", "password");
            var successfulAuthResult = new AuthResult(true, registrationRequest.Username, registrationRequest.Email, "");
            var registrationResponse = new RegistrationResponse(registrationRequest.Email, registrationRequest.Username);
            _authController.ModelState.Clear();
            _authServiceMock.Setup(x => x.RegisterAsync(registrationRequest.Username, registrationRequest.Email, registrationRequest.Password, "User")).ReturnsAsync(successfulAuthResult);

            var result = await _authController.Register(registrationRequest);

            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
                var createdAtResult = result.Result as CreatedAtActionResult;
                Assert.That(createdAtResult?.Value, Is.EqualTo(registrationResponse));
            });

        }

        [Test]
        public async Task AuthenticateMethodFailsIfRequestIsNotValid()
        {
            var invalidAuthRequest = new AuthRequest("random@random.com", "");
            _authController.ModelState.AddModelError("Password", "Password field is missing!");

            var result = await _authController.Authenticate(invalidAuthRequest);

            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
                var badRequestResult = result.Result as BadRequestObjectResult;
                Assert.That(badRequestResult, Is.Not.Null);
                Assert.That(badRequestResult?.Value, Is.InstanceOf<SerializableError>());

                var errors = badRequestResult?.Value as SerializableError;
                Assert.That(errors?.ContainsKey("Password"), Is.True);
                Assert.That(((string[])errors["Password"])[0], Is.EqualTo("Password field is missing!"));
            });
        }

        [Test]
        public async Task AuthenticateMethodFailsIfAuthenticationIsNotSuccessful()
        {
            var authRequest = new AuthRequest("random@random.com", "test");
            var notSuccessfulAuthResult = new AuthResult(false, "Joe", authRequest.Email, "");
            notSuccessfulAuthResult.ErrorMessages.Add("Bad credentials", "Invalid password");
            _authController.ModelState.Clear();
            _authServiceMock.Setup(x => x.LoginAsync(authRequest.Email, authRequest.Password)).ReturnsAsync(notSuccessfulAuthResult);

            var result = await _authController.Authenticate(authRequest);

            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
                var badRequestResult = result.Result as BadRequestObjectResult;
                Assert.That(badRequestResult, Is.Not.Null);
                Assert.That(badRequestResult?.Value, Is.InstanceOf<SerializableError>());
                var errors = badRequestResult?.Value as SerializableError;
                Assert.That(errors?.ContainsKey("Bad credentials"), Is.True);
                Assert.That(((string[])errors["Bad credentials"])[0], Is.EqualTo("Invalid password"));
            });
        }

        [Test]
        public async Task AuthenticateMethodReturnsAuthResponseIfEverythingIsOk()
        {
            var authRequest = new AuthRequest("random@random.com", "password");
            var successfulAuthResult = new AuthResult(true, "Joe", authRequest.Email, "token");
            var authResponse = new AuthResponse(successfulAuthResult.Email, successfulAuthResult.Username, successfulAuthResult.Token);
            _authController.ModelState.Clear();
            _authServiceMock.Setup(x => x.LoginAsync(authRequest.Email, authRequest.Password)).ReturnsAsync(successfulAuthResult);

            var result = await _authController.Authenticate(authRequest);

            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
                var okResult = result.Result as OkObjectResult;
                Assert.That(okResult?.Value, Is.EqualTo(authResponse));
            });

        }

    }
}
