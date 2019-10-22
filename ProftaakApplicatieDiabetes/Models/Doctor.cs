using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Doctor : User
    {
        public Doctor(int bsn, string firstName, string lastName, string address, string city, string emailAddress, DateTime birthDate, Enums.Gender userGender, bool status, Enums.AccountType accountType, string password) : base(bsn, firstName, lastName, address, city, emailAddress, birthDate, userGender, status, accountType, password)
        {

        }

        public Doctor(string firstName, string lastName, string address, string city, string emailAddress, DateTime birthDate, Enums.Gender userGender, bool status, Enums.AccountType accountType, string password) : base(firstName, lastName, address, city, emailAddress, birthDate, userGender, status, accountType, password)
        {

        }

    }
}
