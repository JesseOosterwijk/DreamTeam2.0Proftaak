using Data.Interfaces;
using Logic.Interface;
using Models;
using System.Collections.Generic;

namespace Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserContext _user;

        public UserLogic(IUserContext user)
        {
            _user = user;
        }

        public bool CheckIfUserAlreadyExists(string email)
        {
            return _user.CheckIfUserAlreadyExists(email);
        }

        public bool CheckIfAccountIsActive(string email)
        {
            return _user.CheckIfAccountIsActive(email);
        }

        public User GetUserInfo(string email)
        {
            return _user.GetUserInfo(email);
        }

        public User GetUserById(int userId)
        {
            return _user.GetUserById(userId);
        }

        public bool CheckIfEmailIsValid(string userEmail)
        {
            
            return _user.CheckIfEmailIsValid(userEmail);
        }

        public User CheckValidityUser(string emailAddress, string password)
        {
            return _user.CheckValidityUser(emailAddress, password);
        }

        public void CreateUser(User newUser)
        {
            newUser.Password = Hasher.SecurePasswordHasher.Hash(newUser.Password);

            _user.CreateUser(newUser);
        }

        public List<User> GetAllUsers()
        {
            return _user.GetAllUsers();
        }


    }
}
