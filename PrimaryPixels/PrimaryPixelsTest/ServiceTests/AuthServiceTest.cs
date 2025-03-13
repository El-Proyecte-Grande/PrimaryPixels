using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using NuGet.Frameworks;
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
            IdentityError[] errors = [new() { Code = "DuplicateUserName", Description = "The username is already taken." }, new(){ Code = "DuplicateEmail", Description = "The email is already registered." }];
            _userManagerMock.Setup(x => x.CreateAsync(It.Is<IdentityUser>(u => u.UserName == userName && u.Email == email), password)).ReturnsAsync(IdentityResult.Failed(errors));

            var result = await _authService.RegisterAsync(userName, email, password, role);

            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.False);
                Assert.That(result.ErrorMessages["DuplicateUserName"], Is.EqualTo("The username is already taken."));
                Assert.That(result.ErrorMessages["DuplicateEmail"], Is.EqualTo("The email is already registered."));
            });
        }

        [Test]
        public async Task RegisterAsyncReturnsFalseAuthResultBecauseAddingToRoleWasNotSuccessful()
        {
            string userName = "Jack";
            string email = "jack@jack.com";
            string password = "werZT!!56";
            string invalidRole = "user";
            IdentityError[] errors = [new() { Code = "RoleNotFound", Description = "The role does not exist in the database." }];
            _userManagerMock.Setup(x => x.CreateAsync(It.Is<IdentityUser>(u => u.UserName == userName && u.Email == email), password)).ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(x => x.AddToRoleAsync(It.Is<IdentityUser>(u => u.UserName == userName && u.Email == email), invalidRole)).ReturnsAsync(IdentityResult.Failed(errors));

            var result = await _authService.RegisterAsync(userName, email, password, invalidRole);

            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.False);
                Assert.That(result.ErrorMessages["RoleNotFound"], Is.EqualTo("The role does not exist in the database."));
            });

        }

        [Test]
        public async Task RegisterAsyncReturnsTrueAuthResultIfEverythingIsOk()
        {
            string userName = "Jack";
            string email = "jack@jack.com";
            string password = "werZT!!56";
            string role = "User";
            _userManagerMock.Setup(x => x.CreateAsync(It.Is<IdentityUser>(u => u.UserName == userName && u.Email == email), password)).ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(x => x.AddToRoleAsync(It.Is<IdentityUser>(u => u.UserName == userName && u.Email == email), role)).ReturnsAsync(IdentityResult.Success);

            var result = await _authService.RegisterAsync(userName, email, password, role);

            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.True);
                Assert.That(result.Username, Is.EqualTo(userName));
                Assert.That(result.Email, Is.EqualTo(email));
            });

        }

    }
}
