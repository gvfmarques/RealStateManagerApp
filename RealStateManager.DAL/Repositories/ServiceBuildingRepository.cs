using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.DAL.Repositories
{
    public class ServiceBuildingRepository : RepositoryGeneric<ServiceBuilding>, IServiceBuildingRepository
    {
         public ServiceBuildingRepository(RealStateManagerContext realStateManagerContext) : base(realStateManagerContext)
         {
         }
    }
}
