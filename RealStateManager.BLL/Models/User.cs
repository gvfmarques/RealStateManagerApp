using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.BLL.Models
{
    public class User : IdentityUser<string>
    {
        public string Identification { get; set; }
        public string Picture { get; set; }
        public bool FirstAccess { get; set; }
        public StatusAccount Status { get; set; }

        public virtual ICollection<Apartment> ApartmentResidents { get; set; }
        public virtual ICollection<Apartment> ApartmentOwners { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }

    public enum StatusAccount
    {
        Analysing, Approved, Disapproved
    }
}
