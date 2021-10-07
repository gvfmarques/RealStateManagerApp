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
    public class VehicleRepository : RepositoryGeneric<Vehicle>, IVehicleRepository
    {
        private readonly RealStateManagerContext _realStateManagerContext;
        public VehicleRepository(RealStateManagerContext realStateManagerContext) : base(realStateManagerContext)
        {
            _realStateManagerContext = realStateManagerContext;
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesByUser(string userId)
        {
            try
            {
                return await _realStateManagerContext.Vehicles.Where(v => v.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
