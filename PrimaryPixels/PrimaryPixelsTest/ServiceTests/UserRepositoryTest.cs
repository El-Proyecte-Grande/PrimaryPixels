using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using PrimaryPixels.DTO;
using PrimaryPixels.Exceptions;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixelsTest.ServiceTests
{
    [TestFixture]
    public class UserRepositoryTest
    {
        private UserRepository _userRepository;
        private Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly IdentityUser _identityUser = new() { Id = "234", UserName = "Joe", Email = "joe@joe.com"};
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

        [Test]
        public async Task GetUserByIdReturnsUserResponseSuccessfully()
        {
            _userManagerMock.Setup(x => x.FindByIdAsync(_identityUser.Id)).ReturnsAsync(_identityUser);
            UserResponse userResponse = new(_identityUser.UserName, _identityUser.Email);

            var result = await _userRepository.GetUserById(_identityUser.Id);

            Assert.That(result, Is.EqualTo(userResponse));
        }

        [Test]
        public async Task ChangePasswordAsyncReturnsFalseIfUserCannotBeFound()
        {
            string notValidId = "0123";
            string currentPassword = "00000fgH";
            string newPassword = "Werg345!!zu78";
            _userManagerMock.Setup(x => x.FindByIdAsync(notValidId)).ReturnsAsync((IdentityUser)null);

            var result = await _userRepository.ChangePasswordAsync(currentPassword, newPassword, notValidId);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task ChangePasswordAsyncReturnsFalseIfChangingPasswordFails()
        {
            string currentPassword = "00000fgH";
            string newPassword = "Werg345!!zu78";
            _userManagerMock.Setup(x => x.FindByIdAsync(_identityUser.Id)).ReturnsAsync(_identityUser);
            _userManagerMock.Setup(x => x.ChangePasswordAsync(_identityUser, currentPassword, newPassword)).ReturnsAsync(IdentityResult.Failed());

            var result = await _userRepository.ChangePasswordAsync(currentPassword, newPassword, _identityUser.Id);

            Assert.That(result, Is.False);
        }


        [Test]
        public async Task ChangePasswordAsyncReturnsTrueIfPasswordIsChanged()
        {
            string currentPassword = "00000fgH";
            string newPassword = "Werg345!!zu78";
            _userManagerMock.Setup(x => x.FindByIdAsync(_identityUser.Id)).ReturnsAsync(_identityUser);
            _userManagerMock.Setup(x => x.ChangePasswordAsync(_identityUser, currentPassword, newPassword)).ReturnsAsync(IdentityResult.Success);

            var result = await _userRepository.ChangePasswordAsync(currentPassword, newPassword, _identityUser.Id);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GetPasswordResetTokenThrowsExceptionIfUserCannotBeFound()
        {
            string invalidEmail = "dummy@dummy.com";
            _userManagerMock.Setup(x => x.FindByEmailAsync(invalidEmail)).ReturnsAsync((IdentityUser)null);

            var result = Assert.ThrowsAsync<EmailNotFoundException>(() => _userRepository.GetPasswordResetToken(invalidEmail));

            Assert.That(result.Message, Is.EqualTo("Couldn't find user with this email"));
        }

        [Test]
        public async Task GetPasswordResetTokenReturnsTokenIfEveryThingIsAlright()
        {
            string validToken = "qwdwffbgfb!+%123";
            _userManagerMock.Setup(x => x.FindByEmailAsync(_identityUser.Email)).ReturnsAsync(_identityUser);
            _userManagerMock.Setup(x => x.GeneratePasswordResetTokenAsync(_identityUser)).ReturnsAsync(validToken);

            var result = await _userRepository.GetPasswordResetToken(_identityUser.Email);

            Assert.That(result, Is.EqualTo(Uri.EscapeDataString(validToken)));
        }

        [Test]
        public void ResetPasswordThrowsExceptionIfUserCannotBeFound()
        {
            string invalidEmail = "dummy@dummy.com";
            string encodedToken = "qwdwffbgfb%21%2B%25123";
            string newPassword = "Werg345!!zu78";
            _userManagerMock.Setup(x => x.FindByEmailAsync(invalidEmail)).ReturnsAsync((IdentityUser)null);

            var result = Assert.ThrowsAsync<EmailNotFoundException>(() => _userRepository.ResetPassword(invalidEmail, encodedToken, newPassword));

            Assert.That(result.Message, Is.EqualTo("Couldn't find user with this email"));
        }

        [Test]
        public async Task ResetPasswordReturnsFalseIfResettingThePasswordFails()
        {
            string encodedToken = "qwdwffbgfb%21%2B%25123";
            string newPassword = "Werg345!!zu78";
            _userManagerMock.Setup(x => x.FindByEmailAsync(_identityUser.Email)).ReturnsAsync(_identityUser);
            _userManagerMock.Setup(x => x.ResetPasswordAsync(_identityUser, Uri.UnescapeDataString(encodedToken), newPassword)).ReturnsAsync(IdentityResult.Failed());

            var result = await _userRepository.ResetPassword(_identityUser.Email, encodedToken, newPassword);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task ResetPasswordReturnsTrueIfPasswordIsSuccessfullyResetted()
        {
            string encodedToken = "qwdwffbgfb%21%2B%25123";
            string newPassword = "Werg345!!zu78";
            _userManagerMock.Setup(x => x.FindByEmailAsync(_identityUser.Email)).ReturnsAsync(_identityUser);
            _userManagerMock.Setup(x => x.ResetPasswordAsync(_identityUser, Uri.UnescapeDataString(encodedToken), newPassword)).ReturnsAsync(IdentityResult.Success);

            var result = await _userRepository.ResetPassword(_identityUser.Email, encodedToken, newPassword);

            Assert.That(result, Is.True);
        }
    }
}
