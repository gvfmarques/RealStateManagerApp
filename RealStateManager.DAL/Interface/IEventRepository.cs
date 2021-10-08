using RealStateManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Interface
{
    public interface IEventRepository : IRepositoryGeneric<Event>
    {
        Task<IEnumerable<Event>> GetEventById(string userId);
    }
}
