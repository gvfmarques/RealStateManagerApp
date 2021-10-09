using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealStateManager.ViewModels
{
    public class ServiceApprovedViewModel
    {
        public int ServiceId { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [Display(Name = "Exclusion date")]
        public DateTime Date { get; set; }
    }
}
