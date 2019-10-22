using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class CareRecipient : User
    {
        public CareRecipient(int bsn, string firstName, string lastName, string address, string city, string emailAddress, DateTime birthDate, Enums.Gender userGender, bool status, Enums.AccountType accountType, string password) :base(bsn, firstName, lastName, address, city, emailAddress, birthDate, userGender, status, accountType, password)
        {

        }

        public CareRecipient(string firstName, string lastName, string address, string city, string emailAddress, DateTime birthDate, Enums.Gender userGender, bool status, Enums.AccountType accountType, string password) : base(firstName, lastName, address, city, emailAddress, birthDate, userGender, status, accountType, password)
        {

        }
    }
}
