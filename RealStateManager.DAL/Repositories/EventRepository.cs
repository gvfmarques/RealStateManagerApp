using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Repositories
{
    public class EventRepository : RepositoryGeneric<Event>, IEventRepository
    {
        private readonly RealStateManagerContext _realStateManagerContext;

        public EventRepository(RealStateManagerContext realStateManagerContext) : base(realStateManagerContext)
        {
            _realStateManagerContext = realStateManagerContext;
        }

        public async Task<IEnumerable<Event>> GetEventById(string userId)
        {
            try
            {
                return await _realStateManagerContext.Events.Where(e => e.UserId == userId).ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
