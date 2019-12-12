using Models;
using System.Collections.Generic;

namespace Data.Interfaces
{
    public interface IUserContext
    {
        bool CheckIfUserAlreadyExists(string email);
        bool CheckIfAccountIsActive(string email);
        User GetUserInfo(string email);
        User GetUserById(int userId);
        bool CheckIfEmailIsValid(string userEmail);
        User CheckValidityUser(string emailAddress, string password);
        void CreateUser(User user);
        List<User> GetAllUsers();
        bool SendEmail(string emailaddress, string newPassword);
    }
}
