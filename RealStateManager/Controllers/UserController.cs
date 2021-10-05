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

        public async Task<IActionResult> Index()
        {
            return View(await _userRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

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

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
                await _userRepository.LogOutUser();


            return View();
        }

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
                        return View("RedefinePassword", user);
                    }

                    else
                    {
                        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

                        if(passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) != PasswordVerificationResult.Failed)
                        {
                            await _userRepository.LoginUser(user, false);
                            return RedirectToAction("Index");
                        }

                        else
                        {
                            ModelState.AddModelError("", "Email and/or invalid password!");
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

        public async Task<JsonResult> ApproveUser(string userId)
        {
            User user = await _userRepository.GetById(userId);
            user.Status = StatusAccount.Approved;
            await _userRepository.IncludeUserInFunction(user, "Resident");
            await _userRepository.UpdateUser(user);

            return Json(true);
        }

        public async Task<JsonResult> DisapproveUser(string userId)
        {
            User user = await _userRepository.GetById(userId);
            user.Status = StatusAccount.Disapproved;
            await _userRepository.UpdateUser(user);

            return Json(true);
        }

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
    }
}
