using Enums;
using System;

namespace Models
{
    public class User : IUser
    {
        public int UserId { get; set; }
        public int BSN { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Address { get; }
        public string Residence { get; }
        public string EmailAddress { get; }
        public DateTime BirthDate { get; }
        public Gender UserGender { get; }
        public AccountType UserAccountType { get; }
        public bool Status { get; set; }
        public string Password { get; set; }
        public int Weight { get; set; }
        public User Doctor { get; set; }

        protected User()
        {
               
        }

        protected User(string firstName, string lastName, string address, string residence, string emailAddress, DateTime birthDate, Gender userGender, bool status, AccountType accountType, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Residence = residence;
            EmailAddress = emailAddress;
            BirthDate = birthDate;
            UserGender = userGender;
            Status = status;
            UserAccountType = accountType;
            Password = password;
        }

        public User(int userId, int userBSN, AccountType accountType, string firstName, string lastName, string email, string password, string address, string residence, Gender gender, int weight, DateTime dateOfBirth, bool status)
        {
            UserId = userId;
            BSN = userBSN;
            UserAccountType = accountType;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = email;
            Password = password;
            Address = address;
            Residence = residence;
            UserGender = gender;
            Weight = weight;
            BirthDate = dateOfBirth;
            Status = status;
        }

        public User(int userBSN, AccountType accountType, string firstName, string lastName, string email, string password, string address, string residence, Gender gender, int weight, DateTime dateOfBirth, bool status)
        {
            BSN = userBSN;
            UserAccountType = accountType;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = email;
            Password = password;
            Address = address;
            Residence = residence;
            UserGender = gender;
            Weight = weight;
            BirthDate = dateOfBirth;
            Status = status;
        }


        protected User(int bsn, string firstName, string lastName, string address, string residence, string emailAddress, DateTime birthDate, Gender userGender, bool status, AccountType accountType, string password)
        {
            BSN = bsn;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Residence = residence;
            EmailAddress = emailAddress;
            BirthDate = birthDate;
            UserGender = userGender;
            Status = status;
            UserAccountType = accountType;
            Password = password;
        }
    }
}
