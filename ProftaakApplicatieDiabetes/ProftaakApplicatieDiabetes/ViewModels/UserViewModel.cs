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

        [Required(ErrorMessage = "Gewicht vereist!")]
        public int Weight { get; set; }

        public int UserId { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "BSN ")]
        [Required(ErrorMessage = "BSN Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Zorg ervoor dat uw BSN klopt")]
        public int UserBSN { get; set; }

        [Required(ErrorMessage = "Voornaam vereist!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Achternaam vereist!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Adres vereist!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Woonplaats vereist!")]
        public string Residence { get; set; }

        [Required(ErrorMessage = "EmailAdres vereist!")]
        [EmailAddress(ErrorMessage = "Incorrect emailadres ingevoerd")]
        public string EmailAddress { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Geboortedag vereist!")]
        public DateTime BirthDate { get; set; }

        public string UserGender { get; set; }

        public string Type { get; set; }
        public Enums.AccountType UserAccountType { get; set; }

        public bool Status { get; set; }

        public User Doctor { get; set; }

        public bool ShareInfo { get; set; }

        public bool DeleteAllow { get; set; }

        public IEnumerable<User> Users { get; set; }

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
            ShareInfo = user.InfoSharing;
            DeleteAllow = user.InfoDeleteAllow;
        }

        public UserViewModel()
        {

        }
    }
}
