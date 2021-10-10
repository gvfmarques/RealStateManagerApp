using RealStateManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Interface
{
    public interface IPaymentRepository : IRepositoryGeneric<Payment>
    {
        Task<IEnumerable<Payment>> GetPaymentByUser(string userId);
    }
}
