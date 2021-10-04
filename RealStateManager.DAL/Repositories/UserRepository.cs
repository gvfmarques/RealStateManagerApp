using Microsoft.AspNetCore.Identity;
using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
