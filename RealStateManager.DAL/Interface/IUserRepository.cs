using Microsoft.AspNetCore.Identity;
using RealStateManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Interface
{
    public interface IUserRepository : IRepositoryGeneric<User>
    {
        int VerifyIfRegisterExist();
        Task LoginUser(User user, bool remember);
        Task LogOutUser();
        Task<IdentityResult> CreateUser(User user, string password);
        Task IncludeUserInFunction(User user, string function);

        Task<User> GetUserByEmail(string email);

        Task UpdateUser(User user);

        Task<bool> VerifyIfUserInFunction(User user, string function);

        Task<IEnumerable<string>> GetUserFunctions(User user);

        Task<IdentityResult> RemoveUserFunctions(User user, IEnumerable<string> functions);

        Task<IdentityResult> IncludeUserInFunctions(User user, IEnumerable<string> functions);
    }
}
