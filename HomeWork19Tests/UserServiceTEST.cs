using Xunit;
using HomeWork19.FakeServices;
using HomeWork19.Models;

namespace HomeWork19.Tests
{
    public class UserServiceTEST
    {
        [Fact]
        public void Login_ValidUser_ReturnsUser()
        {
            // Arrange
            var fakeUserService = new FakeUserService();

            var userLoginModel = new UserLoginModel
            {
                UserName = "User123",
                Password = "User123"
            };

            // Act
            var result = fakeUserService.Login(userLoginModel);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("User123", result.UserName);
        }

        [Fact]
        public void Login_InvalidUser_ReturnsNull()
        {
            // Arrange
            var fakeUserService = new FakeUserService();

            var userLoginModel = new UserLoginModel
            {
                UserName = "NonExistingUser",
                Password = "SomePassword"
            };

            // Act
            var result = fakeUserService.Login(userLoginModel);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Register_ValidUser_ReturnsUser()
        {
            // Arrange
            var fakeUserService = new FakeUserService();

            var registrationModel = new UserRegisterModel
            {
                FirstName = "New",
                LastName = "User",
                UserName = "NewUser",
                Password = "NewUser123"
            };

            // Act
            var result = fakeUserService.Register(registrationModel);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("NewUser", result.UserName);
        }
    }
}
