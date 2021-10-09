using RealStateManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Interface
{
    public interface IApartmentRepository : IRepositoryGeneric<Apartment>
    {
        new Task<IEnumerable<Apartment>> GetAll();

        Task<IEnumerable<Apartment>> GetApartmentByUser(string userId);
    }
}
