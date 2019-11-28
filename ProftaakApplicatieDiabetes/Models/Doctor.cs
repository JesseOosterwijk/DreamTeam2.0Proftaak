using System;
using Enums;

namespace Models
{
    public class Doctor : User
    {
        public Doctor(int bsn, string firstName, string lastName, string address, string city, string emailAddress, DateTime birthDate, Gender userGender, bool status, AccountType accountType, string password) : base(bsn, firstName, lastName, address, city, emailAddress, birthDate, userGender, status, accountType, password)
        {

        }

        public Doctor(int userId, int userBSN, AccountType accountType, string firstName, string lastName, string email, string password, string address, string residence, Gender gender, DateTime dateOfBirth, bool status) : base(userId, userBSN, accountType, firstName, lastName, email, password, address, residence, gender, dateOfBirth, status)
        {
        }

    }
}
