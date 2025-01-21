using System;
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

namespace PrimaryPixelsTest
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
            _authController = new AuthController( _authServiceMock.Object );
        }

        [Test]
        public async Task RegisterMethodFailsIfRequestIsNotValid()
        {
            var registrationRequest = new RegistrationRequest("", "Test", "password");
            _authController.ModelState.AddModelError("Email", "Email field is missing!");

            var result = await _authController.Register(registrationRequest);

            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
                var badRequestResult = result.Result as BadRequestObjectResult;
                Assert.That(badRequestResult, Is.Not.Null);
                Assert.That(badRequestResult.Value, Is.InstanceOf<SerializableError>());

                var errors = badRequestResult.Value as SerializableError;
                Assert.That(errors?.ContainsKey("Email"), Is.True);
                Assert.That(((string[])errors["Email"])[0], Is.EqualTo("Email field is missing!"));

            });

        }
    }
}
