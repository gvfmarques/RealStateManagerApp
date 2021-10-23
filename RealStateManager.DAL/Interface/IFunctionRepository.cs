using RealStateManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Interface
{
    public interface IFunctionRepository : IRepositoryGeneric<Function>
    {
        Task AddFunction(Function function);

        Task UpdateFunction(Function function);
    }
}
