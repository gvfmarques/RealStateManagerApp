using Microsoft.AspNetCore.Identity;
using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Repositories
{
    public class FunctionRepository : RepositoryGeneric<Function>, IFunctionRepository
    {
        private readonly RoleManager<Function> _managingFunctions;
        public FunctionRepository(RealStateManagerContext _realStateManagerContext, RoleManager<Function> managingFunctions) : base(_realStateManagerContext)
        {
            _managingFunctions = managingFunctions;
        }

        public async Task AddFunction(Function function)
        {
            try
            {
                function.Id = Guid.NewGuid().ToString();
                await _managingFunctions.CreateAsync(function);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task UpdateFunction(Function function)
        {
            try
            {
                Function f = await GetById(function.Id);
                f.Name = function.Name;
                f.NormalizedName = function.NormalizedName;
                f.Description = function.Description;
                await _managingFunctions.UpdateAsync(f);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
