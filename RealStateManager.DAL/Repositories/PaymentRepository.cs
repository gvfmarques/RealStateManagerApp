using Microsoft.EntityFrameworkCore;
using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Repositories
{
    public class PaymentRepository : RepositoryGeneric<Payment>, IPaymentRepository
    {

        private readonly RealStateManagerContext _realStateManagerContext;

        public PaymentRepository(RealStateManagerContext realStateManagerContext) : base(realStateManagerContext)
        {
            _realStateManagerContext = realStateManagerContext;
        }
        public async Task<IEnumerable<Payment>> GetPaymentByUser(string userId)
        {
            try
            {
                return await _realStateManagerContext.Payments.Include(p => p.Rent)
                    .ThenInclude(p => p.Month)
                    .Where(p => p.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
