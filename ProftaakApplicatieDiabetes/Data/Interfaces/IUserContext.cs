using System;
using System.Collections.Generic;
using System.Text;
using Models;

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
    }
}
