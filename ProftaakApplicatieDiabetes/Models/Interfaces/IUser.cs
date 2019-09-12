using Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public interface IUser
    {

        int BSN { get; }
        string FirstName { get; }
        string LastName { get; }
        string Address { get; }
        string City { get; }
        string EmailAddress { get; }
        DateTime BirthDate { get; }
        Gender UserGender { get; }
        AccountType UserAccountType { get; }
        bool Status { get; }
        string Password { get; }
        User Doctor { get; }
    }
}
