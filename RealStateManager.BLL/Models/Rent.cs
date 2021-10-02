using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealStateManager.BLL.Models
{
    public class Rent
    {
        public int RentId { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        [Range(0, int.MaxValue, ErrorMessage = "Invalid Value")]
        public decimal RentValue { get; set; }

        [Display(Name = "Month")]
        public int MonthId { get; set; }
        public Month Month { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        [Range(2020, 2040, ErrorMessage = "Invalid Value")]
        public int Year { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
