using System;
using System.Collections.Generic;
using Data.Interfaces;
using Logic.Interface;
using Models;


namespace Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserContext _user;

        public UserLogic(IUserContext user)
        {
            _user = user;
        }
    }
}
