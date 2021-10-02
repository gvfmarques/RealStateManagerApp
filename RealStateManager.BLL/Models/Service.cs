using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.BLL.Models
{
    public class Service
    {
        public int ServiceId { get; set; }

        public string Name { get; set; }
        public decimal Value { get; set; }

        public StatusService Status { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public ICollection<BuildingService> BuildingServices { get; set; }
    }

    public enum StatusService
    {
        Pending, Refused, Accepted
    }

}
