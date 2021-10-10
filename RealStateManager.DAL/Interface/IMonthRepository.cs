using RealStateManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Interface
{
    public interface IMonthRepository : IRepositoryGeneric<Month>
    {
        new Task<IEnumerable<Month>> GetAll();
    }
}
