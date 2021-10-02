using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealStateManager.BLL.Models
{
    public class Apartment
    {
        public int ApartmentId { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        [Range(0, 1000, ErrorMessage = "Invalid Value")]

        public int Number { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        [Range(0, 10, ErrorMessage = "Invalid Value")]
        public int Floor { get; set; }

        public string Picture { get; set; }

        public string ApartmentResidentId { get; set; }
        public virtual User ApartmentResident { get; set; }

        public string ApartmentOwnerId { get; set; }
        public virtual User ApartmentOwner { get; set; }
    }
}
