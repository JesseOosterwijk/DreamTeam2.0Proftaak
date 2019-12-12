using System.ComponentModel.DataAnnotations;

namespace ProftaakApplicatieDiabetes.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Het wachtwoord moet worden ingevuld.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Het wachtwoord moet worden ingevuld.")]
        public string PasswordValidation { get; set; }
        public int UserId { get; set; }
        public ChangePasswordViewModel(string password, int userId)
        {
            Password = password;
            UserId = userId;
        }

        public ChangePasswordViewModel()
        {

        }
    }
}
