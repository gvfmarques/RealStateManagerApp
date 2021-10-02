using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.BLL.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int RentId { get; set; }
        public Rent Rent { get; set; }

        public DateTime? DatePayment { get; set; }

        public StatusPayment Status { get; set; }
    }

    public enum StatusPayment
    {
        PaidOut, Pending, Late
    }
}
