using Models;
using System.Collections.Generic;

namespace Logic.Interface
{
    public interface IUserLogic
    {
        bool CheckIfUserAlreadyExists(string email);
        bool CheckIfAccountIsActive(string email);
        User GetUserInfo(string email);
        User GetUserById(int userId);
        bool CheckIfEmailIsValid(string userEmail);
        User CheckValidityUser(string emailAddress, string password);
        void CreateUser(User newUser);
        List<User> GetAllUsers();
    }
}
