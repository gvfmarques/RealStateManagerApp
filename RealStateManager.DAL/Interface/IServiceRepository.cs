using RealStateManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Interface
{
    public interface IServiceRepository : IRepositoryGeneric<Service>
    {
        Task<IEnumerable<Service>> GetServiceByUser(string userId);
    }
}
