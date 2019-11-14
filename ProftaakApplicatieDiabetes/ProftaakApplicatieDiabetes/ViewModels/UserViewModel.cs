using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models;

namespace ProftaakApplicatieDiabetes.Models
{
    public class UserViewModel
    {
        public enum AccountType { CareRecipient, Volunteer, Professional, Admin }
        public enum Gender { Man, Vrouw, Anders }
        public int Weight { get; set; }
        public int UserId { get; set; }
        public int UserBSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Residence { get; set; }
        public string EmailAddress { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string UserGender { get; set; }
        public Enums.AccountType UserAccountType { get; set; }
        public bool Status { get; set; }
        public User Doctor { get; set; }

        public IEnumerable<User> Users = new List<User>();

        public UserViewModel(User user)
        {
            UserId = user.UserId;
            UserBSN = user.BSN;
            UserAccountType = user.UserAccountType;
            FirstName = user.FirstName;
            LastName = user.LastName;
            EmailAddress = user.EmailAddress;
            Address = user.Address;
            Residence = user.Residence;
            UserGender = user.UserGender.ToString();
            Weight = user.Weight;
            BirthDate = user.BirthDate.Date;
            Status = user.Status;
        }

        public UserViewModel()
        {

        }
    }
}
