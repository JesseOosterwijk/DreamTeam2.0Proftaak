using Models.Enums;
using System;

namespace Models
{
    public class User : IUser
    {
        public int BSN { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Address { get; }
        public string City { get; }
        public string EmailAddress { get; }
        public DateTime BirthDate { get; }
        public Gender UserGender { get; }
        public AccountType UserAccountType { get; }
        public bool Status { get; set; }
        public string Password { get; set; }
        public User Doctor { get; set; }

        protected User()
        {
               
        }

        protected User(string firstName, string lastName, string address, string city, string emailAddress, DateTime birthDate, Gender userGender, bool status, AccountType accountType, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            EmailAddress = emailAddress;
            BirthDate = birthDate;
            UserGender = userGender;
            Status = status;
            UserAccountType = accountType;
            Password = password;
        }

        protected User(int bsn, string firstName, string lastName, string address, string city, string emailAddress, DateTime birthDate, Gender userGender, bool status, AccountType accountType, string password)
        {
            BSN = bsn;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            EmailAddress = emailAddress;
            BirthDate = birthDate;
            UserGender = userGender;
            Status = status;
            UserAccountType = accountType;
            Password = password;
        }
    }
}
