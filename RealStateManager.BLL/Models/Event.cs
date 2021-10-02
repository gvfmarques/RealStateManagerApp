using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealStateManager.BLL.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        [StringLength(50, ErrorMessage = "Use less characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        public DateTime Date { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
