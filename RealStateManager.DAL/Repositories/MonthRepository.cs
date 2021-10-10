using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Repositories
{
    public class MonthRepository : RepositoryGeneric<Month>, IMonthRepository
    {
        private readonly RealStateManagerContext _realStateManagerContext;

        public MonthRepository(RealStateManagerContext realStateManagerContext) : base(realStateManagerContext)
        {
            _realStateManagerContext = realStateManagerContext;
        }

        public new async Task<IEnumerable<Month>> GetAll()
        {
            try
            {
                return await _realStateManagerContext.Months.OrderBy(m => m.MonthId).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
