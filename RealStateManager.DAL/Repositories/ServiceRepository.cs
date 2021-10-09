using Microsoft.EntityFrameworkCore;
using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Repositories
{
    public class ServiceRepository : RepositoryGeneric<Service>, IServiceRepository
    {
        private readonly RealStateManagerContext _realStateManagerContext;
        public ServiceRepository(RealStateManagerContext realStateManagerContext) : base(realStateManagerContext)
        {
            _realStateManagerContext = realStateManagerContext;
        }

        public async Task<IEnumerable<Service>> GetServiceByUser(string userId)
        {
            try
            {
                return await _realStateManagerContext.Services.Where(s => s.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}