using RealStateManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Interface
{
    public interface IVehicleRepository : IRepositoryGeneric<Vehicle>
    {
        Task<IEnumerable<Vehicle>> GetVehiclesByUser(string userId);
    }
}
