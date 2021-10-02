using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealStateManager.BLL.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        [StringLength(20, ErrorMessage = "Use less characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        [StringLength(20, ErrorMessage = "Use less characters")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        [StringLength(20, ErrorMessage = "Use less characters")]
        public string Color { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        public string Plate { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
