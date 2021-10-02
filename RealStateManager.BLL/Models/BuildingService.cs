using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.BLL.Models
{
    public class BuildingService
    {
        public int BuildingServiceId { get; set; }

        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }

        public DateTime ExecutionDate { get; set; }
    }
}
