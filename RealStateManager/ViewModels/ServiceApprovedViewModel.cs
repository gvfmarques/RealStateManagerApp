using System;
using System.ComponentModel.DataAnnotations;

namespace RealStateManager.ViewModels
{
    public class ServiceApprovedViewModel
    {
        public int ServiceId { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [Display(Name = "Execution date")]
        public DateTime ExecutionDate { get; set; }
    }
}
