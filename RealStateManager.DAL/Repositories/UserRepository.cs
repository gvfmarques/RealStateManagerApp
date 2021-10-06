using Microsoft.AspNetCore.Identity;
using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Repositories
{
    public class UserRepository : RepositoryGeneric<User>, IUserRepository
    {
        private readonly RealStateManagerContext _realStateManagerContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _managerLogin;

        public UserRepository(RealStateManagerContext realStateManagerContext, UserManager<User> userManager, SignInManager<User> managerLogin) : base(realStateManagerContext)
        {
            _realStateManagerContext = realStateManagerContext;
            _userManager = userManager;
            _managerLogin = managerLogin;
        }

        public async Task<IdentityResult> CreateUser(User user, string password)
        {
            try
            {
                return await _userManager.CreateAsync(user, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task IncludeUserInFunction(User user, string function)
        {
            try
            {
                await _userManager.AddToRoleAsync(user, function);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task LoginUser(User user, bool remember)
        {
            try
            {
                await _managerLogin.SignInAsync(user, remember);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task LogOutUser()
        {
            try
            {
                await _managerLogin.SignOutAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int VerifyIfRegisterExist()
        {
            try
            {
                return _realStateManagerContext.Users.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                return await _userManager.FindByEmailAsync(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateUser(User user)
        {
            try
            {
                await _userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> VerifyIfUserInFunction(User user, string function)
        {
            try
            {
                return await _userManager.IsInRoleAsync(user, function);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<string>> GetUserFunctions(User user)
        {
            try
            {
                return await _userManager.GetRolesAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IdentityResult> RemoveUserFunctions(User user, IEnumerable<string> functions)
        {
            try
            {
                return await _userManager.RemoveFromRolesAsync(user, functions);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IdentityResult> IncludeUserInFunctions(User user, IEnumerable<string> functions)
        {
            try
            {
                return await _userManager.AddToRolesAsync(user, functions);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetUserByName(ClaimsPrincipal user)
        {
            try
            {
                return await _userManager.FindByNameAsync(user.Identity.Name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetUserById(string userId)
        {
            try
            {
                return await _userManager.FindByIdAsync(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CodePassword(User user, string password)
        {
            try
            {
                return _userManager.PasswordHasher.HashPassword(user, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
