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


    }
}
