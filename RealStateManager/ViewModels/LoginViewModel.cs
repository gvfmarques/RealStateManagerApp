using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealStateManager.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
