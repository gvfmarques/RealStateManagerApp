using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Repositories
{
    public class ApartmentRepository : RepositoryGeneric<Apartment>, IApartmentRepository
    {
        private readonly RealStateManagerContext _realStateManagerContext;
        public ApartmentRepository(RealStateManagerContext realStateManagerContext) : base(realStateManagerContext)
        {
            _realStateManagerContext = realStateManagerContext;
        }

        public async Task<IEnumerable<Apartment>> GetApartmentByUser(string userId)
        {
            try
            {
                return await _realStateManagerContext.Apartments
                    .Include(a => a.ApartmentResident).Include(a => a.ApartmentOwner)
                    .Where(a => a.ApartmentResidentId == userId || a.ApartmentOwnerId == userId).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public new async Task<IEnumerable<Apartment>> GetAll()
        {
            try
            {
                return await _realStateManagerContext.Apartments.Include(a => a.ApartmentResident).Include(a => a.ApartmentOwner).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
