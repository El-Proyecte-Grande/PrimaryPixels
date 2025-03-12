using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixelsTest.ServiceTests
{
    [TestFixture]
    public class UserRepositoryTest
    {
        private UserRepository _userRepository;
        private Mock<UserManager<IdentityUser>> _userManagerMock;
        private IdentityUser _identityUser = new IdentityUser() { Id = "234", UserName = "Joe", Email = "joe@joe.com"};
        private Mock<UserManager<IdentityUser>> GetMockUserManager()
        {
            var store = new Mock<IUserStore<IdentityUser>>();
            return new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
        }

        [SetUp]
        public void Setup()
        {
            _userManagerMock = GetMockUserManager();
            _userRepository = new UserRepository(_userManagerMock.Object);
        }

        [Test]
        public void GetUserByIdThrowsExceptionBecauseUserDoesNotExist()
        {
            string idToFind = "0000";
            _userManagerMock.Setup(x => x.FindByIdAsync(idToFind)).ReturnsAsync((IdentityUser)null);

            var result = Assert.ThrowsAsync<InvalidOperationException>(() => _userRepository.GetUserById(idToFind));

            Assert.That(result.Message, Is.EqualTo("Couldn't find user with this id"));

        }



    }
}
