using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.BLL.Models
{
    public class ServiceBuilding
    {
        public int ServiceBuildingId { get; set; }
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }

        public DateTime DateExecution { get; set; }
    }
}
