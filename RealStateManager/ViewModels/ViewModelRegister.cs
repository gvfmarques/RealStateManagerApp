using System.ComponentModel.DataAnnotations;

namespace RealStateManager.ViewModels
{
    public class ViewModelRegister
    {
        [Required(ErrorMessage = "The {0} field is mandatory")]
        [StringLength(40, ErrorMessage = "use less characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        public string Identification { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        public string Phone { get; set; }

        public string Picture { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        [StringLength(40, ErrorMessage = "use less characters")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        [StringLength(40, ErrorMessage = "use less characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        [StringLength(40, ErrorMessage = "use less characters")]
        [DataType(DataType.Password)]
        [Display(Name = "confirm your password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmedPassword { get; set; }
    }
}
