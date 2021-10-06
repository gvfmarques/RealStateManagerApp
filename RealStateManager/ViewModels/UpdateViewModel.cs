using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealStateManager.ViewModels
{
    public class UpdateViewModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [StringLength(40, ErrorMessage = "Use less characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Identification { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Phone { get; set; }

        public string Picture { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [StringLength(40, ErrorMessage = "Use less characters")]
        [EmailAddress(ErrorMessage = "Invalid Email Address!")]
        public string Email { get; set; }
    }
}
