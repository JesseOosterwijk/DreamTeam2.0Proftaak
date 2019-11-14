using System;
using Enums;

namespace Models
{
    public class Administrator : User
    {
        public Administrator(int bsn, string firstName, string lastName, string address, string city, string emailAddress, DateTime birthDate, Gender userGender, bool status, AccountType accountType, string password) : base(bsn, firstName, lastName, address, city, emailAddress, birthDate, userGender, status, accountType, password)
        {

        }

        public Administrator(string firstName, string lastName, string address, string city, string emailAddress, DateTime birthDate, Enums.Gender userGender, bool status, Enums.AccountType accountType, string password) : base(firstName, lastName, address, city, emailAddress, birthDate, userGender, status, accountType,  password)
        {

        }

        public Administrator(int userId, int userBSN, AccountType accountType, string firstName, string lastName, string email, string password, string address, string residence, Gender gender, int weight, DateTime dateOfBirth, bool status) : base(userId, userBSN, accountType, firstName, lastName, email, password, address, residence, gender, weight, dateOfBirth, status)
        {
        }

        public Administrator(int userBSN, AccountType accountType, string firstName, string lastName, string email, string password, string address, string residence, Gender gender, int weight, DateTime dateOfBirth, bool status) : base(userBSN, accountType, firstName, lastName, email, password, address, residence, gender, weight, dateOfBirth, status)
        {

        }
    }
}
