using System;
using Enums;

namespace Models
{
    public class CareRecipient : User
    {
        public CareRecipient(int bsn, string firstName, string lastName, string address, string city, string emailAddress, DateTime birthDate, Gender userGender, bool status, AccountType accountType, int weight, string password) :base(bsn, firstName, lastName, address, city, emailAddress, birthDate, userGender, status, accountType, weight, password)
        {

        }

        public CareRecipient(int userId, int userBSN, AccountType accountType, string firstName, string lastName, string email, string password, string address, string residence, Gender gender, DateTime dateOfBirth, int weight, bool status) : base(userId, userBSN, accountType, firstName, lastName, email, password, address, residence, gender, dateOfBirth, weight, status)
        {
        }
    }
}
