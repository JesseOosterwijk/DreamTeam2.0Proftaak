using Enums;
using System;

namespace Models
{
    public class User : IUser
    {
        public int UserId { get; set; }
        public int BSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Residence { get; set; }
        public string EmailAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public int Weight { get; set; }
        public Gender UserGender { get; }
        public AccountType UserAccountType { get; }
        public bool Status { get; set; }
        public string Password { get; set; }
        public User Doctor { get; set; }
        public bool InfoSharing { get; set; }

        public User()
        {
               
        }

        public User(int userId, int userBSN, AccountType accountType, string firstName, string lastName, string email, string password, string address, string residence, Gender gender, DateTime dateOfBirth, int weight, bool status)
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
            BirthDate = dateOfBirth;
            Weight = weight;
            Status = status;
        }

        public User(int userBSN, AccountType accountType, string firstName, string lastName, string email, string password, string address, string residence, Gender gender, DateTime dateOfBirth, int weight, bool status)
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
            BirthDate = dateOfBirth;
            Weight = weight;
            Status = status;
        }


        public User(int bsn, string firstName, string lastName, string address, string residence, string emailAddress, DateTime birthDate, Gender userGender, bool status, AccountType accountType, int weight, string password)
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
            Weight = weight;
            Password = password;
        }

        public User(int userId, string firstName, AccountType accountType)
        {
            UserId = userId;
            FirstName = firstName;
            UserAccountType = accountType;
        }
    }
}
