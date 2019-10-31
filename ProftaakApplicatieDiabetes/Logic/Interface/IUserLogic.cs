﻿using Models;
using System;
using System.Collections.Generic;
using System.Text;


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
    }
}
