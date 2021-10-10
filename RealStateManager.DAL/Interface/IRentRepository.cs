using RealStateManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Interface
{
    public interface IRentRepository : IRepositoryGeneric<Rent>
    {
        bool RentExists(int monthId, int year);

        new Task<IEnumerable<Rent>> GetAll();

        Task<IEnumerable<int>> GetAllYears();
    }
}
