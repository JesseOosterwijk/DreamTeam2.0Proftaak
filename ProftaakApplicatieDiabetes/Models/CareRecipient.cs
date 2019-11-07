using System;
using System.Collections.Generic;
using System.Text;
using Enums;

namespace Models
{
    public class CareRecipient : User
    {
        public CareRecipient(int bsn, string firstName, string lastName, string address, string city, string emailAddress, DateTime birthDate, Gender userGender, bool status, AccountType accountType, string password) :base(bsn, firstName, lastName, address, city, emailAddress, birthDate, userGender, status, accountType, password)
        {

        }

        public CareRecipient(string firstName, string lastName, string address, string city, string emailAddress, DateTime birthDate, Gender userGender, bool status, AccountType accountType, string password) : base(firstName, lastName, address, city, emailAddress, birthDate, userGender, status, accountType, password)
        {

        }

        //take
        public CareRecipient(int userId, int userBSN, AccountType accountType, string firstName, string lastName, string email, string password, string address, string residence, Gender gender, int weight, DateTime dateOfBirth, bool status) : base(userId, userBSN, accountType, firstName, lastName, email, password, address, residence, gender, weight, dateOfBirth, status)
        {
        }

        public CareRecipient(int userBSN, AccountType accountType, string firstName, string lastName, string email, string password, string address, string residence, Gender gender, int weight, DateTime dateOfBirth, bool status) : base(userBSN, accountType, firstName, lastName, email, password, address, residence, gender, weight, dateOfBirth, status)
        {

        }
    }
}
