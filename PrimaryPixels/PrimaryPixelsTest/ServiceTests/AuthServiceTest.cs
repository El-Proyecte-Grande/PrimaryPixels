using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using PrimaryPixels.Services.Authentication;

namespace PrimaryPixelsTest.ServiceTests
{
    [TestFixture]
    public class AuthServiceTest
    {
        private AuthService _authService;
        private Mock<UserManager<IdentityUser>> _userManagerMock;
        private Mock<ITokenService> _mockTokenService; 
        private Mock<UserManager<IdentityUser>> GetMockUserManager()
        {
            var store = new Mock<IUserStore<IdentityUser>>();
            return new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
        }

        [SetUp]
        public void Setup()
        {
            _userManagerMock = GetMockUserManager();
            _mockTokenService = new();
            _authService = new(_userManagerMock.Object, _mockTokenService.Object);
        }

        [Test]
        public async Task RegisterAsyncReturnsFalseAuthResultBecauseUserCreationWasNotSuccessful()
        {
            string userName = "Joe";
            string email = "joe@joe.com";
            string password = "werZT!!56";
            string role = "User";
            IdentityUser invalidIdentityUser = new() { UserName = userName, Email = email};
            IdentityError[] errors = [new() { Code = "DuplicateUserName", Description = "The username is already taken." }, new(){ Code = "DuplicateEmail", Description = "The email is already registered." }];
            _userManagerMock.Setup(x => x.CreateAsync(invalidIdentityUser, password)).ReturnsAsync(IdentityResult.Failed(errors));

            var result = await _authService.RegisterAsync(userName, email, password, role);

            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.False);
                Assert.That(result.ErrorMessages["DuplicateUserName"], Is.EqualTo("The username is already taken."));
            });
        }

    }
}
