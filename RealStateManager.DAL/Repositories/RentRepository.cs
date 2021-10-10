using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Repositories
{
    public class RentRepository : RepositoryGeneric<Rent>, IRentRepository
    {
        private readonly RealStateManagerContext _realStateManagerContext;

        public RentRepository(RealStateManagerContext realStateManagerContext) : base(realStateManagerContext)
        {
            _realStateManagerContext = realStateManagerContext;
        }

        public async Task<IEnumerable<int>> GetAllYears()
        {
            try
            {
                return await _realStateManagerContext.Rents.Select(a => a.Year).Distinct().ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool RentExists(int monthId, int year)
        {
            try
            {
                return _realStateManagerContext.Rents.Any(a => a.MonthId == monthId && a.Year == year);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public new async Task<IEnumerable<Rent>> GetAll()
        {
            try
            {
                return await _realStateManagerContext.Rents.Include(a => a.Month).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
