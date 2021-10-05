using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.DAL.Repositories
{
    public class FunctionRepository : RepositoryGeneric<Function>, IFunctionRepository
    {
        public FunctionRepository(RealStateManagerContext _realStateManagerContext) : base(_realStateManagerContext)
        {
        }
    }
}
