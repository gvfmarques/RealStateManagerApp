using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using RealStateManager.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealStateManager.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IFunctionRepository _functionRepository;
        private readonly IWebHostEnvironment _webHostEnvironmnet;

        public UserController(IUserRepository userRepository, IWebHostEnvironment webHostEnvironment, IFunctionRepository functionRepository)
        {
            _userRepository = userRepository;
            _webHostEnvironmnet = webHostEnvironment;
            _functionRepository = functionRepository;
        }

        [Authorize(Roles = "Manager, ResidentManager")]
        public async Task<IActionResult> Index()
        {
            return View(await _userRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(ViewModelRegister model, IFormFile picture)
        {
            if(ModelState.IsValid)
            {
                if(picture != null)
                {
                    string directoryFolder = Path.Combine(_webHostEnvironmnet.WebRootPath, "Images");
                    string namePicture = Guid.NewGuid().ToString() + picture.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(directoryFolder, namePicture), FileMode.Create))
                    {
                        await picture.CopyToAsync(fileStream);
                        model.Picture = "~/Images/" + namePicture;
                    }
                }

                User user = new User();
                IdentityResult userCreated;

                if(_userRepository.VerifyIfRegisterExist() == 0)
                {
                    user.UserName = model.Name;
                    user.Identification = model.Identification;
                    user.Email = model.Email;
                    user.PhoneNumber = model.Phone;
                    user.Picture = model.Picture;
                    user.FirstAccess = false;
                    user.Status = StatusAccount.Approved;

                    userCreated = await _userRepository.CreateUser(user, model.Password);

                    if(userCreated.Succeeded)
                    {
                        await _userRepository.IncludeUserInFunction(user, "Manager");
                        await _userRepository.LoginUser(user, false);
                        return RedirectToAction("Index", "User");
                    }
                }

                user.UserName = model.Name;
                user.Identification = model.Identification;
                user.Email = model.Email;
                user.PhoneNumber = model.Phone;
                user.Picture = model.Picture;
                user.FirstAccess = true;
                user.Status = StatusAccount.Analysing;

                userCreated = await _userRepository.CreateUser(user, model.Password);

                if(userCreated.Succeeded)
                {
                    return View("Analyse", user.UserName);
                }

                else
                {
                    foreach(IdentityError error in userCreated.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
                await _userRepository.LogOutUser();


            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                User user = await _userRepository.GetUserByEmail(model.Email);

                if(user != null)
                {
                    if(user.Status == StatusAccount.Analysing)
                    {
                        return View("Analyse", user.UserName);
                    }

                    else if(user.Status == StatusAccount.Disapproved)
                    {
                        return View("Disapproved", user.UserName);
                    }

                    else if(user.FirstAccess == true)
                    {
                        return RedirectToAction(nameof(RedefinePassword), user);
                    }

                    else
                    {
                        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

                        if(passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) != PasswordVerificationResult.Failed)
                        {
                            await _userRepository.LoginUser(user, false); 
                            if (await _userRepository.VerifyIfUserInFunction(user, "Resident"))
                                return RedirectToAction(nameof(MyInformations));
                            else
                                return RedirectToAction(nameof(Index));
                        }

                        else
                        {
                            ModelState.AddModelError("", "User and/or invalid password!");
                            return View(model);
                        }
                    }
                }

                else
                {
                    ModelState.AddModelError("", "User and/or invalid password!");
                    return View(model);
                }
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout(string name)
        {
            await _userRepository.LogOutUser();
            return RedirectToAction("Login");
        }

        public IActionResult Analyse(string name)
        {
            return View(name);
        }

        public IActionResult Disapproved(string name)
        {
            return View(name);
        }

        [Authorize(Roles = "Manager, ResidentManager")]
        public async Task<JsonResult> ApproveUser(string userId)
        {
            User user = await _userRepository.GetById(userId);
            user.Status = StatusAccount.Approved;
            await _userRepository.IncludeUserInFunction(user, "Resident");
            await _userRepository.UpdateUser(user);

            return Json(true);
        }

        [Authorize(Roles = "Manager, ResidentManager")]
        public async Task<JsonResult> DisapproveUser(string userId)
        {
            User user = await _userRepository.GetById(userId);
            user.Status = StatusAccount.Disapproved;
            await _userRepository.UpdateUser(user);

            return Json(true);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<IActionResult> UserManagement(string userId, string name)
        {
            if (userId == null)
                return NotFound();

            TempData["userId"] = userId;
            ViewBag.Name = name;
            User user = await _userRepository.GetById(userId);

            if (user == null)
                return NotFound();

            List<FunctionUserViewModel> viewModel = new List<FunctionUserViewModel>();

            foreach(Function function in await _functionRepository.GetAll())
            {
                FunctionUserViewModel model = new FunctionUserViewModel
                {
                    FunctionId = function.Id,
                    Name = function.Name,
                    Description = function.Description
                };

                if (await _userRepository.VerifyIfUserInFunction(user, function.Name))
                {
                    model.IsSelected = true;
                }

                else
                    model.IsSelected = false;

                viewModel.Add(model);
            }
            return View(viewModel);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> UserManagement(List<FunctionUserViewModel> model)
        {
            string userId = TempData["userId"].ToString();

            User user = await _userRepository.GetById(userId);

            if (user == null)
                return NotFound();

            IEnumerable<string> functions = await _userRepository.GetUserFunctions(user);
            IdentityResult result = await _userRepository.RemoveUserFunctions(user, functions);

            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Unable to update user roles!");
                TempData["Exclusao"] = $"Unable to update user roles for {user.UserName}";
                return View("UserManagement", userId);
            }

            result = await _userRepository.IncludeUserInFunctions(user,
                model.Where(x => x.IsSelected == true).Select(x => x.Name));

            if (!result.Succeeded)
            {
                ModelState.TryAddModelError("", "Unable to update user roles!");
                TempData["Exclusion"] = $"Unable to update user roles for {user.UserName}";
                return View("UserManagement", userId);
            }

            TempData["Update"] = $"User roles for {user.UserName} have been updated ";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MyInformations()
        {
            return View(await _userRepository.GetUserByName(User));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Updating(string id)
        {
            User user = await _userRepository.GetById(id);

            if (user == null)
                return NotFound();

            UpdateViewModel model = new UpdateViewModel
            {
                UserId = user.Id,
                Name = user.UserName,
                Identification = user.Identification,
                Email = user.Email,
                Picture = user.Picture,
                Phone = user.PhoneNumber
            };

            TempData["Picture"] = user.Picture;

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Updating(UpdateViewModel viewModel, IFormFile picture)
        {
            if(ModelState.IsValid)
            {
                if (picture != null)
                {
                    string directoryFolder = Path.Combine(_webHostEnvironmnet.WebRootPath, "Images");
                    string namePicture = Guid.NewGuid().ToString() + picture.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(directoryFolder, namePicture), FileMode.Create))
                    {
                        await picture.CopyToAsync(fileStream);
                        viewModel.Picture = "~/Images/" + namePicture;
                    }
                }

                else
                    viewModel.Picture = TempData["Picture"].ToString();

                User user = await _userRepository.GetUserById(viewModel.UserId);
                user.UserName = viewModel.Name;
                user.Identification = viewModel.Identification;
                user.PhoneNumber = viewModel.Phone;
                user.Picture = viewModel.Picture;
                user.Email = viewModel.Email;

                await _userRepository.UpdateUser(user);

                TempData["Updating"] = "Updated record";

                if(await _userRepository.VerifyIfUserInFunction(user, "Manager") || 
                    await _userRepository.VerifyIfUserInFunction(user, "ResidentManager"))
                {
                    return RedirectToAction(nameof(Index));
                }

                else
                    return RedirectToAction(nameof(MyInformations));
            }

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult RedefinePassword(User user)
        {
            LoginViewModel model = new LoginViewModel
            {
                Email = user.Email
            };

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RedefinePassword(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                User user = await _userRepository.GetUserByEmail(model.Email);
                model.Password = _userRepository.CodePassword(user, model.Password);
                user.PasswordHash = model.Password;
                user.FirstAccess = false;
                await _userRepository.UpdateUser(user);
                await _userRepository.LoginUser(user, false);

                return RedirectToAction(nameof(MyInformations));
            }

            return View(model);
        }
    }
}
