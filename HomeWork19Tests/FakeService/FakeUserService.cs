using System.Collections.Generic;
using HomeWork19.Domain;
using HomeWork19.Models;
using HomeWork19.Services;

namespace HomeWork19.FakeServices
{
    public class FakeUserService : IUserService
    {
        private readonly List<User> _users = new List<User>();

        public FakeUserService()
        {
            // Initialize some fake users for testing
            _users.Add(new User
            {
                Id = 2,
                FirstName = "User123",
                LastName = "User123",
                UserName = "User123",
                Password = "User123",
                Role = Roles.User
            });
        }

        public User Login(UserLoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.UserName) || string.IsNullOrEmpty(loginModel.Password))
            {
                return null;
            }
            var user = _users.Find(x => x.UserName == loginModel.UserName);
            if (user == null)
            {
                return null;
            }
            if (user.Password != loginModel.Password)
            {
                return null;
            }
            return user;
        }

        public User Register(UserRegisterModel registrationModel)
        {
            var newUser = new User
            {
                FirstName = registrationModel.FirstName,
                LastName = registrationModel.LastName,
                UserName = registrationModel.UserName,
                Password = registrationModel.Password,
                Role = Roles.User
            };

            _users.Add(newUser);
            return newUser;
        }
    }
}
