﻿using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProftaakApplicatieDiabetes.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Het emailadres moet worden ingevuld")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Het wachtwoord moet worden ingevuld.")]
        public string Password { get; set; }

        public LoginViewModel(User user, string password)
        {
            EmailAddress = user.EmailAddress;
            Password = password;
        }

        public LoginViewModel()
        {

        }
    }
}
